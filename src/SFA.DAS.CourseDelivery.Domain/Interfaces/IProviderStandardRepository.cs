using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderStandardRepository
    {
        void DeleteAll();
        Task InsertMany(IEnumerable<ProviderStandard> providerStandards);
        Task<ProviderStandard> GetByUkprnAndStandard(int ukPrn, int standardId);
    }
}