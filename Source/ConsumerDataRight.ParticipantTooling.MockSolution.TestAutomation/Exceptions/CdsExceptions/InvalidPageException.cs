﻿using System.Net;
using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    [Serializable]
    public class InvalidPageException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidPageException"/> class.
        /// <para>Status code: 422 (Unprocessable Entity).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidPageException(string detail, string message)
            : base(CdsError.InvalidPage, detail, HttpStatusCode.UnprocessableEntity, message)
        { }

        public InvalidPageException(string detail)
        : base(CdsError.InvalidPage, detail, HttpStatusCode.UnprocessableEntity, null)
        { }

        protected InvalidPageException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
