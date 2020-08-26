using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SFA.DAS.CourseDelivery.Api.ApiRequests
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum SortOrder : short
    {
        Distance = 0,
        Name = 1
    }
}