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
            var apiKey = "5a5db6fa5b4c5";
            var transcodingProfile = "5a5db6fa5b8ac";
            var transferMethod = "5a68fa964f41a";
            var videoUrl = "https://qa.stagevids.com/static/1.mp4";
            try
            {
                var q = new QencodeApiClient(apiKey, "https://api-qa.qencode.com/");
                Console.WriteLine("Access token: " + q.AccessToken);

                var task = q.CreateTask();
                Console.WriteLine("Created new task: " + task.TaskToken);
                TranscodingTaskStatus response;
                var started = task.Start(transcodingProfile, videoUrl, transferMethod);
                Console.WriteLine("Status URL: " + started.status_url);
                do
                {
                    Thread.Sleep(5000);
                    Console.Write("Checking status... ");
                    response = task.GetStatus();
                    Console.WriteLine(String.Format("{0} - {1}%", response.status, response.percent.ToString("0.00")));
                } while (response.status != "completed");
                var video = response.videos[0];
                Console.WriteLine(video.user_tag + ": " + video.url);
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
