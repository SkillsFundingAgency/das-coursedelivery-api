using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class DevRoatpApiService : IRoatpApiService
    {
        public async Task<IEnumerable<ProviderRegistration>> GetProviderRegistrations()
        {
            using (var r = new StreamReader("roatp.json"))
            {
                var json = await r.ReadToEndAsync();
                var items = JsonConvert.DeserializeObject<List<ProviderRegistration>>(json);
                return items.ToList();
            }
        }

        public async Task<ProviderRegistrationLookup> GetProviderRegistrationLookupData(IEnumerable<int> ukprns)
        {
            using (var r = new StreamReader("roatp-address.json"))
            {
                var json = await r.ReadToEndAsync();
                var items = JsonConvert.DeserializeObject<ProviderRegistrationLookup>(json);
                return items;
            }
        }
    }
}
