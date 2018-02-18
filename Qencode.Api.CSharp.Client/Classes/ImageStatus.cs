using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes
{
    public class ImageStatus
    {
        /// <summary>
        /// Resulting thumbnail url
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// System tag value (e.g. image-0-0)
        /// </summary>
        public string tag { get; set; }

        /// <summary>
        /// Contains info about where the thumbnail is stored
        /// </summary>
        public StorageInfo storage { get; set; }
    }
}
