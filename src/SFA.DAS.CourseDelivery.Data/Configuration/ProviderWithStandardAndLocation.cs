using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderWithStandardAndLocation : IEntityTypeConfiguration<Domain.Entities.ProviderWithStandardAndLocation>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderWithStandardAndLocation> builder)
        {
            builder.HasKey(x => new {UkPrn = x.Ukprn, x.LocationId});
            builder.Property(x => x.DistanceInMiles).HasColumnType("float");
            builder.Property(x => x.ProviderDistanceInMiles).HasColumnType("float");
            builder.Property(x => x.Id).IsRequired(false);
            builder.Property(x => x.Age).IsRequired(false);
            builder.Property(x => x.SectorSubjectArea).IsRequired(false);
            builder.Property(x => x.ApprenticeshipLevel).IsRequired(false);
            builder.Property(x => x.OverallCohort).IsRequired(false);
            builder.Property(x => x.OverallAchievementRate).IsRequired(false);
            builder.Property(x => x.ShortlistId).HasColumnType("UniqueIdentifier").IsRequired(false);
        }
    }
}