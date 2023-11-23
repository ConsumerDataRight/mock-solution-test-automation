﻿using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    [Serializable]
    public class MissingRequiredFieldException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MissingRequiredFieldException"/> class.
        /// <para>Status code: 400 (Bad Request).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public MissingRequiredFieldException(string detail, string message)
            : base(CdsError.MissingRequiredField, detail, System.Net.HttpStatusCode.BadRequest, message)
        { }

        public MissingRequiredFieldException(string detail)
          : base(CdsError.MissingRequiredField, detail, System.Net.HttpStatusCode.BadRequest, null)
        { }

        protected MissingRequiredFieldException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
