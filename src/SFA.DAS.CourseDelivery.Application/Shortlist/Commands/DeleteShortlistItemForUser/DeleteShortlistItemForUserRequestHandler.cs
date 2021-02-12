using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistItemForUser
{
    public class DeleteShortlistItemForUserRequestHandler : IRequestHandler<DeleteShortlistItemForUserRequest, Unit>
    {
        private readonly IShortlistService _service;

        public DeleteShortlistItemForUserRequestHandler (IShortlistService service)
        {
            _service = service;
        }
        public Task<Unit> Handle(DeleteShortlistItemForUserRequest request, CancellationToken cancellationToken)
        {
            _service.DeleteShortlistUserItem(request.Id, request.ShortlistUserId);
            
            return Task.FromResult(Unit.Value);
        }
    }
}