using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace SFA.DAS.CourseDelivery.Data.Configuration
{
    public class Provider : IEntityTypeConfiguration<Domain.Entities.Provider>
    {
        private readonly bool _buildRelations;

        public Provider(bool buildRelations = true)
        {
            _buildRelations = buildRelations;
        }

        public void Configure(EntityTypeBuilder<Domain.Entities.Provider> builder)
        {
            builder.ToTable("Provider");
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
            builder.Property(x => x.MarketingInfo).HasColumnName("MarketingInfo").HasColumnType("varchar(max)").IsRequired(false);
            
            if (_buildRelations)
            {
                builder.HasMany(c => c.ProviderStandards)
                    .WithOne(c=>c.Provider)
                    .HasPrincipalKey(c => c.Ukprn)
                    .HasForeignKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            
                builder.HasMany(c=>c.NationalAchievementRates)
                    .WithOne(c=>c.Provider)
                    .HasPrincipalKey(c =>c.Ukprn)
                    .HasForeignKey(c=>c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            
                builder.HasMany(c => c.ProviderRegistrationFeedbackAttributes)
                    .WithOne(c => c.Provider)
                    .HasForeignKey(c => c.Ukprn)
                    .HasPrincipalKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            
                builder.HasMany(c => c.ProviderRegistrationFeedbackRating)
                    .WithOne(c => c.Provider)
                    .HasForeignKey(c => c.Ukprn)
                    .HasPrincipalKey(c => c.Ukprn).Metadata.DeleteBehavior = DeleteBehavior.Restrict;
            }
            else
            {
                builder.Ignore(c => c.ProviderStandards);
                builder.Ignore(c => c.NationalAchievementRates);
                builder.Ignore(c => c.ProviderRegistrationFeedbackAttributes);
                builder.Ignore(c => c.ProviderRegistrationFeedbackRating);
                builder.Ignore(c => c.ProviderRegistration);
            }
            
            
            builder.HasIndex(x => x.Id).IsUnique();
        }
    }
}