
using HRManagerApp.Database;
using HRManagerApp.Strategy;
using CommonLibrary.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MassTransit;

namespace HRManagerApp.Service
{
    public class HRManagerService
    {
        private readonly ManagerDbContext _dbContext;
        private readonly ILogger<HRManagerService> _logger;
        private readonly ITeamBuildingStrategy _teamBuildingStrategy;
        private readonly IPublishEndpoint _publishEndpoint;
        private readonly TimeSpan _checkInterval = TimeSpan.FromSeconds(30);
        private readonly HashSet<int> _finalDistributionSentPerHackathon = new HashSet<int>();

        public HRManagerService(ILogger<HRManagerService> logger, ManagerDbContext dbContext, IPublishEndpoint publishEndpoint)
        {
            _teamBuildingStrategy = new SimpleTeamBuildingStrategy();
            _logger = logger;
            _dbContext = dbContext;
            _publishEndpoint = publishEndpoint;
        }

        public async Task SaveJuniorPreferencesAsync(PreferencesMessage preferences)
        {
            Console.WriteLine($"Received Junior Preference: {preferences.EmployeeId} for Hackathon: {preferences.HackathonId}");
            await SavePreferenceToDatabaseAsync(preferences);
        }

        public async Task SaveTeamLeadPreferencesAsync(PreferencesMessage preferences)
        {
            Console.WriteLine($"Received Team Lead Preference: {preferences.EmployeeId} for Hackathon: {preferences.HackathonId}");
            await SavePreferenceToDatabaseAsync(preferences);
        }

        public async Task SavePreferenceToDatabaseAsync(PreferencesMessage preferences)
        {
            try
            {
                _logger.LogInformation($"Saving preferences to database for EmployeeId: {preferences.EmployeeId} for Hackathon: {preferences.HackathonId}");
                var entity = new EmployeePreference
                {
                    EmployeeId = preferences.EmployeeId,
                    Role = preferences.Role,
                    Preferences = string.Join(",", preferences.Preferences),
                    HackathonId = preferences.HackathonId
                };

                await _dbContext.EmployeePreferences.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Successfully saved preferences for EmployeeId: {preferences.EmployeeId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving preferences for EmployeeId: {preferences.EmployeeId}");
                throw;
            }
        }

        private async Task CheckPreferencesAndSendTeamsAsync()
        {
            try
            {
                var allPreferences = await _dbContext.EmployeePreferences.ToListAsync();
                var groupedByHackathon = allPreferences
                    .GroupBy(p => p.HackathonId)
                    .ToList();

                foreach (var hackathonGroup in groupedByHackathon)
                {
                    var hackathonId = hackathonGroup.Key;

                    if (_finalDistributionSentPerHackathon.Contains(hackathonId))
                    {
                        _logger.LogInformation($"Teams for Hackathon {hackathonId} have already been sent, skipping...");
                        continue; 
                    }

                    var juniorPreferences = hackathonGroup.Where(p => p.Role == "Junior").ToList();
                    var teamLeadPreferences = hackathonGroup.Where(p => p.Role == "TeamLead").ToList();

                    _logger.LogInformation($"Checking preferences for Hackathon {hackathonId}: Juniors - {juniorPreferences.Count}, TeamLeads - {teamLeadPreferences.Count}");

                    if (juniorPreferences.Count >= 5 && teamLeadPreferences.Count >= 5)
                    {
                        _logger.LogInformation($"Enough preferences collected for Hackathon {hackathonId}, preparing to send to HR Director...");

                        var juniorMessages = juniorPreferences.Select(p => new PreferencesMessage
                        {
                            EmployeeId = p.EmployeeId,
                            Role = p.Role,
                            Preferences = p.Preferences.Split(',').Select(int.Parse).ToArray(),

                            HackathonId = p.HackathonId
                        }).ToList();

                        var teamLeadMessages = teamLeadPreferences.Select(p => new PreferencesMessage
                        {
                            EmployeeId = p.EmployeeId,
                            Role = p.Role,
                            Preferences = p.Preferences.Split(',').Select(int.Parse).ToArray(),
                            HackathonId = p.HackathonId
                        }).ToList();

                        await SendFinalDistribution(hackathonId, juniorMessages, teamLeadMessages);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while checking preferences and sending teams.");
            }
        }

        private async Task SendFinalDistribution(int hackathonId, List<PreferencesMessage> juniorPreferences, List<PreferencesMessage> teamLeadPreferences)
        {
            try
            {
                _logger.LogInformation($"Building teams for Hackathon {hackathonId}...");
                var juniors = juniorPreferences.Select(w => new Employee { Id = w.EmployeeId }).ToList();
                var teamLeads = teamLeadPreferences.Select(w => new Employee { Id = w.EmployeeId }).ToList();

                var teams = _teamBuildingStrategy.BuildTeams(teamLeads, juniors, teamLeadPreferences, juniorPreferences);
                _logger.LogInformation($"Built {teams.Count()} teams for Hackathon {hackathonId}.");

                var preferencesAndTeamsResponse = new PreferencesAndTeamsResponse
                {
                    Teams = teams.Select(team => new Team(team.TeamLeadId, team.JuniorId)).ToList(),
                    JuniorPreferences = juniorPreferences,
                    TeamLeadPreferences = teamLeadPreferences,
                    HackathonId = hackathonId
                };

                _logger.LogInformation($"Publishing final distribution for Hackathon {hackathonId} to HR Director...");
                await _publishEndpoint.Publish(preferencesAndTeamsResponse);
                _finalDistributionSentPerHackathon.Add(hackathonId); 
                _logger.LogInformation($"Final distribution successfully sent to HR Director for Hackathon {hackathonId}.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to send final distribution for Hackathon {hackathonId}.");
            }
        }

        public async Task StartCheckingDatabaseAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await CheckPreferencesAndSendTeamsAsync();
                await Task.Delay(_checkInterval, stoppingToken);
            }
        }
    }
}
