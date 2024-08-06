using System.Net.Http.Headers;
using System.Security.Claims;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Exceptions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions;
using ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Models;
using FluentAssertions;
using Newtonsoft.Json;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation
{
    public static class Assertions
    {

        /// <summary>
        /// Assert response content and expectedJson are equivalent
        /// </summary>
        /// <param name="expectedJson">The expected json</param>
        /// <param name="content">The response content</param>
        public static async Task AssertHasContentJson(string? expectedJson, HttpContent? content)
        {
            content.Should().NotBeNull(expectedJson ?? "");
            if (content == null)
            {
                return;
            }

            var actualJson = await content.ReadAsStringAsync();
            AssertJson(expectedJson, actualJson);
        }

        public static async Task AssertHasContentJson<T>(string? expectedJson, HttpContent? content)
        {
            content.Should().NotBeNull(expectedJson ?? "");
            if (content == null)
            {
                return;
            }

            var actualJson = await content.ReadAsStringAsync();
            AssertJson<T>(expectedJson, actualJson);
        }

        /// <summary>
        /// Assert response content is empty
        /// </summary>
        /// <param name="content">The response content</param>
        /// <param name="because">Reason the assertion is needed</param>
        public static async Task AssertHasNoContent(HttpContent? content, string? because = null)
        {
            content.Should().NotBeNull();
            if (content == null)
            {
                return;
            }

            var actualJson = await content.ReadAsStringAsync();
            actualJson.Should().BeNullOrEmpty(because);
        }

        /// <summary>
        /// Assert_HasNoContent because "No detail about response content in AC, check that API does not actually return any response content"
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public static async Task AssertHasNoContent2(HttpContent? content)
        {
            // Assert - No detail about response content in AC, check that API does not actually return any response content
            await AssertHasNoContent(content, "(Assert_HasNoContent2) AC does not specify response content and yet content is returned by API. Either AC needs to specify expected response content or API needs to return no content.");
        }

        /// <summary>
        /// Assert actual json is equivalent to expected json
        /// </summary>
        /// <param name="expectedJson">The expected json</param>
        /// <param name="actualJson">The actual json</param>
        public static void AssertJson(string? expectedJson, string actualJson)
        {
            AssertJson<object>(expectedJson, actualJson);            
        }

        public static void AssertJson<T>(string? expectedJson, string actualJson)
        {
            if (AssertIsJsonNullOrEmpty(expectedJson, actualJson))
            {
                return;
            }

            var expectedObject = JsonConvert.DeserializeObject<T>(expectedJson);
            expectedObject.Should().NotBeNull($"Error deserializing expected json - '{expectedJson}'");

            var actualObject = JsonConvert.DeserializeObject<T>(actualJson);
            actualObject.Should().NotBeNull($"Error deserializing actual json - '{actualJson}'");

            var expectedJsonNormalised = JsonConvert.SerializeObject(expectedObject);
            var actualJsonNormalised = JsonConvert.SerializeObject(actualObject);

            actualJson?.JsonCompare(expectedJson).Should().BeTrue(
                $"\r\nExpected json:\r\n{expectedJsonNormalised}\r\nActual Json:\r\n{actualJsonNormalised}\r\n"
            );
        }

        private static bool AssertIsJsonNullOrEmpty(string? expectedJson, string actualJson)
        {
            expectedJson.Should().NotBeNullOrEmpty();
            actualJson.Should().NotBeNullOrEmpty(expectedJson == null ? "" : $"expected {expectedJson}");

            if (string.IsNullOrEmpty(expectedJson) || string.IsNullOrEmpty(actualJson))
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// Assert headers has a single header with the expected value.
        /// If expectedValue then just check for the existence of the header (and not it's value)
        /// </summary>
        /// <param name="expectedValue">The expected header value</param>
        /// <param name="headers">The headers to check</param>
        /// <param name="name">Name of header to check</param>
        /// <param name="startsWith">Whether the header just needs to start with the expected value (opposed to matching exactly)</param>
        public static void AssertHasHeader(string? expectedValue, HttpHeaders headers, string name, bool startsWith = false)
        {
            headers.Should().NotBeNull();
            if (headers != null)
            {
                headers.Contains(name).Should().BeTrue($"name={name}");
                if (headers.Contains(name))
                {
                    var headerValues = headers.GetValues(name);
                    headerValues.Should().ContainSingle(name, $"name={name}");

                    if (expectedValue != null)
                    {
                        string headerValue = headerValues.First();

                        if (startsWith)
                        {
                            headerValue.Should().StartWith(expectedValue, $"name={name}");
                        }
                        else
                        {
                            headerValue.Should().Be(expectedValue, $"name={name}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Assert header has content type of ApplicationJson
        /// </summary>
        /// <param name="content"></param>
        public static void AssertHasContentTypeApplicationJson(HttpContent content)
        {
            content.Should().NotBeNull();
            content?.Headers.Should().NotBeNull();
            content?.Headers?.ContentType.Should().NotBeNull();
            content?.Headers?.ContentType?.ToString().Should().StartWith("application/json");
        }

        /// <summary>
        /// Assert claim exists
        /// </summary>
        public static void AssertClaim(IEnumerable<Claim> claims, string claimType, string claimValue)
        {
            claims.Should().NotBeNull();
            if (claims != null)
            {
                claims.FirstOrDefault(claim => claim.Type == claimType && claim.Value == claimValue).Should().NotBeNull($"Expected {claimType}={claimValue}");
            }
        }

        public static async Task AssertErrorAsync(HttpResponseMessage responseMessage, AuthoriseException expectedError)
        {
            responseMessage.StatusCode.Should().Be(expectedError.StatusCode);

            AssertHasContentTypeApplicationJson(responseMessage.Content);

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var receivedError = JsonConvert.DeserializeObject<AuthError>(responseContent);

            receivedError.Should().NotBeNull();
            if (receivedError != null)
            {
                receivedError.Description.Should().Be(expectedError.ErrorDescription);
                receivedError.Code.Should().Be(expectedError.Error);
            }
        }
        public static async Task AssertErrorAsync(HttpResponseMessage responseMessage, CdrException expectedError)
        {
            responseMessage.StatusCode.Should().Be(expectedError.StatusCode);

            AssertHasContentTypeApplicationJson(responseMessage.Content);

            var responseContent = await responseMessage.Content.ReadAsStringAsync();
            var receivedError = JsonConvert.DeserializeObject<AuthError>(responseContent);

            receivedError.Should().NotBeNull();
            if (receivedError != null)
            {
                receivedError.Description.Should().Be(expectedError.Detail);
                receivedError.Code.Should().Be(expectedError.Code);
            }
        }
    }
}
