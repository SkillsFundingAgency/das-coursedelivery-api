using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ApprenticeFeedbackAttributes : IEntityTypeConfiguration<Domain.Entities.ApprenticeFeedbackAttributes>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ApprenticeFeedbackAttributes> builder)
        {
            builder.ToTable("ApprenticeFeedbackAttributes");
            builder.HasKey(x => x.AttributeId);

            builder.Property(x => x.AttributeId).UseIdentityColumn().IsRequired().HasColumnName("AttributeId");
            builder.Property(x => x.AttributeName).HasColumnName("AttributeName").HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Category).HasColumnName("Category").HasColumnType("nvarchar").HasMaxLength(100).IsRequired();

            builder.HasIndex(x => x.AttributeId).IsUnique();
        }
    }
}
