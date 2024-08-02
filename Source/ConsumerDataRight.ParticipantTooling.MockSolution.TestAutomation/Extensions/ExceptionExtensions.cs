namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception Log(this Exception ex)
        {
            Serilog.Log.Error(ex, ex.Message);
            return ex;
        }

        public static void LogAndThrow(this Exception ex)
        {
            Serilog.Log.Error(ex, ex.Message);
            throw new Exception("An error occurred in. See inner exception for details.", ex);
        }
    }
}
