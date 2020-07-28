using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class DevCourseDirectoryService : ICourseDirectoryService
    {
        public async Task<IEnumerable<Provider>> GetProviderCourseInformation()
        {
            using (var r = new StreamReader("coursedir.json"))
            {
                var json = await r.ReadToEndAsync();
                var items = JsonConvert.DeserializeObject<List<Provider>>(json);
                return items.ToList();
            }
        }
    }
}