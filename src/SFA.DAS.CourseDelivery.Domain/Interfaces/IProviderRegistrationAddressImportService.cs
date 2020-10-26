using System.Threading.Tasks;

namespace SFA.DAS.CourseDelivery.Domain.Interfaces
{
    public interface IProviderRegistrationAddressImportService
    {
        Task ImportAddressData();
    }
}