using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ProviderRegistrationFeedbackAttribute : IEntityTypeConfiguration<Domain.Entities.ProviderRegistrationFeedbackAttribute>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ProviderRegistrationFeedbackAttribute> builder)
        {
            builder.ToTable("ProviderRegistrationFeedbackAttribute");
            builder.HasKey(x => new
            {
                x.Ukprn,x.AttributeName
            });
            builder.Property(x=>x.Ukprn).HasColumnName("Ukprn").HasColumnType("int").IsRequired();
            builder.Property(x=>x.Strength).HasColumnName("Strength").HasColumnType("int").IsRequired();
            builder.Property(x=>x.Weakness).HasColumnName("Weakness").HasColumnType("int").IsRequired();
            builder.Property(x => x.AttributeName).HasColumnName("AttributeName").IsRequired();
        }
    }
}