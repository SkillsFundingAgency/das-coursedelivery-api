using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser
{
    public class CreateShortlistItemForUserRequest : IRequest<Unit>
    {
        public Guid ShortlistUserId { get ; set ; }
        public int CourseId { get ; set ; }
        public int Level { get ; set ; }
        public string SectorSubjectArea { get ; set ; }
        public int ProviderUkprn { get ; set ; }
    }
}