using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes
{
    public class TranscodingTaskStatus
    {
        /// <summary>
        /// Possible values of task status are listed below: 
        ///     downloading: Video is being downloaded to Qencode server.
        ///     queued: Task is waiting for available encoders.
        ///     encoding: Video is being transcoded.
        ///     saving: Video is being saved to destination location (e.g. Amazon S3 bucket).
        ///     completed: Video was saved to destination location. Transcoding job has completed successfully.
        /// </summary>
        public string status { get; set; }

        /// <summary>
        /// Percent of completion
        /// </summary>
        public float? percent { get; set; }

        /// <summary>
        /// Contains statuses for all output videos in a job
        /// </summary>
        public List<VideoStatus> videos { get; set; }
        public List<VideoStatus> audios { get; set; }

        /// <summary>
        /// Contains statuses for all thumbnails
        /// </summary>
        public List<ImageStatus> images { get; set; }

        /// <summary>
        /// 0 = OK, 1 = Error
        /// </summary>
        public int error { get; set; }

        /// <summary>
        /// Error description
        /// </summary>
        public string error_description { get; set; }

        public string payload;
    }
}
