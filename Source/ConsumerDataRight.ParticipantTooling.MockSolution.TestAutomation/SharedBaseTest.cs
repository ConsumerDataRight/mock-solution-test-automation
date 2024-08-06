using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Attributes;
using FluentAssertions.Execution;
using Microsoft.Extensions.Configuration;
using Serilog;
using Serilog.Extensions.Hosting;
using Xunit.DependencyInjection;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    [DisplayTestMethodName]
    abstract public class SharedBaseTest
    {
        public IAssertionStrategy BaseTestAssertionStrategy { get; init; }
        protected SharedBaseTest(ITestOutputHelperAccessor testOutputHelperAccessor, IConfiguration config)
        {
            BaseTestAssertionStrategy = new TestAssertionStrategy();

            //Will only reload the first time, as it is frozen after that
            ((ReloadableLogger)Log.Logger).Reload(lc =>
            {
                return lc
                  .ReadFrom.Configuration(config)
                  .WriteTo.TestOutput(testOutputHelperAccessor.Output);
            });

            try
            {
                //Workaround as we can't tell if the Logger has been frozen, but it will fail if we try to freeze it 
                ((ReloadableLogger)Log.Logger).Freeze();
            }
            catch (Exception)
            {
                //No need to add to log as the reconfiguration wasn't needde
                return;
            }

            Log.Information("-Logger has been reconfigured to also write to TestOutput.-");
        }
    }
}
