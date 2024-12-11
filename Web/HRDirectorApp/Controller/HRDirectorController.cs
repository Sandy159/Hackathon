using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/[controller]")]
public class HRDirectorController : ControllerBase
{
    private readonly IHRDirectorService _hrDirectorService;

    public HRDirectorController(IHRDirectorService hrDirectorService)
    {
        _hrDirectorService = hrDirectorService;
    }

    [HttpPost("evaluate-harmony")]
    public IActionResult EvaluateHarmony([FromBody] TeamAssignmentRequest request)
    {
        var result = _hrDirectorService.EvaluateHarmony(request);
        return Ok(result);
    }
}
