using System.Collections.Generic;
using CommonLibrary.Dto;
using HRManagerApp.RefitClients;
using Refit;

namespace HRManager.Service
{
    public class HRManagerService
    {
        private readonly List<PreferencesDto> _juniorPreferences = new();
        private readonly List<PreferencesDto> _teamLeadPreferences = new();
        private readonly IHRDirectorApi _hrDirectorApi;

        public HRManagerService(IHRDirectorApi hrDirectorApi)
        {
            _hrDirectorApi = hrDirectorApi;
        }

        public void SaveJuniorPreferences(PreferencesDto preferences)
        {
            _juniorPreferences.Add(preferences);
            CheckIfAllPreferencesReceived();
        }

        public void SaveTeamLeadPreferences(PreferencesDto preferences)
        {
            _teamLeadPreferences.Add(preferences);
            CheckIfAllPreferencesReceived();
        }

        private void CheckIfAllPreferencesReceived()
        {
            if (_juniorPreferences.Count >= 5 && _teamLeadPreferences.Count >= 5)
            {
                SendFinalDistribution();
            }
        }

        private async void SendFinalDistribution()
        {
            var result = new TeamDistributionDto
            {
                JuniorPreferences = _juniorPreferences,
                TeamLeadPreferences = _teamLeadPreferences
            };

            await _hrDirectorApi.SendFinalDistributionAsync(result);
        }
    }
}
