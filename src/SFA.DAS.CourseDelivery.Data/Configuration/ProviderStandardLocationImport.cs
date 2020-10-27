using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderStandardLocationImport : IEntityTypeConfiguration<Domain.Entities.ProviderStandardLocationImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderStandardLocationImport> builder)
        {
            builder.ToTable("ProviderStandardLocation_Import");
            builder.HasKey(x => new {x.Ukprn, x.StandardId, x.LocationId });
            
            builder.Property(c => c.StandardId).HasColumnName("StandardId").HasColumnType("int").IsRequired();
            builder.Property(c => c.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(c => c.LocationId).HasColumnName("LocationId").HasColumnType("int").IsRequired();
            builder.Property(c => c.DeliveryModes).HasColumnName("DeliveryModes").HasColumnType("varchar").HasMaxLength(256).IsRequired();
            builder.Property(c => c.Radius).HasColumnName("Radius").HasColumnType("decimal").IsRequired();
            builder.Property(c => c.National).HasColumnName("National").HasColumnType("bit").IsRequired();

            builder.HasIndex(x => new { x.Ukprn , x.StandardId, x.LocationId }).IsUnique();
        }
    }
}