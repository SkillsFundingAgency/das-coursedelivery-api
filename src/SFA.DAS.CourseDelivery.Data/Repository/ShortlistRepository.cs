using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Data.Repository
{
    public class ShortlistRepository : IShortlistRepository
    {
        private readonly ICourseDeliveryDataContext _dataContext;
        private readonly ICourseDeliveryReadonlyDataContext _readonlyDataContext;

        public ShortlistRepository(
            ICourseDeliveryDataContext dataContext, 
            ICourseDeliveryReadonlyDataContext readonlyDataContext)
        {
            _dataContext = dataContext;
            _readonlyDataContext = readonlyDataContext;
        }

        public async Task<IEnumerable<Shortlist>> GetAllForUser(Guid userId)
        {
            return await _readonlyDataContext.Shortlists
                .Where(shortlist => shortlist.ShortlistUserId == userId)
                .ToListAsync();
        }

        public async Task<Shortlist> GetShortlistUserItem(Guid shortlistUserId, int courseId, int providerUkprn, float? lat, float? lon)
        {
            return await _readonlyDataContext.Shortlists.SingleOrDefaultAsync(c=>
                c.ShortlistUserId.Equals(shortlistUserId)
                && c.CourseId.Equals(courseId)
                && c.ProviderUkprn.Equals(providerUkprn)
                && c.Lat.Equals(lat)
                && c.Long.Equals(lon)
                );
        }

        public async Task Insert(Shortlist item)
        {
            await _dataContext.Shortlists.AddAsync(item);
            _dataContext.SaveChanges();
        }
    }
}