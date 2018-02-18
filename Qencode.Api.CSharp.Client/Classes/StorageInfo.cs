using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes
{
    public class StorageInfo
    {
        /// <summary>
        /// Output video format
        /// </summary>
        public string format;

        /// <summary>
        /// Storage host or endpoint
        /// </summary>
        public string host;

        /// <summary>
        /// Video path on storage
        /// </summary>
        public string path;

        /// <summary>
        /// Storage type
        /// </summary>
        public string type;
    }
}
