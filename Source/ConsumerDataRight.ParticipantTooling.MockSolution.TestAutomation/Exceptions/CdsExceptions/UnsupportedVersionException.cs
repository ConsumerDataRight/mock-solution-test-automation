using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    [Serializable]
    public class UnsupportedVersionException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedVersionException"/> class.
        /// <para>Status code: 406 (Not Acceptable).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public UnsupportedVersionException(string detail, string message)
            : base(CdsError.UnsupportedVersion, detail, System.Net.HttpStatusCode.NotAcceptable, message)
        { }

        public UnsupportedVersionException(string detail)
         : base(CdsError.UnsupportedVersion, detail, System.Net.HttpStatusCode.NotAcceptable, null)
        { }

        public UnsupportedVersionException()
        : base(CdsError.UnsupportedVersion, "Version requested is lower than the minimum version or greater than maximum version.", System.Net.HttpStatusCode.NotAcceptable, null)
        { }

        protected UnsupportedVersionException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
