using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes
{
    public class UploadStatus
    {
        public UploadStatus()
        {
            errors = new List<string>();
        }
        /// <summary>
        /// Resulting video url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Count Error upload video
        /// </summary>
        public int countError => errors.Count;

        /// <summary>
        /// List errors upload video
        /// </summary>
        public List<string> errors { get; set; }
        /// <summary>
        /// Status upload
        /// </summary>
        public string status { get; set; }
    }
}
