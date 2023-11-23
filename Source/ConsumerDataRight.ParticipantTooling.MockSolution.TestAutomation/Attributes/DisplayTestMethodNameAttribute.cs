using System.Reflection;
using Serilog;
using Xunit.Sdk;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Attributes
{
    public class DisplayTestMethodNameAttribute : BeforeAfterTestAttribute
    {
        private int _count = 0;

        public override void Before(MethodInfo methodUnderTest)
        {
            Log.Logger.Information("-----------------------------------------------------");
            Log.Logger.Information("--Test #{count} - {TestClassName}.{TestName}", ++_count, methodUnderTest.DeclaringType?.Name, methodUnderTest.Name);
        }

        public override void After(MethodInfo methodUnderTest)
        {
            Log.Logger.Information("--Test complete - {TestClassName}.{TestName}", methodUnderTest.DeclaringType?.Name, methodUnderTest.Name);
        }
    }
}
