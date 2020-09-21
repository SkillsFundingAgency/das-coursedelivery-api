using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderRegistrationFeedbackRatingImport : IEntityTypeConfiguration<Domain.Entities.ProviderRegistrationFeedbackRatingImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderRegistrationFeedbackRatingImport> builder)
        {
            builder.ToTable("ProviderRegistrationFeedbackRating_Import");
            builder.HasKey(x => x.Ukprn);
            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.FeedbackCount).HasColumnName("FeedbackCount").HasColumnType("int").IsRequired();
            builder.Property(x => x.FeedbackName).HasColumnName("FeedbackName").IsRequired();
        }
    }
}