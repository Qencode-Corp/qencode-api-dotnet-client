using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes.CustomParams
{
    public class Format
    {
        /// <summary>
        /// Output video format. Currently supported values are mp4, webm, advanced_hls, advanced_dash. Required.
        /// </summary>
        public string output { get; set; }

        /// <summary>
        /// Output video file extension (for MP4 - defaults to '.mp4', for WEBM - defaults to '.webm').
        /// </summary>
        public string file_extension { get; set; }

        /// <summary>
        /// URI to store output video in. In case this value is not specified, video is temporarily stored on Qencode servers.
        /// </summary>
        public Destination destination { get; set; }

        /// <summary>
        /// Segment duration to split media (in seconds). Defaults to 8.
        /// </summary>
        //public int? segment_duration { get; set; }

        /// <summary>
        /// Contains a list of elements each describing a single view stream (e.g. for HLS format).
        /// </summary>
        public List<Stream> stream { get; set; }

        public Format()
        {
            stream = new List<Stream>();
        }
    }
}
