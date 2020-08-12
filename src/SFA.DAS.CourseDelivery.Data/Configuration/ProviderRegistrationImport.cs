using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderRegistrationImport : IEntityTypeConfiguration<Domain.Entities.ProviderRegistrationImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderRegistrationImport> builder)
        {
            builder.ToTable("ProviderRegistration_Import");
            builder.HasKey(x => x.Ukprn);
        }
    }
}