namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    using System.Net;
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

    public class ResourceNotFoundException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ResourceNotFoundException"/> class.
        /// <para>Status code: 404 (Not Found).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public ResourceNotFoundException(string detail, string message)
            : base(CdsError.ResourceNotFound, detail, HttpStatusCode.NotFound, message)
        {
        }

        public ResourceNotFoundException(string detail)
        : base(CdsError.ResourceNotFound, detail, HttpStatusCode.NotFound, null)
        {
        }

        public ResourceNotFoundException()
        : base(CdsError.ResourceNotFound, "The authorised consumer's consent is insufficient to execute the resource", HttpStatusCode.NotFound, null)
        {
        }
    }
}
