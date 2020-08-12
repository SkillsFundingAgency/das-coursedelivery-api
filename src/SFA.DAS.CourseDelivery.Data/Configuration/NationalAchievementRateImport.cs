using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class NationalAchievementRateImport : IEntityTypeConfiguration<Domain.Entities.NationalAchievementRateImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.NationalAchievementRateImport> builder)
        {
            builder.ToTable("NationalAchievementRate_Import");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn().IsRequired().HasColumnName("Id");
            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int");
            builder.Property(x => x.Age).HasColumnName("Age").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.SectorSubjectArea).HasColumnName("SectorSubjectArea").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ApprenticeshipLevel).HasColumnName("ApprenticeshipLevel").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.OverallCohort).HasColumnName("OverallCohort").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OverallAchievementRate).HasColumnName("OverallAchievementRate").HasColumnType("decimal").IsRequired(false);
            
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}