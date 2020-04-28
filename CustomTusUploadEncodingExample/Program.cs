using Qencode.Api.CSharp.Client.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Qencode.Api.CSharp.Client;
using Qencode.Api.CSharp.Client.Classes.CustomParams;
using Qencode.Api.CSharp.Client.Exceptions;
using Stream = Qencode.Api.CSharp.Client.Classes.CustomParams.Stream;

namespace CustomTusUploadEncodingExample
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var apiKey = "your_api_key";
            var file = new FileInfo(@"your_file_path");
            
          
            

            try
            {
                var q = new QencodeApiClient(apiKey);
                TranscodingTask transcodingTask = q.CreateTask();
                Console.WriteLine("Access token: " + q.AccessToken);
                Console.WriteLine("Created new task: " + transcodingTask.TaskToken);
                var uploadUrl = $"{transcodingTask.UploadUrl}/{transcodingTask.TaskToken}";
                var res = await TusTools.UploadAsync(file, uploadUrl, 1000);
                var fileUrl = res.url;

                var transcodingParams = new CustomTranscodingParams();

                transcodingParams.source = fileUrl;
                var format = new Format();
                format.output = "mp4";
                transcodingParams.format.Add(format);
                var startedTask = transcodingTask.StartCustom(transcodingParams);
                Console.WriteLine("Status URL: " + startedTask.status_url);
                TranscodingTaskStatus response;
                do
                {
                    Thread.Sleep(5000);
                    Console.Write("Checking status... ");
                    response = transcodingTask.GetStatus();
                    Console.WriteLine(String.Format("{0} - {1}%", response.status,
                        response.percent == null ? "0" : ((float)response.percent).ToString("0.00")));
                } while (response.status != "completed");
                if (response.videos.Count > 0)
                {
                    var video = response.videos[0];
                    Console.WriteLine("Playlist url: " + video.url);
                }
                if (response.error > 0)
                {
                    Console.WriteLine(response.error_description);
                }
                Console.WriteLine("Done!");
                var statusUrl = startedTask.status_url;

               
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
