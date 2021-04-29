using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Extensions;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Services
{
    public class ShortlistService : IShortlistService
    {
        private readonly IShortlistRepository _shortlistRepository;

        public ShortlistService(IShortlistRepository shortlistRepository)
        {
            _shortlistRepository = shortlistRepository;
        }

        public async Task<IEnumerable<Domain.Models.Shortlist>> GetAllForUser(Guid userId)
        {
            var shortlist = await _shortlistRepository.GetAllForUser(userId);
            return shortlist.Select(entity => (Domain.Models.Shortlist) entity);
        }

        public async Task<Guid> CreateShortlistItem(Domain.Entities.Shortlist shortlist)
        {
            var item = await _shortlistRepository.GetShortlistUserItem(shortlist);
            if (item == null)
            {
                await _shortlistRepository.Insert(shortlist);    
            }
            return item?.Id ?? shortlist.Id;
        }

        public async Task<IEnumerable<Domain.Models.Shortlist>> GetAllForUserWithProviders(Guid userId)
        {
            var items = await _shortlistRepository.GetShortListForUser(userId);

            return items.ToList().BuildShortlistProviderLocation();
        }

        public void DeleteShortlistUserItem(Guid id, Guid userId)
        {
            _shortlistRepository.Delete(id, userId);
        }

        public async Task<int> GetShortlistItemCountForUser(Guid userId)
        {
            return await _shortlistRepository.GetShortlistItemCountForUser(userId);
        }

        public async Task<IEnumerable<Guid>> GetExpiredShortlistUserIds(uint expiryInDays)
        {
            return await _shortlistRepository.GetExpiredShortlistUserIds(expiryInDays);
        }
    }
}