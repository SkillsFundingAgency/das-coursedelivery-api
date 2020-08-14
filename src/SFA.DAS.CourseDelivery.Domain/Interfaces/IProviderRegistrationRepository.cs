using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationRepository
    {
        Task InsertFromImportTable();
        void DeleteAll();
    }
}