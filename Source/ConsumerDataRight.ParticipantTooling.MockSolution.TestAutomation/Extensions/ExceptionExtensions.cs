using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    public static class ExceptionExtensions
    {
        public static Exception Log(this Exception ex)
        {
            Serilog.Log.Error(ex.Message);
            return ex;
        }
    }
}
