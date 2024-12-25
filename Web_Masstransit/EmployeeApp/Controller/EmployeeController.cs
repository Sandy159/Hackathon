using Microsoft.AspNetCore.Mvc;
using EmployeeApp.Service;

namespace EmployeeApp.Controller;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly EmployeeService _employeeService;

    public EmployeeController(EmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [HttpPost("generate-wishlists")]
    public async Task<IActionResult> GenerateAndSubmitWishlists()
    {
        await _employeeService.ProcessWishlistsAsync();
        return Ok("Wishlists generated and submitted successfully.");
    }
}
