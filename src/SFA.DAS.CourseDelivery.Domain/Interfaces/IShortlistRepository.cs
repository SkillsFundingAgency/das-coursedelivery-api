using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IShortlistRepository
    {
        Task<IEnumerable<Shortlist>> GetAllForUser(Guid userId);
        Task Insert(Shortlist item);
    }
}