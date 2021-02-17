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
        Task<Shortlist> GetShortlistUserItem(Shortlist item);
        Task<IEnumerable<ShortlistProviderWithStandardAndLocation>> GetShortListForUser(Guid userId);
        void Delete(Guid id, Guid shortlistUserId);
        Task<int> GetShortlistItemCountForUser(Guid userId);
    }
}