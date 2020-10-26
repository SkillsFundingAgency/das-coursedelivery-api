using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IPostcodeApiService
    {
        Task<PostcodeLookup> GetPostcodeData(PostcodeLookupRequest postcodeLookupRequest);
    }
}