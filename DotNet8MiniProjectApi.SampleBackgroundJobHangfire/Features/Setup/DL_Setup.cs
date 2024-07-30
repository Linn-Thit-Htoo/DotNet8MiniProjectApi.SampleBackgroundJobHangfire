namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Features.Setup;

public class DL_Setup
{
    private readonly AppDbContext _context;

    public DL_Setup(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Result<CreateSetupResponseModel>> CreateSetup()
    {
        Result<CreateSetupResponseModel> responseModel;
        var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            string sixDigitNumber = GetSixRandomNumbers();
            var model = GetTblSetup(sixDigitNumber);

            await _context.TblSetups.AddAsync(model);
            await _context.SaveChangesAsync();

            responseModel = Result<CreateSetupResponseModel>.SuccessResult(
                new CreateSetupResponseModel { Code = sixDigitNumber }
            );
            BackgroundJob.Schedule<DL_Setup>(
                x => x.ExpireCode(model.SetupId),
                TimeSpan.FromMinutes(1)
            );
            await transaction.CommitAsync();
        }
        catch (Exception ex)
        {
            await transaction.RollbackAsync();
            responseModel = Result<CreateSetupResponseModel>.FailureResult(ex);
        }

        return responseModel;
    }

    public async Task<Result<SetupResponseModel>> ExpireCode(string id)
    {
        Result<SetupResponseModel> responseModel;
        try
        {
            var item = await _context.TblSetups.FindAsync(id);
            if (item is null)
            {
                responseModel = Result<SetupResponseModel>.NotFoundResult();
                goto result;
            }

            item.IsExpired = true;
            _context.TblSetups.Update(item);
            await _context.SaveChangesAsync();

            responseModel = Result<SetupResponseModel>.SuccessResult();
        }
        catch (Exception ex)
        {
            responseModel = Result<SetupResponseModel>.FailureResult(ex);
        }

    result:
        return responseModel;
    }

    private string GetSixRandomNumbers()
    {
        Random r = new();
        int randNum = r.Next(1000000);
        string sixDigitNumber = randNum.ToString("D6");

        return sixDigitNumber;
    }

    private TblSetup GetTblSetup(string sixDigitNumber)
    {
        return new TblSetup()
        {
            SetupCode = sixDigitNumber,
            SetupId = Ulid.NewUlid().ToString(),
            IsExpired = false
        };
    }
}
