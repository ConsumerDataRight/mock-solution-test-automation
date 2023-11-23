using System.Runtime.Serialization;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.AuthoriseExceptions
{
    [Serializable]
    public class InvalidClientMetadataException : AuthoriseException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidClientMetadataException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        public InvalidClientMetadataException(string detail)
          : base(string.Empty, System.Net.HttpStatusCode.BadRequest, "invalid_client_metadata", detail)
        { }

        protected InvalidClientMetadataException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class AuthorizationSignedResponseAlgClaimInvalidException : InvalidClientMetadataException
    {
        public AuthorizationSignedResponseAlgClaimInvalidException() : base("The 'authorization_signed_response_alg' claim value must be one of 'PS256,ES256'.")//TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected AuthorizationSignedResponseAlgClaimInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class AuthorizationSignedResponseAlgClaimMissingException : InvalidClientMetadataException
    {
        public AuthorizationSignedResponseAlgClaimMissingException() : base("The 'authorization_signed_response_alg' claim is missing.")//TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected AuthorizationSignedResponseAlgClaimMissingException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class TokenEndpointAuthSigningAlgClaimInvalidException : InvalidClientMetadataException
    {
        public TokenEndpointAuthSigningAlgClaimInvalidException() : base("The 'token_endpoint_auth_signing_alg' claim value must be one of 'PS256,ES256'.")//TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected TokenEndpointAuthSigningAlgClaimInvalidException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class DuplicateRegistrationForSoftwareIdException : InvalidClientMetadataException
    {
        public DuplicateRegistrationForSoftwareIdException() : base(Constants.ErrorMessages.Dcr.DuplicateRegistration)
        { }

        protected DuplicateRegistrationForSoftwareIdException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }

    [Serializable]
    public class GrantTypesMissingAuthorizationCodeException : InvalidClientMetadataException
    {
        public GrantTypesMissingAuthorizationCodeException() : base("The 'grant_types' claim value must contain the 'authorization_code' value.")//TODO: Should a error gen code and add to constants. Bug 64146
        { }

        protected GrantTypesMissingAuthorizationCodeException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
