using MassTransit;
using HRManagerApp.Service;
using CommonLibrary.Contracts;

namespace HRManagerApp.Service;

public class PreferencesConsumer : IConsumer<PreferencesMessage>
{
    private readonly HRManagerService _hrManagerService;

    public PreferencesConsumer(HRManagerService hrManagerService)
    {
        _hrManagerService = hrManagerService;
    }

    public async Task Consume(ConsumeContext<PreferencesMessage> context)
    {
        var preferences = context.Message;

        if (preferences.Role == "Junior")
        {
            await _hrManagerService.SaveJuniorPreferencesAsync(preferences);
        }
        else if (preferences.Role == "TeamLead")
        {
            await _hrManagerService.SaveTeamLeadPreferencesAsync(preferences);
        }
    }
}
