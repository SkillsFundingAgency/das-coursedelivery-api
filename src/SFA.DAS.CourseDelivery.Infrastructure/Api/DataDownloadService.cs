using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class DataDownloadService : IDataDownloadService
    {
        private readonly HttpClient _client;

        public DataDownloadService (HttpClient client)
        {
            _client = client;
        }

        public async Task<Stream> GetFileStream(string downloadPath)
        {
            var response = await _client.GetAsync(downloadPath);
            response.EnsureSuccessStatusCode();
            var stream = await response.Content.ReadAsStreamAsync();
            
            return stream;
        }
    }
}