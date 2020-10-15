using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderStandard : IEntityTypeConfiguration<Domain.Entities.ProviderStandard>
    {
        private readonly bool _buildRelations;

        public ProviderStandard (bool buildRelations = true)
        {
            _buildRelations = buildRelations;
        }
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderStandard> builder)
        {
            builder.ToTable("ProviderStandard");
            builder.HasKey(x => new { x.Ukprn , x.StandardId }).HasName("PK_ProviderStandard");

            builder.Property(c => c.StandardId).HasColumnName("StandardId").HasColumnType("int").IsRequired();
            builder.Property(c => c.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(c => c.StandardInfoUrl).HasColumnName("StandardInfoUrl").HasMaxLength(1000).HasColumnType("varchar").IsRequired(false);
            builder.Property(c => c.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(c => c.Phone).HasColumnName("Phone").HasColumnType("varchar").HasMaxLength(50).IsRequired(false);
            builder.Property(c => c.ContactUrl).HasColumnName("ContactUrl").HasColumnType("varchar").HasMaxLength(500).IsRequired(false);

            if (_buildRelations)
            {
                builder.HasMany(c => c.ProviderStandardLocation)
                    .WithOne(c => c.ProviderStandard)
                    .HasForeignKey(c => new {c.Ukprn, c.StandardId })
                    .HasPrincipalKey(c => new {c.Ukprn, c.StandardId}).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
                builder.HasOne(c => c.Provider)
                    .WithMany(c=>c.ProviderStandards)
                    .HasPrincipalKey(c => c.Ukprn)
                    .HasForeignKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            
                builder.HasMany(c=>c.NationalAchievementRate)
                    .WithOne(c => c.ProviderStandard)
                    .HasPrincipalKey(c => c.Ukprn)
                    .HasForeignKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;    
            }
            else
            {
                builder.Ignore(c => c.ProviderStandardLocation);
                builder.Ignore(c => c.Provider);
                builder.Ignore(c => c.NationalAchievementRate);
            }

            builder.HasIndex(x => new { x.Ukprn , x.StandardId }).IsUnique();
        }
    }
}