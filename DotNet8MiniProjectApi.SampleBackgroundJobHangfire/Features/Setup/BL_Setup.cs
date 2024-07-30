using DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Models.Setup;
using DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Models;

namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Features.Setup
{
    public class BL_Setup
    {
        private readonly DL_Setup _dL_Setup;

        public BL_Setup(DL_Setup dL_Setup)
        {
            _dL_Setup = dL_Setup;
        }

        public async Task<Result<CreateSetupResponseModel>> CreateSetupAsync()
        {
            return await _dL_Setup.CreateSetup();
        }
    }
}
