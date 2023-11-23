using System;
using System.Net;
using System.Runtime.Serialization;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Enums;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions
{
    [Serializable]
    public class CdrException : Exception
    {
        private readonly string _code;
        private readonly string _title;
        private readonly string _detail;
        private readonly HttpStatusCode _statusCode;

        public CdrException(string code, string title, string detail, HttpStatusCode statusCode, string? message)
            : base(message)
        {
            _code = code;
            _title = title;
            _detail = detail;
            _statusCode = statusCode;
        }

        public CdrException(CdsError cdsError, string detail, HttpStatusCode statusCode, string? message)
            : base(message)
        {
            var errorInfo = cdsError.GetErrorInfo();
            _code = errorInfo.ErrorCode;
            _title = errorInfo.Title;
            _detail = detail;
            _statusCode = statusCode;
        }

        protected CdrException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public string Code { get => _code; }

        public string Title { get => _title; }

        public string Detail { get => _detail; }

        public HttpStatusCode StatusCode { get => _statusCode; }
    }
}
