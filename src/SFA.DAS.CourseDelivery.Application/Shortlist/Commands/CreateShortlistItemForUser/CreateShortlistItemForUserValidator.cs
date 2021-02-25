using System;
using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Validation;

namespace SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser
{
    public class CreateShortlistItemForUserValidator : IValidator<CreateShortlistItemForUserRequest>
    {
        public Task<ValidationResult> ValidateAsync(CreateShortlistItemForUserRequest item)
        {
            var result = new ValidationResult();

            if (item.ShortlistUserId == Guid.Empty)
            {
                result.AddError(nameof(item.ShortlistUserId));
            }

            if (item.StandardId == 0)
            {
                result.AddError(nameof(item.StandardId));
            }

            if (item.Ukprn == 0)
            {
                result.AddError(nameof(item.Ukprn));
            }

            if (string.IsNullOrEmpty(item.SectorSubjectArea))
            {
                result.AddError(nameof(item.SectorSubjectArea));
            }
            
            return Task.FromResult(result);
        }
    }
}