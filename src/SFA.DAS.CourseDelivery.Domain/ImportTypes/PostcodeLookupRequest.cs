using System.Collections.Generic;
using Newtonsoft.Json;

namespace SFA.DAS.CourseDelivery.Domain.ImportTypes
{
    public class PostcodeLookupRequest
    {
        [JsonProperty("postcodes")]
        public List<string> Postcodes { get; set; }
    }
}