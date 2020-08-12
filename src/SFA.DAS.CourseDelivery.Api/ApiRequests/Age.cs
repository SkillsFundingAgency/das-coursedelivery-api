using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace SFA.DAS.CourseDelivery.Api.ApiRequests
{
    [JsonConverter(typeof(StringEnumConverter))]
    public enum Age : short
    {
        Unknown = 0,
        SixteenToEighteen = 1,
        NineteenToTwentyThree = 2,
        TwentyFourPlus = 3,
        AllAges = 4
    }
}