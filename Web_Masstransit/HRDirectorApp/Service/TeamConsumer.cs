using CommonLibrary.Contracts;
using MassTransit;

namespace HRDirectorApp.Service;

public class TeamConsumer : IConsumer<PreferencesAndTeamsResponse>
{
    private readonly HRDirectorService _hrDirectorService;

    public TeamConsumer(HRDirectorService hrDirectorService)
    {
        _hrDirectorService = hrDirectorService;
    }

    public async Task Consume(ConsumeContext<PreferencesAndTeamsResponse> context)
    {
        var teams = context.Message;
        await _hrDirectorService.EvaluateHarmonyAsync(teams);
    }
}