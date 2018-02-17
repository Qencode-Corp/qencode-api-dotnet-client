using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Exceptions
{
    [Serializable]
    public class QencodeException : Exception
    {
        public QencodeException() : base() { }
        public QencodeException(string message) : base (message) { }
        public QencodeException(string message, Exception inner) : base(message, inner) { }

        public QencodeException(string message, string rawResponse) : base(message)
        {
            RawResponse = rawResponse;
        }

        /// <summary>
        /// Last response from API that triggered this exception
        /// </summary>
        public string RawResponse { get; set; }
    }
}
