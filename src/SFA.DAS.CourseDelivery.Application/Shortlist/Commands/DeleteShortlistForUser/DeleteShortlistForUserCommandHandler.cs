using System.Threading;
using System.Threading.Tasks;
using MediatR;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.DeleteShortlistForUser
{
    public class DeleteShortlistForUserCommandHandler : IRequestHandler<DeleteShortlistForUserCommand, Unit>
    {
        private readonly IShortlistService _service;

        public DeleteShortlistForUserCommandHandler (IShortlistService service)
        {
            _service = service;
        }
        public async Task<Unit> Handle(DeleteShortlistForUserCommand request, CancellationToken cancellationToken)
        {
            await _service.DeleteShortlist(request.ShortlistUserId);
            
            return Unit.Value;
        }
    }
}