using Newtonsoft.Json;
using Qencode.Api.CSharp.Client.Classes;
using Qencode.Api.CSharp.Client.Exceptions;
using Qencode.Api.CSharp.Client.Helpers;
using Qencode.Api.CSharp.Client.Responses;
using System;
using System.Collections.Generic;
using System.Net;

namespace Qencode.Api.CSharp.Client
{
    public class QencodeApiClient
    {
        private string key;
        /// <summary>
        /// Qencode Project API Key
        /// </summary>
        public string Key
        {
            get { return key; }
        }

        public QencodeApiClient() { }

        private string accessToken;
        /// <summary>
        /// Qencode API Access Token
        /// </summary>
        public string AccessToken
        {
            get { return accessToken; }
        }

        private string lastResponseRaw;

        private QencodeApiResponse lastResponse;

        private string url = @"https://api.qencode.com/";
        /// <summary>
        /// Qencode API Endpoint
        /// </summary>
        public string Url
        {
            get { return url; }
        }

        private string version = "v1";
        /// <summary>
        /// Qencode API version
        /// </summary>
        public string Version
        {
            get { return version; }
        }

        private const string USER_AGENT = "Qencode .NET API SDK 1.0";

        /**

         * TODO: Maximum amount of time in seconds that is allowed to make the connection to the API server

         */

        private uint connectTimeout = 20;
        public uint ConnectTimeout
        {
            get { return connectTimeout; }
            set { connectTimeout = value; }
        }

        /**

         * TODO: Maximum amount of time in seconds to which the execution of cURL call will be limited

         */

        private uint timeout = 20;
        public uint Timeout
        {
            get { return timeout; }
            set { timeout = value; }
        }

        /// <summary>
        /// Creates QencodeApiClient instance
        /// </summary>
        /// <param name="key">Qencode Project API Key</param>
        /// <param name="url">Optional url to any different API endpoint</param>
        public QencodeApiClient(string key, string url = null)
        {
            if (key.Length < 12)
            {
                throw new QencodeException("Missing or invalid Qencode project api key!");
            }

            if (!String.IsNullOrEmpty(url)) {
                //TODO: validate url
                /*if (!url_valid(url))
                {
                    throw new QencodeException("Invalid API endpoint url!");
                }*/

                this.url = url;
            }

            this.key = key;

            getAccessToken();
        }

        private void getAccessToken()
        {
            var response = Request<AccessTokenResponse>("access_token", 
                    new Dictionary<string, string>(){ { "api_key", key } }
                ) as AccessTokenResponse;
            accessToken = response.token;
        }

        /// <summary>
        /// Raw response data from the last request
        /// </summary>
        public string LastResponseRaw
        {
            get { return this.lastResponseRaw; }
        }

        /// <summary>
        /// Response data from the last request
        /// </summary>
        public QencodeApiResponse LastResponse
        {
            get { return lastResponse; }
        }

        /// <summary>
        /// Performs request to a specified API method
        /// </summary>
        /// <typeparam name="T">Response type</typeparam>
        /// <param name="path">API method path, e.g. "create_task"</param>
        /// <param name="parameters">Dictionary<string, string> OR raw string</param>
        public QencodeApiResponse Request<T>(string path, object parameters)
        { 
            lastResponseRaw = null;
            lastResponse = null;
            string requestUrl = null;
            if (path.ToLower().IndexOf("http") == 0)
            {
                requestUrl = path;
            }
            else
            {
                requestUrl = this.url + "/" + this.version + "/" + path.Trim('/');
            }

            if (parameters != null && ! (parameters is String))
            {
                parameters = QueryStringBuilder.BuildQueryString(parameters);
            }

            try
            {
                lastResponseRaw = HttpPost(requestUrl, parameters as string);
            }
            catch (Exception e)
            {
                throw new QencodeException("Error executing request to url: " + requestUrl, e);
            }

            var response = JsonConvert.DeserializeObject<T>(lastResponseRaw) as QencodeApiResponse;

            if (response == null || response.error == null)
            {
                throw new QencodeException("Invalid API response", lastResponseRaw);
            }

            if (response.error != 0)
            {
                throw new QencodeApiException(response.message);
            }

            return response;
        }

        /// <summary>
        /// Performs raw http post and reads response back
        /// </summary>
        /// <param name="uri">Request uri</param>
        /// <param name="parameters">x-www-form-urlencoded params</param>
        /// <returns>Http response</returns>
        private static string HttpPost(string uri, string parameters)
        {
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(uri);
            req.UserAgent = USER_AGENT;
            //req.Proxy = new System.Net.WebProxy(ProxyString, true);
            
            req.ContentType = "application/x-www-form-urlencoded";
            req.Method = "POST";
 
            byte[] bytes = System.Text.Encoding.ASCII.GetBytes(parameters);
            req.ContentLength = bytes.Length;
            System.IO.Stream os = req.GetRequestStream();
            os.Write(bytes, 0, bytes.Length); //Push it out there
            os.Close();
            System.Net.WebResponse resp = req.GetResponse();
            if (resp == null) return null;
            System.IO.StreamReader sr =
                  new System.IO.StreamReader(resp.GetResponseStream());
            return sr.ReadToEnd().Trim();
        }

        /// <summary>
        /// Creates new transcoding task
        /// </summary>
        public TranscodingTask CreateTask()
        {
            var response = this.Request<CreateTaskResponse>("create_task", new Dictionary<string, string>() {
                { "token", accessToken }
            }) as CreateTaskResponse; 

            return new TranscodingTask(this, response.task_token, response.upload_url);
        }
    }
}
