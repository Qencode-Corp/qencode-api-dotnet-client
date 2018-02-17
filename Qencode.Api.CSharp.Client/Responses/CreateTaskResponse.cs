using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Responses
{
    public class CreateTaskResponse : QencodeApiResponse
    {
        public string task_token { get; set; }

        /// <summary>
        /// Url for direct video upload using tus.io protocol 
        /// (currently not supported with this library)
        /// </summary>
        public string upload_url { get; set; }
    }
}
