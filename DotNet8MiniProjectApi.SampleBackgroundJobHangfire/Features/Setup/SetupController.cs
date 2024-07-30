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

        [HttpGet]
        public IActionResult Get()
        {
            var result = _bL_Setup.CreateSetup();
            return Ok(result);
        }
    }
}
