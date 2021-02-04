using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class Shortlist : IEntityTypeConfiguration<Domain.Entities.Shortlist>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.Shortlist> builder)
        {
            builder.ToTable("Shortlist");
            builder.HasKey(x=> x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.ShortlistUserId).HasColumnName("ShortlistUserId").HasColumnType("UniqueIdentifier").IsRequired();
            builder.Property(x => x.ProviderUkprn).HasColumnName("ProviderUkprn").HasColumnType("int").IsRequired();
            builder.Property(x => x.CourseId).HasColumnName("CourseId").HasColumnType("int").IsRequired();
            builder.Property(x => x.LocationId).HasColumnName("LocationId").HasColumnType("int").IsRequired();

            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}