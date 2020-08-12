using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IStandardLocationRepository
    {
        Task InsertFromImportTable();
        void DeleteAll();
    }
}