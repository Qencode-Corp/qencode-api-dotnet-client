using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Qencode.Api.CSharp.Client.Helpers;
using TusDotNetClient;

namespace Qencode.Api.CSharp.Client.Classes
{
    public static class TusTools
    {

        /// <summary>
        /// Upload a file to the Tus server.
        /// </summary>
        /// <param name="file">>The file to upload.</param>
        /// <param name="url">URL to a previously created file.</param>
        /// <param name="chunkSize">The size, in megabytes, of each file chunk when uploading.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation with.</param>
        /// <returns></returns>
        public static async Task<UploadStatus> UploadAsync(
            FileInfo file, 
            string url, 
            double chunkSize = 5.0,
            CancellationToken cancellationToken = default)
        {
            var result = new UploadStatus();
            try
            {
                var tusClient = new TusClient();
                var fileUrl = await tusClient.CreateAsync(url, file.Length);
                var uploadOperation = tusClient.UploadAsync(fileUrl, file, chunkSize, cancellationToken);
                await uploadOperation;
                result.status = "Upload success";
                result.url = StringParser.ConvertSourceToTus(fileUrl);
                return result;
            }
            catch (Exception e)
            {
                result.errors.Add(e.ToString());
                result.status = "Upload error";
                return result;
            }
        }

        /// <summary>
        /// Upload a file to the Tus server.
        /// </summary>
        /// <param name="url">URL to a previously created file.</param>
        /// <param name="fileStream">A file stream of the file to upload. The stream will be closed automatically.</param>
        /// <param name="chunkSize">The size, in megabytes, of each file chunk when uploading.</param>
        /// <param name="cancellationToken">A cancellation token to cancel the operation with.</param>
        /// <returns>A <see cref="UploadStatus"/> which represents the upload operation.</returns>
        public static async Task<UploadStatus> UploadAsync(
            string url,
            Stream fileStream,
            double chunkSize = 5.0,
            CancellationToken cancellationToken = default)
        {
            var result = new UploadStatus();
            try
            {
                var tusClient = new TusClient();
                var fileUrl = await tusClient.CreateAsync(url, fileStream.Length);
                var uploadOperation = tusClient.UploadAsync(fileUrl, fileStream, chunkSize, cancellationToken);
                await uploadOperation;
                result.status = "Upload success";
                result.url = StringParser.ConvertSourceToTus(fileUrl);
                return result;
            }
            catch (Exception e)
            {
                result.errors.Add(e.ToString());
                result.status = "Upload error";
                return result;
            }
        }



    }
}
