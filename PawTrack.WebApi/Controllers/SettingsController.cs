using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PawTrack.Business.Operations.Setting;

namespace PawTrack.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingService _settingService;

        public SettingsController(ISettingService settingService)
        {
            _settingService = settingService;    
        }

        [HttpPatch]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ToggleMaintanence()
        {
            await _settingService.ToggleMaintanence();

            return Ok();
        }
    }
}
