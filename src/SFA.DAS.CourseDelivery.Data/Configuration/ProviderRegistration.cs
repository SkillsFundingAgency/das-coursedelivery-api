using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderRegistration : IEntityTypeConfiguration<Domain.Entities.ProviderRegistration>
    {
        private readonly bool _buildRelations;
        public const string TableName = "ProviderRegistration";

        public ProviderRegistration (bool buildRelations = true)
        {
            _buildRelations = buildRelations;
        }
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderRegistration> builder)
        {
            builder.ToTable(TableName);
            builder.HasKey(x => x.Ukprn);

            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.StatusDate).HasColumnName("StatusDate").HasColumnType("datetime").IsRequired();
            builder.Property(x => x.StatusId).HasColumnName("StatusId").HasColumnType("int").IsRequired();
            builder.Property(x => x.ProviderTypeId).HasColumnName("ProviderTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.OrganisationTypeId).HasColumnName("OrganisationTypeId").HasColumnType("int").IsRequired();
            builder.Property(x => x.FeedbackTotal).HasColumnName("FeedbackTotal").HasColumnType("int").IsRequired();

            if (_buildRelations)
            {
                builder.HasOne(c => c.Provider)
                        .WithOne(c => c.ProviderRegistration)
                        .HasPrincipalKey<Domain.Entities.Provider>(c => c.Ukprn)
                        .HasForeignKey<Domain.Entities.ProviderRegistration>(c => c.Ukprn).Metadata.DeleteBehavior =
                    DeleteBehavior.Restrict;

                builder.HasMany(c => c.ProviderRegistrationFeedbackAttributes)
                    .WithOne(c => c.ProviderRegistration)
                    .HasForeignKey(c => c.Ukprn)
                    .HasPrincipalKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;

                builder.HasMany(c => c.ProviderRegistrationFeedbackRating)
                    .WithOne(c => c.ProviderRegistration)
                    .HasForeignKey(c => c.Ukprn)
                    .HasPrincipalKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            }

            builder.HasIndex(x => x.Ukprn).IsUnique();
        }
    }
}
