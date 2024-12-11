using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;
using CommonLibrary.Contracts;
using HRManagerApp.RefitClients;
using CommonLibrary.Dto;

namespace HRManagerApp.Service
{
    public class DistributionService : IDistributionService
    {
        private readonly ConcurrentBag<Wishlist> _juniorPreferences = new();
        private readonly ConcurrentBag<Wishlist> _teamLeadPreferences = new();
        private readonly ConcurrentBag<Employee> _juniors = new();
        private readonly ConcurrentBag<Employee> _teamLeads = new();

        private readonly ITeamBuildingStrategy _teamBuildingStrategy;
        private readonly IHRDirectorApi _hrDirectorApi;
        private readonly ILogger<DistributionService> _logger;
        private bool _finalDistributionSent = false;

        public DistributionService(
            ITeamBuildingStrategy teamBuildingStrategy,
            IHRDirectorApi hrDirectorApi,
            ILogger<DistributionService> logger)
        {
            _teamBuildingStrategy = teamBuildingStrategy;
            _hrDirectorApi = hrDirectorApi;
            _logger = logger;
        }

        public void SaveJuniorPreferences(Employee employee, Wishlist wishlist)
        {
            _juniorPreferences.Add(wishlist);
            _juniors.Add(employee);
            CheckIfAllPreferencesReceived();
        }

        public void SaveTeamLeadPreferences(Employee employee, Wishlist wishlist)
        {
            _teamLeadPreferences.Add(wishlist);
            _teamLeads.Add(employee);
            CheckIfAllPreferencesReceived();
        }

        private void CheckIfAllPreferencesReceived()
        {
            _logger.LogInformation($"Junior Preferences: {_juniorPreferences.Count}, Team Lead Preferences: {_teamLeadPreferences.Count}");

            if (_juniorPreferences.Count >= 5 && _teamLeadPreferences.Count >= 5 && !_finalDistributionSent)
            {
                _logger.LogInformation("All preferences received, sending to HR Director...");
                _finalDistributionSent = true;
                SendFinalDistribution();
            }
        }

        private async void SendFinalDistribution()
        {
            try
            {
                var teams = _teamBuildingStrategy.BuildTeams(
                    _teamLeads.ToList(),
                    _juniors.ToList(),
                    _teamLeadPreferences.ToList(),
                    _juniorPreferences.ToList()).ToList();

                // Преобразуем JuniorPreferences и TeamLeadPreferences в List<PreferencesDto>
                var preferencesEntity = new TeamDistributionDto
                {
                    JuniorPreferences = _juniorPreferences
                        .Select(w => new PreferencesDto
                        {
                            EmployeeId = w.EmployeeId.ToString(),
                            Preferences = w.Preferences.Select(p => p.ToString()).ToList()
                        }).ToList(),

                    TeamLeadPreferences = _teamLeadPreferences
                        .Select(w => new PreferencesDto
                        {
                            EmployeeId = w.EmployeeId.ToString(),
                            Preferences = w.Preferences.Select(p => p.ToString()).ToList()
                        }).ToList()
                };

                await _hrDirectorApi.SendFinalDistributionAsync(preferencesEntity);
                _logger.LogInformation("Final distribution successfully sent to HR Director.");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Failed to send final distribution.");
            }
        }
    }
}
