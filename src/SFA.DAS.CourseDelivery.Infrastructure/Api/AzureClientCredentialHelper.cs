using System.Threading.Tasks;
using Microsoft.Azure.Services.AppAuthentication;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.Api
{
    public class AzureClientCredentialHelper : IAzureClientCredentialHelper
    {
        public async Task<string> GetAccessTokenAsync(string identifier)
        {
            var azureServiceTokenProvider = new AzureServiceTokenProvider();
            var accessToken = await azureServiceTokenProvider.GetAccessTokenAsync(identifier);
         
            return accessToken;
        }
    }
}