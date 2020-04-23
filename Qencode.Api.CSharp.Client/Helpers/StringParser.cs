using System;
using System.Linq;


namespace Qencode.Api.CSharp.Client.Helpers
{
    public static class StringParser
    {
        private static string pattern => @"([^/]+$)";
        public static string ConvertSourceToTus(string uriSource)
        {
            var uri = "";
            if (uriSource.IndexOf("tus:", StringComparison.Ordinal) == 0)
            {
                return uriSource;

            }
            else
            {
                var tusId = uriSource.Split('/').LastOrDefault();

                if (tusId != uriSource)
                {
                    uri = $"tus:{tusId}";
                }
                else
                {
                    uri = uriSource;
                }
            }

            return uri;
        }
    }
}
