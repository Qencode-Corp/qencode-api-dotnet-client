using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes.CustomParams
{
    public class Destination
    {
        /// <summary>
        /// Destination bucket url, e.g. s3://example.com/bucket
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Access key
        /// </summary>
        public string key { get; set; }

        /// <summary>
        /// Access secret
        /// </summary>
        public string secret { get; set; }

        /// <summary>
        /// Permissions (S3 only)
        /// </summary>
        public string permissions { get; set; }

    }
    
}
