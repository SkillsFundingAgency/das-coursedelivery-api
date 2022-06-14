using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ApprenticeFeedbackAttributesImport : IEntityTypeConfiguration<Domain.Entities.ApprenticeFeedbackAttributesImport>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ApprenticeFeedbackAttributesImport> builder)
        {
            builder.ToTable("ApprenticeFeedbackAttributes_Import");
            builder.HasKey(x => x.AttributeId);

            builder.Property(x => x.AttributeId).UseIdentityColumn().IsRequired().HasColumnName("AttributeId");
            builder.Property(x => x.AttributeName).HasColumnName("AttributeName").HasColumnType("nvarchar").HasMaxLength(100).IsRequired();
            builder.Property(x => x.Category).HasColumnName("Category").HasColumnType("nvarchar").HasMaxLength(100).IsRequired();

            builder.HasIndex(x => x.AttributeId).IsUnique();
        }
    }
}
