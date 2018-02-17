using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Responses
{
    public class StartEncodeResponse : QencodeApiResponse
    {
        /// <summary>
        /// Url to check task status at
        /// </summary>
        public string status_url { get; set; }
    }
}
