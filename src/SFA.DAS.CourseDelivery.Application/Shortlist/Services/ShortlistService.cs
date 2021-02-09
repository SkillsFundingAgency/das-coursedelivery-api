using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task CreateShortlistItem(Domain.Entities.Shortlist shortlist)
        {
            await _shortlistRepository.Insert(shortlist);
        }
    }
}