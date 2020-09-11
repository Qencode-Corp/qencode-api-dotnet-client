using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using Newtonsoft.Json;
using Qencode.Api.CSharp.Client.Classes.CustomParams;
using Qencode.Api.CSharp.Client.Exceptions;

namespace Qencode.Api.CSharp.Client.Classes
{

    public class Metadata: TranscodingTask
    {
        private QencodeApiClient api;

        private string taskToken;

        public Metadata(QencodeApiClient api, string taskToken, string uploadUrl) : base(api, taskToken, uploadUrl)
        {
            this.api = api;
            this.taskToken = taskToken;
            this.uploadUrl = uploadUrl;
        }

        /// <summary
        /// Transcoding task token
        /// </summary>
        public string TaskToken
        {
            get { return taskToken; }
        }

       

        private string uploadUrl;
        /// <summary
        /// Endpoint url for direct uploads
        /// </summary>
        public string UploadUrl
        {
            get { return uploadUrl; }
        }

        public dynamic Get(string uploadUrl)
        {
          
            var format = new List<Dictionary<string, string>>() { new Dictionary<string, string>(){{ "output", "metadata" }, { "metadata_version", "4.1.5" } }};
            var query_json = JsonConvert.SerializeObject(format,
                Formatting.None,
                new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var prm = new Dictionary<string, object>(){ { "source", uploadUrl }, {"format", format } };
            StartCustom(prm);
            TranscodingTaskStatus response;
            do
            {
                Thread.Sleep(5000);
                Console.Write("Checking status... ");
                response = GetStatus();
                Console.WriteLine(String.Format("{0} - {1}%", response.status,
                    response.percent == null ? "0" : ((float)response.percent).ToString("0.00")));
            } while (response.status != "completed");

            var url = "";
            if (response.videos.Count > 0)
            {
                url = response.videos[0].url;
            }
            if (response.audios.Count > 0)
            {
                url = response.audios[0].url;
            }

            if (string.IsNullOrEmpty(url))
            {
                throw new QencodeApiException("No metadata URL found in status response");
            }

            return api.RequestDynamic<dynamic>(url);
        }

      
    }

   


}
