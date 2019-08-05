using System;
using System.IO;
using Qencode.Api.CSharp.Client.Classes;
using Newtonsoft.Json;
using JsonDiffPatchDotNet;
using Newtonsoft.Json.Linq;

namespace CustomTranscodingParamsTest
{
    class Program
    {
        static string regressionTestsPath = "../../regression-tests";

        static void Main(string[] args)
        {
            Console.WriteLine("Reading regression tests...");
            var jdp = new JsonDiffPatch();
            var tests = Directory.GetDirectories(regressionTestsPath);
            foreach (var test in tests)
            {
                if (!File.Exists(test + "/query.json"))
                {
                    Console.WriteLine("Skipped: " + test);
                    continue;
                }
                Console.WriteLine(test);
                var originalJson = File.ReadAllText(test + "/query.json");
                var transcodingParams = CustomTranscodingParams.FromJSON(originalJson);
                var serialized = JsonConvert.SerializeObject(transcodingParams,
                    Formatting.None,
                    new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                var left = JToken.Parse(originalJson);
                var right = JToken.Parse("{ \"query\": " + serialized + " }");
                var patch = jdp.Diff(left, right);
                if (patch != null)
                {
                    Console.WriteLine("FAILED!");
                    Console.WriteLine(patch);
                }
                else
                {
                    Console.WriteLine("SUCCESS!");
                }
            }
            Console.ReadKey();
        }
    }
}
