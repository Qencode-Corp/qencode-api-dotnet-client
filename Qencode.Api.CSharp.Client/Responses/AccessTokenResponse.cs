using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Responses
{
    public class AccessTokenResponse : QencodeApiResponse
    {
        public string token { get; set; }
        public DateTime expire { get; set; }     
    }
}
