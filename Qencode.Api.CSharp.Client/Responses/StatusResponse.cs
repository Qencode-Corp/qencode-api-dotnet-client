using Qencode.Api.CSharp.Client.Classes;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Responses
{
    public class StatusResponse : QencodeApiResponse
    {
        /// <summary>
        /// Contains status for each task specified in query
        /// Dictinary key is task token
        /// </summary>
        public Dictionary<string, TranscodingTaskStatus> statuses;
    }
}
