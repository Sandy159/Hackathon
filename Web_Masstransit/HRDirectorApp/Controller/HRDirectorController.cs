using Microsoft.AspNetCore.Mvc;
using HRDirectorApp.Service;
using CommonLibrary.Contracts;

[ApiController]
[Route("api/[controller]")]
public class HRDirectorController : ControllerBase
{
    private readonly HRDirectorService _hrDirectorService;

    public HRDirectorController(HRDirectorService hrDirectorService)
    {
        _hrDirectorService = hrDirectorService;
    }

    [HttpPost("evaluate-harmony")]
    public async Task<IActionResult> EvaluateHarmonyAsync([FromBody] PreferencesAndTeamsResponse preferencesAndTeamsResponse)
    {
        try
        {
            var harmony = await _hrDirectorService.EvaluateHarmonyAsync(preferencesAndTeamsResponse);
            return Ok(new { AverageHarmony = harmony });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error calculating harmony: {ex.Message}");
        }
    }
}
