using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class StandardLocationImport : IEntityTypeConfiguration<Domain.Entities.StandardLocationImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.StandardLocationImport> builder)
        {
            builder.ToTable("StandardLocation_Import");
            builder.HasKey(x => x.LocationId);

            builder.Property(c => c.LocationId).HasColumnName("LocationId").HasColumnType("bigint").IsRequired();
            builder.Property(c => c.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(250).IsRequired(false);
            builder.Property(c => c.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(c => c.Website).HasColumnName("Website").HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(c => c.Phone).HasColumnName("Phone").HasColumnType("varchar").HasMaxLength(50).IsRequired(false);
            builder.Property(c => c.Address1).HasColumnName("Address1").HasColumnType("varchar").HasMaxLength(250).IsRequired();
            builder.Property(c => c.Address2).HasColumnName("Address2").HasColumnType("varchar").HasMaxLength(250).IsRequired(false);
            builder.Property(c => c.County).HasColumnName("County").HasColumnType("varchar").HasMaxLength(250).IsRequired(false);
            builder.Property(c => c.Town).HasColumnName("Town").HasColumnType("varchar").HasMaxLength(250).IsRequired(false);
            builder.Property(c => c.Postcode).HasColumnName("Postcode").HasColumnType("varchar").HasMaxLength(25).IsRequired();
            builder.Property(c => c.Lat).HasColumnName("Lat").HasColumnType("float").IsRequired();
            builder.Property(c => c.Long).HasColumnName("Long").HasColumnType("float").IsRequired();
            
            builder.HasIndex(x => x.LocationId).IsUnique();
        }
    }
}