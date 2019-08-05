using Qencode.Api.CSharp.Client;
using Qencode.Api.CSharp.Client.Classes;
using Qencode.Api.CSharp.Client.Exceptions;
using System;
using System.ComponentModel;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // You can find your API key under Project settings in your Dashboard on Qencode portal
            var apiKey = "your_api_key";

            // This loads API query from file. 
            // Make sure you change source video url and destination params to some actual values.
            var transcodingParams = CustomTranscodingParams.FromFile("query.json");
            
            try
            {
                var q = new QencodeApiClient(apiKey);
                Console.WriteLine("Access token: " + q.AccessToken);

                var task = q.CreateTask();
                Console.WriteLine("Created new task: " + task.TaskToken);
                task.TaskCompleted = new RunWorkerCompletedEventHandler(
                delegate (object o, RunWorkerCompletedEventArgs e)
                {
                    if (e.Error != null)
                    {
                        Console.WriteLine("Error: ", e.Error.Message);
                    }

                    var response = e.Result as TranscodingTaskStatus;
                    if (response.error == 1)
                    {
                        Console.WriteLine("Error: " + response.error_description);
                    }
                    else if (response.status == "completed")
                    {
                        Console.WriteLine("Video urls: ");
                        foreach (var video in response.videos)
                        {
                            Console.WriteLine(video.url);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.status);
                    }
                    Console.WriteLine("Done!");
                });

                var started = task.StartCustom(transcodingParams);
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
