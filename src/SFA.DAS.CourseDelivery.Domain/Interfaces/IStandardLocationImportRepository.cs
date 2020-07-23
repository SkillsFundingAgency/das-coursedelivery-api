using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IStandardLocationImportRepository
    {
        Task InsertMany(IEnumerable<StandardLocationImport> items);
        void DeleteAll();
        Task<IEnumerable<StandardLocationImport>> GetAll();
    }
}