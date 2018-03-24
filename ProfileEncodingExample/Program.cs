using Qencode.Api.CSharp.Client;
using Qencode.Api.CSharp.Client.Classes;
using Qencode.Api.CSharp.Client.Exceptions;
using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "your_api_key";
            var transcodingProfile = "your_transcoding_profile_id";
            var transferMethod = "your_transfer_method_id";
            var videoUrl = "https://qa.stagevids.com/static/test_mini.mp4";
            try
            {
                var q = new QencodeApiClient(apiKey);
                Console.WriteLine("Access token: " + q.AccessToken);

                var task = q.CreateTask();
                task.StartTime = 60.015;
                task.Duration = 10.575;
                Console.WriteLine("Created new task: " + task.TaskToken);
                TranscodingTaskStatus response;
                var started = task.Start(transcodingProfile, videoUrl, transferMethod);
                Console.WriteLine("Status URL: " + started.status_url);
                do
                {
                    Thread.Sleep(5000);
                    Console.Write("Checking status... ");
                    response = task.GetStatus();
                    Console.WriteLine(String.Format("{0} - {1}%", response.status,
                        response.percent == null ? "0" : ((float)response.percent).ToString("0.00")));
                } while (response.status != "completed" && response.error != 1);
                if (response.status == "completed")
                {
                    var video = response.videos[0];
                    Console.WriteLine(video.user_tag + ": " + video.url);
                }
                else
                {
                    Console.WriteLine("Error: " + response.error_description);
                }
                Console.WriteLine("Done!");
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
