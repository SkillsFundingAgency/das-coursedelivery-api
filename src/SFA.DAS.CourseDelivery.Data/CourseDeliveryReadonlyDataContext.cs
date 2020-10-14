using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.CourseDelivery.Data.Configuration;
using SFA.DAS.CourseDelivery.Domain.Configuration;

namespace SFA.DAS.CourseDelivery.Data
{
    public interface ICourseDeliveryReadonlyDataContext : IDataContext
    {
        
    }
    
    public class CourseDeliveryReadonlyDataContext : DbContext, ICourseDeliveryReadonlyDataContext
    {
        public DbSet<Domain.Entities.Provider> Providers { get; set; }
        public DbSet<Domain.Entities.ProviderStandard> ProviderStandards { get; set; }
        public DbSet<Domain.Entities.ProviderStandardLocation> ProviderStandardLocations { get; set; }
        public DbSet<Domain.Entities.StandardLocation> StandardLocations { get; set; }
        public DbSet<Domain.Entities.ImportAudit> ImportAudit { get; set; }
        public DbSet<Domain.Entities.NationalAchievementRate> NationalAchievementRates { get; set; }
        public DbSet<Domain.Entities.NationalAchievementRateOverall> NationalAchievementRateOverall { get; set; }
        public DbSet<Domain.Entities.ProviderRegistration> ProviderRegistrations { get; set; }
        public DbSet<Domain.Entities.ProviderWithStandardAndLocation> ProviderWithStandardAndLocations { get; set; }
        public DbSet<Domain.Entities.ProviderRegistrationFeedbackRating> ProviderRegistrationFeedbackRatings { get; set; }
        
        private const string AzureResource = "https://database.windows.net/";
        private readonly CourseDeliveryConfiguration _configuration;
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider;

        public CourseDeliveryReadonlyDataContext ()
        {
            
        }
        
        public CourseDeliveryReadonlyDataContext(DbContextOptions options) : base(options)
        {
        }
        
        public CourseDeliveryReadonlyDataContext(IOptions<CourseDeliveryConfiguration> config, DbContextOptions options, AzureServiceTokenProvider azureServiceTokenProvider) :base(options)
        {
            _configuration = config.Value;
            _azureServiceTokenProvider = azureServiceTokenProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            
            if (_configuration == null || _azureServiceTokenProvider == null)
            {
                return;
            }
            
            var connection = new SqlConnection
            {
                ConnectionString = _configuration.ConnectionString,
                AccessToken = _azureServiceTokenProvider.GetAccessTokenAsync(AzureResource).Result
            };
            optionsBuilder.UseSqlServer(connection);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Provider());
            modelBuilder.ApplyConfiguration(new StandardLocation());
            modelBuilder.ApplyConfiguration(new ProviderStandard());
            modelBuilder.ApplyConfiguration(new ProviderStandardLocation());
            modelBuilder.ApplyConfiguration(new ImportAudit());
            modelBuilder.ApplyConfiguration(new NationalAchievementRate());
            modelBuilder.ApplyConfiguration(new NationalAchievementRateOverall());
            modelBuilder.ApplyConfiguration(new ProviderRegistration());
            modelBuilder.ApplyConfiguration(new ProviderWithStandardAndLocation());
            modelBuilder.ApplyConfiguration(new ProviderRegistrationFeedbackAttribute());
            modelBuilder.ApplyConfiguration(new ProviderRegistrationFeedbackRating());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}