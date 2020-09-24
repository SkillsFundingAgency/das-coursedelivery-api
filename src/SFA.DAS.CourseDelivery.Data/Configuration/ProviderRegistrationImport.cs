using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderRegistrationImport : IEntityTypeConfiguration<Domain.Entities.ProviderRegistrationImport>
    {
        public const string TableName = "ProviderRegistration_Import";

        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderRegistrationImport> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Ukprn);

            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.StatusDate).HasColumnName("StatusDate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.StatusId).HasColumnName("StatusId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ProviderTypeId).HasColumnName("ProviderTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.OrganisationTypeId).HasColumnName("OrganisationTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FeedbackTotal).HasColumnName("FeedbackTotal").HasColumnType("int").IsRequired();

            builder.HasIndex(x => x.Ukprn).IsUnique();
        }
    }
}