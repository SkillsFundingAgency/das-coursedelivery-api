using System.Threading.Tasks;
using SFA.DAS.CourseDelivery.Domain.Validation;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IValidator<T>
    {
        Task<ValidationResult> ValidateAsync(T item);
    }
}