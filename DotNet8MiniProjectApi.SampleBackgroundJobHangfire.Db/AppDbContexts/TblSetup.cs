using System;
using System.Collections.Generic;

namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Db.AppDbContexts;

public partial class TblSetup
{
    public string SetupId { get; set; } = null!;

    public string SetupCode { get; set; } = null!;

    public bool IsExpired { get; set; }
}
