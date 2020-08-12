using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderStandardLocationRepository
    {
        Task InsertFromImportTable();
        void DeleteAll();
    }
}