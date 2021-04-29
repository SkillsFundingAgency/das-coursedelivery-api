using System;
using MediatR;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistForUser
{
    public class DeleteShortlistForUserCommand : IRequest<Unit>
    {
        public Guid ShortlistUserId { get; set; }
    }
}