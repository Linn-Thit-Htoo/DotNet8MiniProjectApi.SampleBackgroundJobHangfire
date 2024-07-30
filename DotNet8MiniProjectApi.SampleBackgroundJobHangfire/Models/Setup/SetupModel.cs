namespace DotNet8MiniProjectApi.SampleBackgroundJobHangfire.Models.Setup
{
    public class SetupModel
    {
        public string SetupId { get; set; } = null!;

        public string SetupCode { get; set; } = null!;

        public bool IsExpired { get; set; }
    }
}
