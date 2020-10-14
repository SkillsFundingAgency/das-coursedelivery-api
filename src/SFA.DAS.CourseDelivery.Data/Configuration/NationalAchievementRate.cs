using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class NationalAchievementRate : IEntityTypeConfiguration<Domain.Entities.NationalAchievementRate>
    {
        private readonly bool _buildRelations;

        public NationalAchievementRate (bool buildRelations = true)
        {
            _buildRelations = buildRelations;
        }
        public void Configure(EntityTypeBuilder<Domain.Entities.NationalAchievementRate> builder)
        {
            builder.ToTable("NationalAchievementRate");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id).UseIdentityColumn().IsRequired().HasColumnName("Id");
            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int");
            builder.Property(x => x.Age).HasColumnName("Age").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.SectorSubjectArea).HasColumnName("SectorSubjectArea").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.ApprenticeshipLevel).HasColumnName("ApprenticeshipLevel").HasColumnType("smallint").IsRequired();
            builder.Property(x => x.OverallCohort).HasColumnName("OverallCohort").HasColumnType("int").IsRequired(false);
            builder.Property(x => x.OverallAchievementRate).HasColumnName("OverallAchievementRate").HasColumnType("decimal").IsRequired(false);

            if(_buildRelations)
            {
                builder.HasOne(c => c.Provider)
                .WithMany(c => c.NationalAchievementRates)
                .HasForeignKey(c => c.Ukprn)
                .HasPrincipalKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            }
            
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}