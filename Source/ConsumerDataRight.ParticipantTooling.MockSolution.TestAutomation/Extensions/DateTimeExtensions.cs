namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    public static class DateTimeExtensions
    {
        public static string? GetDateFromFapiDate(string? XFapiAuthDate)
        {
            return XFapiAuthDate switch
            {
                "DateTime.UtcNow" => DateTime.UtcNow.ToString(),
                "DateTime.UtcNow+1" => DateTime.UtcNow.AddDays(1).ToString(),
                "DateTime.Now.RFC1123" => DateTime.Now.ToUniversalTime().ToString("r"),
                "DateTime.Now.RFC1123+1" => DateTime.Now.AddDays(1).ToUniversalTime().ToString("r"),
                "foo" or "000" or "" or null => XFapiAuthDate,
                _ => throw new ArgumentOutOfRangeException(nameof(XFapiAuthDate)).Log()
            };
        }

        /// <summary>
        /// Return datetime converted to Unix Epoch (number of seconds since 00:00:00 UTC on 1 Jan 1970)
        /// </summary>
        public static int UnixEpoch(this DateTime datetime)
        {
            return Convert.ToInt32(datetime.Subtract(DateTime.UnixEpoch).TotalSeconds);
        }
    }
}
