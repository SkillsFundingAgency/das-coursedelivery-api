using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IStandardLocationRepository
    {
        Task InsertMany(IEnumerable<StandardLocation> standardLocations);
        void DeleteAll();
    }
}