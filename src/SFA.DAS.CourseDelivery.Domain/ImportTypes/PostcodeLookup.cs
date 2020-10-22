using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class PostcodeLookup
    {
        [JsonProperty("status")]
        public long Status { get; set; }

        [JsonProperty("result")]
        public List<PostcodeQueryData> Result { get; set; }
    }
    public partial class PostcodeQueryData
    {
        [JsonProperty("query")]
        public string Query { get; set; }

        [JsonProperty("result")]
        public PostcodeData Result { get; set; }
    }

    public partial class PostcodeData
    {
        [JsonProperty("postcode")]
        public string Postcode { get; set; }

        [JsonProperty("longitude")]
        public double Longitude { get; set; }

        [JsonProperty("latitude")]
        public double Latitude { get; set; }

    }

}