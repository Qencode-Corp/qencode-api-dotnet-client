using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Responses
{
	public class CreateUploadTaskResponse : QencodeApiResponse
	{
		/// <summary>
		/// Upload location for this direct video upload
		/// </summary>
		/// <example>
		/// Location: https://storage.qencode.com/v1/upload_file/6c6a9b0a7d23cc9555d460269aa9ed56/fa6ca4b8f06f42daa5b0da04cc461dcb
		/// </example>
		public string Location { get; set; }
	}
}
