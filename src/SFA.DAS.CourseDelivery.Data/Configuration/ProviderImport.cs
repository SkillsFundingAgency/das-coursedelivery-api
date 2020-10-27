using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderImport : IEntityTypeConfiguration<Domain.Entities.ProviderImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderImport> builder)
        {
            builder.ToTable("Provider_Import");
            builder.HasKey(x => x.Id);
            
            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("bigint").IsRequired();
            builder.Property(x => x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.Name).HasColumnName("Name").HasColumnType("varchar").HasMaxLength(1000).IsRequired();
            builder.Property(x => x.EmployerSatisfaction).HasColumnName("EmployerSatisfaction").HasColumnType("decimal").IsRequired(false);
            builder.Property(x => x.LearnerSatisfaction).HasColumnName("LearnerSatisfaction").HasColumnType("decimal").IsRequired(false);
            builder.Property(x => x.TradingName).HasColumnName("TradingName").HasColumnType("varchar").HasMaxLength(1000).IsRequired(false);
            builder.Property(x => x.Email).HasColumnName("Email").HasColumnType("varchar").HasMaxLength(256).IsRequired(false);
            builder.Property(x => x.Phone).HasColumnName("Phone").HasColumnType("varchar").HasMaxLength(50).IsRequired(false);
            builder.Property(x => x.Website).HasColumnName("Website").HasColumnType("varchar").HasMaxLength(500).IsRequired(false);
            
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}