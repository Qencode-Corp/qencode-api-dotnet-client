using Qencode.Api.CSharp.Client;
using Qencode.Api.CSharp.Client.Classes;
using Qencode.Api.CSharp.Client.Exceptions;
using System;
using System.ComponentModel;
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
                task.ProgressChanged = new ProgressChangedEventHandler(
                delegate (object o, ProgressChangedEventArgs e)
                {
                    Console.WriteLine(string.Format("{0}% completed", e.ProgressPercentage));
                });

                task.TaskCompleted = new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs e)
                {
                    if (e.Error != null)
                    {
                        Console.WriteLine("Error: ", e.Error.Message);
                    }
                    
                    var response = e.Result as TranscodingTaskStatus;
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
                });
               
                var started = task.Start(transcodingProfile, videoUrl, transferMethod);
                Console.WriteLine("Status URL: " + started.status_url);
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
