using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Entities;
using Shortlist = SFA.DAS.CourseDelivery.Domain.Models.Shortlist;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IShortlistService
    {
        Task<IEnumerable<Shortlist>> GetAllForUser(Guid userId);
        Task<Guid> CreateShortlistItem(Domain.Entities.Shortlist shortlist);
        Task<IEnumerable<Shortlist>> GetAllForUserWithProviders(Guid userId);
        void DeleteShortlistUserItem(Guid id, Guid userId);
        Task<int> GetShortlistItemCountForUser(Guid userId);
        Task<IEnumerable<Guid>> GetExpiredShortlistUserIds(uint expiryInDays);
    }
}