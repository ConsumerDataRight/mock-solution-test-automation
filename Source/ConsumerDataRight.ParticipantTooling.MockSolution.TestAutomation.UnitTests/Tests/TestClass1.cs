using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UnitTests.Tests
{
    [Trait("Category", "UnitTests")]
    public class TestClass1
    {
        public class Startup
        {
            // A default startup is required due to the test project inheriting Xunit.DependencyInjection from the Nuget project. 
            public void ConfigureServices(IServiceCollection services)
            {
                // Method intentionally left empty.
            }
        }

        [Fact]
        public void Test1()
        {
            var num = 1;
            num.Should().Be(1);
        }
    }
}