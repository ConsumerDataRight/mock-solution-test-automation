﻿using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions.CdsExceptions
{
    [Serializable]
    public class InvalidEnergyAccountException : CdrException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidEnergyAccountException"/> class.
        /// <para>Status code: 404 (Not Found).</para>
        /// </summary>
        /// <param name="detail"></param>
        /// <param name="message"></param>
        public InvalidEnergyAccountException(string detail, string message)
            : base(CdsError.InvalidEnergyAccount, detail, System.Net.HttpStatusCode.NotFound, message)
        { }

        public InvalidEnergyAccountException(string detail)
          : base(CdsError.InvalidEnergyAccount, detail, System.Net.HttpStatusCode.NotFound, null)
        { }

        protected InvalidEnergyAccountException(SerializationInfo info, StreamingContext context) : base(info, context)
        { }
    }
}
