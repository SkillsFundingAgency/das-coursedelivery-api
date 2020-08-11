using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class NationalAchievementRateOverallImport : IEntityTypeConfiguration<Domain.Entities.NationalAchievementRateOverallImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.NationalAchievementRateOverallImport> builder)
        {
            builder.ToTable("NationalAchievementRateOverall_Import");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn().IsRequired().HasColumnName("Id");
            builder.Property(x => x.Age).HasColumnName("Age").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.SectorSubjectArea).HasColumnName("SectorSubjectArea").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ApprenticeshipLevel).HasColumnName("ApprenticeshipLevel").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.OverallCohort).HasColumnName("OverallCohort").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OverallAchievementRate).HasColumnName("OverallAchievementRate").HasColumnType("decimal").IsRequired(false);

            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}