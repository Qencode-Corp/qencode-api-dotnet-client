using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes
{
    public class VideoStatus
    {
        /// <summary>
        /// Resulting video url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// User tag value
        /// </summary>
        public string user_tag { get; set; }

        /// <summary>
        /// Transcoding profile ID (Is empty for custom transcoding jobs)
        /// </summary>
        public string profile { get; set; }

        /// <summary>
        /// System tag value (e.g. video-0-0)
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// Contains info about where the output video is stored
        /// </summary>
        public StorageInfo storage { get; set; }

    }
}
