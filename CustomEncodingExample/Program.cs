using Qencode.Api.CSharp.Client;
using Qencode.Api.CSharp.Client.Classes;
using Qencode.Api.CSharp.Client.Classes.CustomParams;
using Qencode.Api.CSharp.Client.Exceptions;
using System;
using System.Threading;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var apiKey = "yourApiKey";
            var videoUrl = "https://someserver.com/video.mp4";
            var s3_path = "s3://s3-yourRegion.amazonaws.com/bucketname";
            var s3_key = "youS3Key";
            var s3_secret = "yourS3secret";

            var transcodingParams = new CustomTranscodingParams();
            transcodingParams.source = videoUrl;
            var format = new Format();
            format.destination = new Destination();
            format.destination.url = s3_path;
            format.destination.key = s3_key;
            format.destination.secret = s3_secret;
            format.output = "advanced_hls";

            var stream = new Stream();
            stream.size = "1920x1080";
            stream.audio_bitrate = 128;

            var vcodec_params = new Libx264_VideoCodecParameters();
            vcodec_params.vprofile = "baseline";
            vcodec_params.level = 31;
            vcodec_params.coder = 0;
            vcodec_params.flags2 = "-bpyramid+fastpskip-dct8x8";
            vcodec_params.partitions = "+parti8x8+parti4x4+partp8x8+partb8x8";
            vcodec_params.directpred = 2;
            stream.video_codec_parameters = vcodec_params;
            format.stream.Add(stream);

            transcodingParams.format.Add(format);

            try
            {
                var q = new QencodeApiClient(apiKey, "https://api-qa.qencode.com/");
                Console.WriteLine("Access token: " + q.AccessToken);

                var task = q.CreateTask();
                Console.WriteLine("Created new task: " + task.TaskToken);
                TranscodingTaskStatus response;
                var started = task.StartCustom(transcodingParams);
                Console.WriteLine("Status URL: " + started.status_url);
                do
                {
                    Thread.Sleep(5000);
                    Console.Write("Checking status... ");
                    response = task.GetStatus();
                    Console.WriteLine(String.Format("{0} - {1}%", response.status, response.percent.ToString("0.00")));
                } while (response.status != "completed");
                var video = response.videos[0];
                Console.WriteLine("Playlist url: " + video.url);
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
