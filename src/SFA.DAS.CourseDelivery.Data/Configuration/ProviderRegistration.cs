using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderRegistration : IEntityTypeConfiguration<Domain.Entities.ProviderRegistration>
    {
        public const string TableName = "ProviderRegistration";

        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderRegistration> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Ukprn);

            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.Ukprn).HasColumnName("StatusDate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.Ukprn).HasColumnName("StatusId").HasColumnType("int").IsRequired();
            builder.Property(x => x.Ukprn).HasColumnName("ProviderTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.Ukprn).HasColumnName("OrganisationTypeId").HasColumnType("int").IsRequired();
        }
    }
}
