using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Exceptions
{
    public class QencodeApiException : QencodeException
    {
        public QencodeApiException() : base() { }
        public QencodeApiException(string message) : base(message) { }
        public QencodeApiException(string message, Exception inner) : base(message, inner) { }

        public QencodeApiException(string message, string rawResponse) : base(message, rawResponse) { }
    }
}
