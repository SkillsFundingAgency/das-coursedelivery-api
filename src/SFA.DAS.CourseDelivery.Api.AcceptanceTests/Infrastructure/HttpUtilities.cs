﻿using Newtonsoft.Json;
using System.Net.Http;
using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Api.AcceptanceTests.Infrastructure
{
    public class HttpUtilities
    {
        public static async Task<T> ReadContent<T>(HttpContent httpContent)
        {
            var json = await httpContent.ReadAsStringAsync();
            var model = JsonConvert.DeserializeObject<T>(json);
            return model;
        }
    }
}
