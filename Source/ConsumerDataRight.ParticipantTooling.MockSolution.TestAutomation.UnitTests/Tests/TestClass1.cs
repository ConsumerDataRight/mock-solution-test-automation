using FluentAssertions;
using Xunit;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.UnitTests.Tests
{
    [Trait("Category", "UnitTests")]
    public class TestClass1
    {
        [Fact]
        public void Test1()
        {
            var num = 1;
            num.Should().Be(1);
        }
    }
}