using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class ImportAudit : IEntityTypeConfiguration<Domain.Entities.ImportAudit>
    {
        public void Configure(EntityTypeBuilder<Domain.Entities.ImportAudit> builder)
        {
            builder.ToTable("ImportAudit");
            builder.HasKey(x=> x.Id);

            builder.Property(x => x.Id).HasColumnName("Id").HasColumnType("int").IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.TimeStarted).HasColumnName("TimeStarted").HasColumnType("DateTime").IsRequired();
            builder.Property(x => x.TimeFinished).HasColumnName("TimeFinished").HasColumnType("DateTime").IsRequired();
            builder.Property(x => x.RowsImported).HasColumnName("RowsImported").HasColumnType("int").IsRequired();
            builder.Property(x => x.ImportType).HasColumnName("ImportType").HasColumnType("tinyint").IsRequired();
            builder.Property(x => x.FileName).HasColumnName("FileName").HasColumnType("varchar").HasMaxLength(250).IsRequired(false);
     
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}