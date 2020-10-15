using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderRegistrationFeedbackRating : IEntityTypeConfiguration<Domain.Entities.ProviderRegistrationFeedbackRating>
    {
        private bool _buildRelations;
    
        public ProviderRegistrationFeedbackRating (bool buildRelations = true)
        {
            _buildRelations = buildRelations;
        }
        
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderRegistrationFeedbackRating> builder)
        {
            builder.ToTable("ProviderRegistrationFeedbackRating");
            builder.HasKey(x => new{x.Ukprn, x.FeedbackName});
            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.FeedbackCount).HasColumnName("FeedbackCount").HasColumnType("int").IsRequired();
            builder.Property(x => x.FeedbackName).HasColumnName("FeedbackName").IsRequired();

            if (!_buildRelations)
            {
                builder.Ignore(c => c.Provider);
                builder.Ignore(c => c.ProviderRegistration);
            }
        }
    }
}