using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IShortlistService
    {
        Task<IEnumerable<Shortlist>> GetAllForUser(Guid userId);
    }
}