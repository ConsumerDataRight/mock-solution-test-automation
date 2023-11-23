using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Serilog;

namespace ConsumerDataRight.ParticipantTooling.MockSolution.TestAutomation.Extensions
{
    public static class JsonExtensions
    {
        public static void WriteJsonToFile(string filename, string json)
        {
            Log.Information("Calling {FunctionName} with Params: {P1}={V1},{P2}={V2}", nameof(WriteJsonToFile), nameof(filename), filename, nameof(json), json);  
            //TODO: Expect this is the System.Text.Json.JsonSerializer way to replace Newtonsoft, if we decide to do it (model property attributes would need to change)
            var options = new JsonSerializerOptions
            {
                WriteIndented = true
            };

            var jsonObj = System.Text.Json.JsonSerializer.Deserialize<object>(json);
            var jsonStr = System.Text.Json.JsonSerializer.Serialize(jsonObj, options);

            //var jsonObj = JsonConvert.DeserializeObject(json);
            //var jsonStr = JsonConvert.SerializeObject(jsonObj, Formatting.Indented);
            File.WriteAllText(filename, jsonStr);
        }

        /// <summary>
        /// Strip comments from json string. 
        /// The json will be reserialized so it's formatting may change (ie whitespace/indentation etc)
        /// </summary>
        public static string JsonStripComments(this string json)
        {
            var options = new JsonSerializerOptions
            {
                ReadCommentHandling = JsonCommentHandling.Skip,
                WriteIndented = true
            };

            var jsonObject = System.Text.Json.JsonSerializer.Deserialize<object>(json, options);

            return System.Text.Json.JsonSerializer.Serialize(jsonObject);
        }

        /// <summary>
        /// Compare json. 
        /// Json is converted to JTokens prior to comparision, thus formatting is ignore.
        /// Returns true if json is equivalent, otherwise false.
        /// </summary>
        public static bool JsonCompare(this string json, string jsonToCompare)
        {
            Log.Information("Calling {FunctionName} with Params: {P1}={V1},{P2}={V2}", nameof(JsonCompare), nameof(json), json, nameof(jsonToCompare), jsonToCompare);

            var jsonToken = JToken.Parse(json);
            var jsonToCompareToken = JToken.Parse(jsonToCompare);
            return JToken.DeepEquals(jsonToken, jsonToCompareToken);
        }

        public static async Task<T?> DeserializeResponseAsync<T>(HttpResponseMessage response)
        {
            var responseContent = await response.Content.ReadAsStringAsync();
            
            if (string.IsNullOrEmpty(responseContent))
            {
                return default(T);
            }

            return JsonConvert.DeserializeObject<T>(responseContent);
        }
    }
}
