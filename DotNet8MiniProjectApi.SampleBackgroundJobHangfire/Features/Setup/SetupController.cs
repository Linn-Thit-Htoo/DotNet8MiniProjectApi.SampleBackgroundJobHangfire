using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Features.Setup
{
    [Route("api/[controller]")]
    [ApiController]
    public class SetupController : ControllerBase
    {
        private readonly BL_Setup _bL_Setup;

        public SetupController(BL_Setup bL_Setup)
        {
            _bL_Setup = bL_Setup;
        }

        [HttpPost]
        public async Task<IActionResult> CreateSetup()
        {
            var result = await _bL_Setup.CreateSetupAsync();
            return Ok(result);
        }
    }
}
