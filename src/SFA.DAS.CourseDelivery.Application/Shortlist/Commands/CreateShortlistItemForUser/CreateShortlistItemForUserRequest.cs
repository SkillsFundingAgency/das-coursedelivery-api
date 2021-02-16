using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser
{
    public class CreateShortlistItemForUserRequest : IRequest<Guid>
    {
        public Guid ShortlistUserId { get ; set ; }
        public int StandardId { get ; set ; }
        public string SectorSubjectArea { get ; set ; }
        public int Ukprn { get ; set ; }
        public float? Lat { get ; set ; }
        public float? Lon { get ; set ; }
        public string LocationDescription { get ; set ; }
    }
}