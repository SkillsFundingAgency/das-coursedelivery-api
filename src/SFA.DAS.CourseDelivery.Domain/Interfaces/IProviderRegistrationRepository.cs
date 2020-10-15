using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationRepository
    {
        Task InsertFromImportTable();
        void DeleteAll();
        Task InsertMany(IEnumerable<ProviderRegistration> providerRegistrations);
    }
}