using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HRManagerApp.RefitClients;
using HRManagerApp.Database;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CommonLibrary.Contracts;
using HRManagerApp.Strategy;
using Newtonsoft.Json;

namespace HRManagerApp.Service
{
    public class HRManagerService
    {
        private readonly ManagerDbContext _dbContext;
        private readonly List<PreferencesMessage> _juniorPreferences = new();
        private readonly List<PreferencesMessage> _teamLeadPreferences = new();
        private readonly IHRDirectorApi _hrDirectorApi;
        private readonly ILogger<HRManagerService> _logger;
        private readonly ITeamBuildingStrategy _teamBuildingStrategy;
        private bool _finalDistributionSent = false;

        public HRManagerService(IHRDirectorApi hrDirectorApi, ILogger<HRManagerService> logger, ManagerDbContext dbContext)
        {
            _teamBuildingStrategy = new SimpleTeamBuildingStrategy();
            _hrDirectorApi = hrDirectorApi;
            _logger = logger;
            _dbContext = dbContext;
        }

        public async Task ApplyMigrationsAsync()
        {
            try
            {
                _logger.LogInformation("Applying database migrations...");
                await _dbContext.Database.MigrateAsync();
                _logger.LogInformation("Database migrations applied successfully.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while applying migrations.");
                throw;
            }
        }

        public async Task SaveJuniorPreferencesAsync(PreferencesMessage preferences)
        {
            Console.WriteLine($"Received Junior Preference: {preferences.EmployeeId}");
            _juniorPreferences.Add(preferences);
            await SavePreferenceToDatabaseAsync(preferences);
            CheckIfAllPreferencesReceived();
        }

        public async Task SaveTeamLeadPreferencesAsync(PreferencesMessage preferences)
        {
            Console.WriteLine($"Received Team Lead Preference: {preferences.EmployeeId}");
            _teamLeadPreferences.Add(preferences);
            await SavePreferenceToDatabaseAsync(preferences);
            CheckIfAllPreferencesReceived();
        }

        private void CheckIfAllPreferencesReceived()
        {
            Console.WriteLine($"Junior Preferences Count: {_juniorPreferences.Count}, Team Lead Preferences Count: {_teamLeadPreferences.Count}");

            if (_juniorPreferences.Count >= 5 && _teamLeadPreferences.Count >= 5 && !_finalDistributionSent)
            {
                Console.WriteLine("All preferences received, sending to HR Director...");
                _finalDistributionSent = true;
                SendFinalDistribution();
            }
        }

        private async Task SendFinalDistribution()
        {
            try
            {
                Console.WriteLine("Building teams...");
                var juniors = _juniorPreferences.Select(w => new Employee {Id = w.EmployeeId }).ToList();
                var teamLeads = _teamLeadPreferences.Select(w => new Employee { Id = w.EmployeeId }).ToList();

                var teams = _teamBuildingStrategy.BuildTeams(teamLeads, juniors, _teamLeadPreferences, _juniorPreferences);
                Console.WriteLine($"Built {teams.Count()} teams.");

                var preferencesAndTeamsResponse = new PreferencesAndTeamsResponse
                {
                    Teams = teams.Select(team => new Team(team.TeamLeadId, team.JuniorId)).ToList(),
                    JuniorPreferences = _juniorPreferences,
                    TeamLeadPreferences = _teamLeadPreferences
                };

                Console.WriteLine("Sending final distribution to HR Director...");
                await _hrDirectorApi.EvaluateHarmonyAsync(preferencesAndTeamsResponse);
                Console.WriteLine("Final distribution successfully sent to HR Director.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send final distribution.");
            }
        }

        public async Task SavePreferenceToDatabaseAsync(PreferencesMessage preferences)
        {
            try
            {
                Console.WriteLine($"Saving preferences to database for EmployeeId: {preferences.EmployeeId}");
                var entity = new EmployeePreference
                {
                    EmployeeId = preferences.EmployeeId,
                    Role = preferences.Role,
                    Preferences = string.Join(",", preferences.Preferences)
                };

                await _dbContext.EmployeePreferences.AddAsync(entity);
                await _dbContext.SaveChangesAsync();
                Console.WriteLine($"Successfully saved preferences for EmployeeId: {preferences.EmployeeId}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error saving preferences for EmployeeId: {preferences.EmployeeId}");
                throw;
            }
        }
    }
}
