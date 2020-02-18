using Qencode.Api.CSharp.Client;
using Qencode.Api.CSharp.Client.Classes;
using Qencode.Api.CSharp.Client.Exceptions;
using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Cryptography;
using System.Threading;
using TusDotNetClient;

namespace CustomEncodingJsonTUSUploadExample
{
	class Program
	{
		/*
		 * This example upload a local file,
		 * transcode it and then,
		 * download the transcoded file
		 */

		static void Main(string[] args)
		{
			// You can find your API key under Project settings in your Dashboard on Qencode portal
			const string apiKey = "YOUR_API_KEY_HERE";
			const string relative_output_dir = "TranscodedOutput"; // relative output dir

			// This is the full file name of the source video
			string sourceVideoFullFileName = "E:\\dev\\My\\Sample720.flv";
			// if an argument is specified, the source video file will be readed from the first argument
			if (args.Length >= 1 && !string.IsNullOrEmpty(args[0]))
				sourceVideoFullFileName = args[0];

			try
			{

				// get access token
				Console.WriteLine("Requesting access token...");
				var q = new QencodeApiClient(apiKey);
				Console.WriteLine("\taccess token: '" + q.AccessToken + "'");

				// create a new task
				Console.WriteLine("Creating a new task...");
				var task = q.CreateTask();
				Console.WriteLine("\tcreated new task with token: '" + task.TaskToken + "' and url for direct video upload (TUS) '" + task.UploadUrl + "'");

				// direct video upload - initiate upload (get endpoint for task)
				Console.WriteLine("Initiate upload...");
				var srcFI = new FileInfo(sourceVideoFullFileName);
				var client = new TusClient();
				var tusUploadLocationUrl = client.CreateAsync(task.UploadUrl + "/" + task.TaskToken, srcFI.Length).Result;
				Console.WriteLine("\tobtained TUS upload location: '" + tusUploadLocationUrl + "'");

				// direct video upload - send data
				var uploadOperation = client.UploadAsync(tusUploadLocationUrl, srcFI);
				Console.WriteLine("\ttransfer started");
				uploadOperation.Progressed += (transferred, total) =>
				{
					Console.CursorLeft = 0;
					Console.Write($"Progress: {transferred}/{total}");
				};
				Console.WriteLine();
				Console.WriteLine();
				uploadOperation.GetAwaiter().GetResult();
				Console.WriteLine("\tupload done");

				// define a custom task by reading query.json and filling the ##TUS_FILE_UUID## placeholder
				var tusFileUUID = tusUploadLocationUrl.Substring(tusUploadLocationUrl.LastIndexOf('/') + 1);
				var customTrascodingJSON = File.ReadAllText("query.json").Replace("##TUS_FILE_UUID##", tusFileUUID);
				var customTranscodingParams = CustomTranscodingParams.FromJSON(customTrascodingJSON);

				// start a custom task
				Console.WriteLine("Custom task starting..");
				Console.WriteLine(customTrascodingJSON);

				// start a custom task - set event handler
				bool taskCompletedOrError = false;
				task.TaskCompleted = new RunWorkerCompletedEventHandler(
					delegate (object o, RunWorkerCompletedEventArgs e)
					{
						if (e.Error != null)
						{
							taskCompletedOrError = true;
							Console.WriteLine("Error: ", e.Error.Message);
						}

						var response = e.Result as TranscodingTaskStatus;
						if (response.error == 1)
						{
							taskCompletedOrError = true;
							Console.WriteLine("Error: " + response.error_description);
						}
						else if (response.status == "completed")
						{
							taskCompletedOrError = true;
							Console.WriteLine("Video urls: ");
							foreach (var video in response.videos)
								Console.WriteLine(video.url);
						}
						else
						{
							Console.WriteLine(response.status);
						}
					});
				// start a custom task - start
				Console.WriteLine("\tstarting...");
				var started = task.StartCustom(customTranscodingParams); // starts and poll 

				// waiting
				Console.WriteLine("Waiting..");
				while (!taskCompletedOrError)
					Thread.Sleep(1000);

				// get download url
				if (task.LastStatus == null)
					throw new InvalidOperationException("Unable to obtain download url");
				var outputDownloadUrl = new Uri(task.LastStatus.videos.First().url);
				Console.WriteLine("Output download url: '" + outputDownloadUrl.ToString() + "'");
				string output_file_name = GetOutputFileName(sourceVideoFullFileName, outputDownloadUrl.Segments.Last());
				Console.WriteLine("Output file name: '" + output_file_name + "'");


				// download
				Console.WriteLine("Downloading..");
				HttpFileDownload(outputDownloadUrl, relative_output_dir, output_file_name);
				Console.WriteLine("\tdownload done");
				Environment.Exit(0);

			}
			catch (QencodeApiException e)
			{
				Console.WriteLine("Qencode API exception: " + e.Message);
				Environment.Exit(-1);
			}
			catch (QencodeException e)
			{
				Console.WriteLine("API call failed: " + e.Message);
				Environment.Exit(-1);
			}
		}

		private static string GetOutputFileName(string inputFullFileName, string outputProposedFileName)
		{
			var inputFI = new FileInfo(inputFullFileName);
			var ouputFI = new FileInfo(outputProposedFileName);
			return inputFI.Name + ouputFI.Extension;
		}

		private static void HttpFileDownload(Uri url, string localFolder, string localFileName)
		{
			if (!Directory.Exists($"./{localFolder}"))
				Directory.CreateDirectory($"./{localFolder}");

			var relativeFileName = $"./{localFolder}/{localFileName}";

			if (File.Exists(relativeFileName))
			{
				File.Delete(relativeFileName);
				Console.WriteLine("\toutput file already existing: deleted");
			}

			using (var wc = new System.Net.WebClient())
				wc.DownloadFile(url, relativeFileName);

		}


	}
}
