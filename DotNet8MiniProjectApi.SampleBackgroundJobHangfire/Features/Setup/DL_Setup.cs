using DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Db.AppDbContexts;
using DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Models;
using DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Models.Setup;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Text;

namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Features.Setup
{
    public class DL_Setup
    {
        private readonly AppDbContext _context;

        public DL_Setup(AppDbContext context)
        {
            _context = context;
        }

        public async Task<Result<SetupResponseModel>> CreateSetup()
        {
            Result<SetupResponseModel> responseModel;
            try
            {
                string sixDigitNumber = GetSixRandomNumbers();
                var model = new TblSetup()
                {
                    SetupCode = sixDigitNumber,
                    SetupId = Ulid.NewUlid().ToString(),
                    IsExpired = false
                };

                await _context.TblSetups.AddAsync(model);
                await _context.SaveChangesAsync();

                responseModel = Result<SetupResponseModel>.SuccessResult(new SetupResponseModel { Code = sixDigitNumber});
            }
            catch (Exception ex)
            {
                responseModel = Result<SetupResponseModel>.FailureResult(ex);
            }

            return responseModel;
        }

        private string GetSixRandomNumbers()
        {
            Random r = new();
            int randNum = r.Next(1000000);
            string sixDigitNumber = randNum.ToString("D6");

            return sixDigitNumber;
        }
    }
}
