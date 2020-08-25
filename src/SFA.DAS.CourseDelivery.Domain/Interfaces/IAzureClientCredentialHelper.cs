using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IAzureClientCredentialHelper
    {
        Task<string> GetAccessTokenAsync(string identifier);
    }
}