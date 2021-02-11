using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ShortlistProviderWithStandardAndLocation : IEntityTypeConfiguration<Domain.Entities.ShortlistProviderWithStandardAndLocation>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ShortlistProviderWithStandardAndLocation> builder)
        {
            builder.HasKey(x => new {x.ShortlistId});
            builder.Property(x => x.ShortlistId).HasColumnType("uniqueidentifier");
            builder.Property(x => x.ShortlistUserId).HasColumnType("uniqueidentifier");
            builder.Property(x => x.DistanceInMiles).HasColumnType("float");
            builder.Property(x => x.ProviderDistanceInMiles).HasColumnType("float");
            builder.Property(x => x.Id).IsRequired(false);
            builder.Property(x => x.Age).IsRequired(false);
            builder.Property(x => x.SectorSubjectArea).IsRequired(false);
            builder.Property(x => x.ApprenticeshipLevel).IsRequired(false);
            builder.Property(x => x.OverallCohort).IsRequired(false);
            builder.Property(x => x.OverallAchievementRate).IsRequired(false);
            builder.Property(x => x.CourseId).HasColumnType("int");
            builder.Property(x => x.LocationDescription).HasColumnType("varchar").HasMaxLength(1000).IsRequired(false);
        }
    }
}