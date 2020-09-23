using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Domain.Validation;

namespace SFA.DAS.CourseDelivery.Application.Provider.Queries.GetUkprnsByCourseAndLocation
{
    public class GetUkprnsQueryValidator : IValidator<GetUkprnsQuery>
    {
        public Task<ValidationResult> ValidateAsync(GetUkprnsQuery item)
        {
            var validationResult= new ValidationResult();
            if (item.StandardId == 0)
            {
                validationResult.AddError(nameof(item.StandardId));
            }

            return Task.FromResult(validationResult);
        }
    }
}