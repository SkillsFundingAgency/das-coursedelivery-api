using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderStandardImport : IEntityTypeConfiguration<Domain.Entities.ProviderStandardImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderStandardImport> builder)
        {
            builder.ToTable("ProviderStandard_Import");
            builder.HasKey(x => new { x.Ukprn , x.StandardId }).HasName("PK_ProviderStandardImport");

            builder.Property(c => c.StandardId).HasColumnName("StandardId").HasColumnType("int").IsRequired();
            builder.Property(c => c.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(c => c.StandardInfoUrl).HasColumnName("StandardInfoUrl").HasMaxLength(1000).HasColumnType("varchar").IsRequired(false);
            builder.Property(c => c.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(c => c.Phone).HasColumnName("Phone").HasColumnType("varchar").HasMaxLength(50).IsRequired(false);
            builder.Property(c => c.ContactUrl).HasColumnName("ContactUrl").HasColumnType("varchar").HasMaxLength(500).IsRequired(false);
            
            builder.HasIndex(x => new { x.Ukprn , x.StandardId }).IsUnique();
        }
    }
}