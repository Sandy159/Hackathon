using Microsoft.AspNetCore.Mvc;
using HRManagerApp.Service;
using CommonLibrary.Contracts;

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

        // Метод для отправки предпочтений для джуниоров и тимлидов
        [HttpPost("submit-preferences")]
        public async Task<IActionResult> SubmitPreferencesAsync([FromBody] PreferencesMessage preferencesMessage)
        {
            if (preferencesMessage == null)
            {
                return BadRequest("Preferences are required.");
            }

            // Логика для определения типа сотрудника, например, на основе role или других полей в preferences
            if (preferencesMessage.Role == "Junior")
            {
                _hrManagerService.SaveJuniorPreferencesAsync(preferencesMessage);
                return Ok("Junior preferences received.");
            }
            else if (preferencesMessage.Role == "TeamLead")
            {
                _hrManagerService.SaveTeamLeadPreferencesAsync(preferencesMessage);
                return Ok("Team lead preferences received.");
            }
            else
            {
                return BadRequest("Invalid employee role.");
            }
        }
    }
}
