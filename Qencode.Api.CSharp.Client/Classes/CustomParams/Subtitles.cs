using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes.CustomParams
{
    public class Subtitles
    {
        /// <summary>
        /// Contains a list of elements each describing a single view stream.
        /// Use this for HLS or DASH outputs only. In this case you should set size, bitrate, etc. on stream level.
        /// </summary>
        public List<Source> sources { get; set; }
    }
}
