using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Qencode.Api.CSharp.Client.Classes
{
    //[JsonConverter(typeof(StitchVideoItemConverter))]
    public class StitchVideoItem
    {
        /// <summary>
        /// Source video URI. Can be http(s) url or tus uri
        /// </summary>
        public string url { get; set; }

        /// <summary>
        /// Specifies the start time (in seconds) in input video to begin transcoding from.
        /// </summary>
        public double? start_time { get; set; }

        /// <summary>
        /// Specifies duration of the video fragment (in seconds) to be transcoded.
        /// </summary>
        public double? duration { get; set; }

        public StitchVideoItem() { }

        public StitchVideoItem(string url)
        {
            this.url = url;
        }



    }


    /*public class StitchVideoItemConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            if (reader.Value == null)
            {
                return new StitchVideoItem();
            }

            JToken token = JToken.Load(reader);
            var jValue = new JValue(reader.Value);
            var tokenType = reader.TokenType;
            if (tokenType == JsonToken.String)
            {
                return new StitchVideoItem((string)jValue);
            }
            return new StitchVideoItem();
            //return first.Value<StitchVideoItem>();

        }

        public override bool CanConvert(Type objectType)
        {
            return true;
        }
    }*/
}
