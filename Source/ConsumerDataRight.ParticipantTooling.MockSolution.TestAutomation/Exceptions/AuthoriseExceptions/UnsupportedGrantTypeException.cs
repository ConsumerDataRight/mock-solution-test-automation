using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class UnsupportedGrantTypeException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UnsupportedGrantTypeException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public UnsupportedGrantTypeException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "unsupported_grant_type", detail)
        { }

        public UnsupportedGrantTypeException()
      : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "unsupported_grant_type", Constants.ErrorMessages.General.UnsupportedGrantType)
        { }

        protected UnsupportedGrantTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class MissingGrantTypeException : UnsupportedGrantTypeException
    {
        public MissingGrantTypeException() : base(Constants.ErrorMessages.General.GrantTypeNotProvided)
        { }

        protected MissingGrantTypeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

}
