using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes
{
	/// <summary>
	/// This class describe a TUS upload task
	/// </summary>
	public class UploadTask
	{

		private QencodeApiClient api;

		private string taskToken;
		/// <summary
		/// Transcoding task token
		/// </summary>
		public string TaskToken
		{
			get { return taskToken; }
		}

		/// <summary>
		/// Output path variables map (used to set transcoding profile output path placeholder values)s
		/// </summary>
		public Dictionary<string, string> OutputPathVariables { get; }

		public UploadTask(QencodeApiClient api, string taskToken, string upload_location, string file_fullName, long file_size, string file_sha1)
		{
			this.api = api;
			this.taskToken = taskToken;
			this.upload_location = upload_location;
			this.file_fullName = file_fullName;
			this.file_size = file_size;
			this.file_sha1 = file_sha1;
			OutputPathVariables = new Dictionary<string, string>();
		}

		public int error { get; set; }
		/// <summary>
		/// Upload url location for specified file.
		/// </summary>
		/// <example>
		/// Location: https://storage.qencode.com/v1/upload_file/6c6a9b0a7d23cc9555d460269aa9ed56/fa6ca4b8f06f42daa5b0da04cc461dcb
		/// (where fa6ca4b8f06f42daa5b0da04cc461dcb is file_uuid)
		/// </example>
		public string upload_location { get; private set; }
		public string file_fullName { get; private set; }
		public long file_size { get; private set; }
		public string file_sha1 { get; private set; }
		/// <summary>
		/// Error description
		/// </summary>
		public string error_description { get; set; }

		public string payload;
	}
}
