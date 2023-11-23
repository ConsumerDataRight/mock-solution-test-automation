using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    public static class CdrErrorExtensions
    {
        private static readonly Dictionary<CdsError, CdsErrorInfo> _errors = InitErrors();

        public static CdsErrorInfo GetErrorInfo(this CdsError error)
        {
            if (_errors.TryGetValue(error, out var res))
            {
                return res;
            }

            throw new ArgumentOutOfRangeException($"Error {error} doesn't have any Register error attribute");
        }

        private static Dictionary<CdsError, CdsErrorInfo> InitErrors()
        {
            var result = new Dictionary<CdsError, CdsErrorInfo>();
            foreach (var i in Enum.GetValues(typeof(CdsError)))
            {
                var error = Enum.Parse<CdsError>(i.ToString());
                var attr = error.GetAttribute<CdrErrorAttribute>();

                if (attr != null)
                {
                    result.Add(error, new CdsErrorInfo { Title = attr.Title, ErrorCode = attr.ErrorCode });
                }
            }

            return result;
        }
    }
}
