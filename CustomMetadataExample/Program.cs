using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.SqlServer.Server;
using Qencode.Api.CSharp.Client;
using Qencode.Api.CSharp.Client.Classes;
using Qencode.Api.CSharp.Client.Exceptions;

namespace CustomMetadataExample
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "5a2a846a26ace";
            var videoUrl = "https://nyc3.s3.qencode.com/qencode/bbb_30s.mp4";
            var s3_path = "s3://s3-eu-west-2.amazonaws.com/qencode-test";
            var s3_key = "your_s3_key";
            var s3_secret = "your_s3_secret";

            try
            {
                var q = new QencodeApiClient(apiKey);
                Console.WriteLine("Access token: " + q.AccessToken);

                var task = q.GetMetadata(videoUrl);
                Console.WriteLine(task);
            }
            catch (QencodeApiException e)
            {
                Console.WriteLine("Qencode API exception: " + e.Message);
            }
            catch (QencodeException e)
            {
                Console.WriteLine("API call failed: " + e.Message);
            }
            Console.ReadKey();
        }
    }
}
