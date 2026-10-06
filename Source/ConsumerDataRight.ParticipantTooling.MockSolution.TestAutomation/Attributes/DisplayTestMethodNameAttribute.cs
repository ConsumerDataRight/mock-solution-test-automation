namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Attributes
{
    using System.Reflection;
    using Serilog;
    using Xunit.v3;

    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = true)]
    public class DisplayTestMethodNameAttribute : BeforeAfterTestAttribute
    {
        private int _count = 0;

        public override void Before(MethodInfo methodUnderTest, IXunitTest test)
        {
            Log.Logger.Information("-----------------------------------------------------");
            Log.Logger.Information("--Test #{Count} - {TestClassName}.{TestName}", ++_count, methodUnderTest.DeclaringType?.Name, methodUnderTest.Name);
        }

        public override void After(MethodInfo methodUnderTest, IXunitTest test)
        {
            Log.Logger.Information("--Test complete - {TestClassName}.{TestName}", methodUnderTest.DeclaringType?.Name, methodUnderTest.Name);
        }
    }
}
