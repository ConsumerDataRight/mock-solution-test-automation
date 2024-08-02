using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions.Execution;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    /// <summary>
    /// A custom assertion strategy used for Participant Tooling assertion scopes which is a close copy of CollectingAssertionStratgy, but required due to the protection level of that class being internal
    /// </summary>
    public class TestAssertionStrategy : IAssertionStrategy
    {
        private readonly List<string> _failureMessages = new List<string>();

        /// <summary>
        /// Returns the messages for the assertion failures that happened until now.
        /// </summary>
        public IEnumerable<string> FailureMessages => _failureMessages;

        /// <summary>
        /// Discards and returns the failure messages that happened up to now.
        /// </summary>
        public IEnumerable<string> DiscardFailures()
        {
            var discardedFailures = _failureMessages.ToArray();
            _failureMessages.Clear();
            return discardedFailures;
        }

        /// <summary>
        /// Will throw a combined exception for any failures have been collected.
        /// Some additional logging has been added for better reporting of issues
        /// </summary>
        public void ThrowIfAny(IDictionary<string, object> context)
        {
            if (_failureMessages.Count != 0)
            {
                var builder = new StringBuilder();
                builder.AppendJoin(Environment.NewLine, _failureMessages).AppendLine();

                Log.Warning("Test Assertion failed with these issues: {ISSUES}.", _failureMessages);

                if (context.Any())
                {
                    foreach (KeyValuePair<string, object> pair in context)
                    {
                        builder.AppendFormat(CultureInfo.InvariantCulture, "\nWith {0}:\n{1}", pair.Key, pair.Value);
                    }
                }

                FluentAssertions.Common.Services.ThrowException(builder.ToString());
            }
        }

        /// <summary>
        /// Instructs the strategy to handle a assertion failure.
        /// </summary>
        public void HandleFailure(string message)
        {
            _failureMessages.Add(message);
        }
    }
}
