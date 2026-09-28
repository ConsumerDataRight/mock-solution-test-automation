namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Attributes;
    using FluentAssertions.Execution;
    using Microsoft.Extensions.Configuration;
    using Serilog;
    using Serilog.Extensions.Hosting;
    using Serilog.Sinks.XUnit3;
    using Xunit;

    [DisplayTestMethodName]
    abstract public class SharedBaseTest
    {
        public IAssertionStrategy BaseTestAssertionStrategy { get; init; }

        protected SharedBaseTest(IConfiguration config)
        {
            BaseTestAssertionStrategy = new TestAssertionStrategy();

            // Will only reload the first time, as it is frozen after that
            ((ReloadableLogger)Log.Logger).Reload(lc =>
            {
                return lc
                  .ReadFrom.Configuration(config)
                  .WriteTo.XUnit3TestOutput(new XUnit3TestOutputSink(new XUnit3TestOutputSinkOptions())
                  {
                      TestOutputHelper = TestContext.Current.TestOutputHelper,
                  });
            });

            try
            {
                // Workaround as we can't tell if the Logger has been frozen, but it will fail if we try to freeze it
                ((ReloadableLogger)Log.Logger).Freeze();
            }
            catch (Exception)
            {
                // No need to add to log as the reconfiguration wasn't needde
                return;
            }

            Log.Information("-Logger has been reconfigured to also write to TestOutput.-");
        }
    }
}
