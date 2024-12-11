using Microsoft.AspNetCore.Mvc;
using HRManager.Service;
using CommonLibrary.Dto;

namespace HRManagerApp.Controllers
{
    [ApiController]
    [Route("api/hr")]
    public class HRManagerController : ControllerBase
    {
        private readonly HRManagerService _hrManagerService;

        public HRManagerController(HRManagerService hrManagerService)
        {
            _hrManagerService = hrManagerService;
        }

        [HttpPost("submit-junior-preferences")]
        public IActionResult SubmitJuniorPreferences([FromBody] PreferencesDto preferences)
        {
            _hrManagerService.SaveJuniorPreferences(preferences);
            return Ok("Junior preferences received.");
        }

        [HttpPost("submit-teamlead-preferences")]
        public IActionResult SubmitTeamLeadPreferences([FromBody] PreferencesDto preferences)
        {
            _hrManagerService.SaveTeamLeadPreferences(preferences);
            return Ok("Team lead preferences received.");
        }
    }
}
