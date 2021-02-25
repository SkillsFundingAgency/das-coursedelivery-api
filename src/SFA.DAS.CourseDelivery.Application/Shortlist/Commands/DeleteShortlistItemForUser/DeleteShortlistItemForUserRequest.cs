using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistItemForUser
{
    public class DeleteShortlistItemForUserRequest : IRequest<Unit>
    {
        public Guid Id { get ; set ; }
        public Guid ShortlistUserId { get ; set ; }
    }
}