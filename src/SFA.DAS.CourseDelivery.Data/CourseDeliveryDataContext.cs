using Microsoft.Azure.Services.AppAuthentication;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SFA.DAS.CourseDelivery.Data.Configuration;
using SFA.DAS.CourseDelivery.Domain.Configuration;
using ProviderImport = SFA.DAS.CourseDelivery.Data.Configuration.ProviderImport;
using StandardLocationImport = SFA.DAS.CourseDelivery.Data.Configuration.StandardLocationImport;
using ProviderStandardImport = SFA.DAS.CourseDelivery.Data.Configuration.ProviderStandardImport;
using ProviderStandardLocationImport = SFA.DAS.CourseDelivery.Data.Configuration.ProviderStandardLocationImport;

namespace SFA.DAS.CourseDelivery.Data
{
    public interface ICourseDeliveryDataContext
    {
        DbSet<Domain.Entities.ProviderImport> ProviderImports { get; set; }
        DbSet<Domain.Entities.ProviderStandardImport> ProviderStandardImports { get; set; }
        DbSet<Domain.Entities.ProviderStandardLocationImport> ProviderStandardLocationImports { get; set; }
        DbSet<Domain.Entities.StandardLocationImport> StandardLocationImports { get; set; }
        DbSet<Domain.Entities.Provider> Providers { get; set; }
        DbSet<Domain.Entities.ProviderStandard> ProviderStandards { get; set; }
        DbSet<Domain.Entities.ProviderStandardLocation> ProviderStandardLocations { get; set; }
        DbSet<Domain.Entities.StandardLocation> StandardLocations { get; set; }
        int SaveChanges();
    }

    public class CourseDeliveryDataContext: DbContext, ICourseDeliveryDataContext
    {
        public DbSet<Domain.Entities.ProviderImport> ProviderImports { get; set; }
        public DbSet<Domain.Entities.ProviderStandardImport> ProviderStandardImports { get; set; }
        public DbSet<Domain.Entities.ProviderStandardLocationImport> ProviderStandardLocationImports { get; set; }
        public DbSet<Domain.Entities.StandardLocationImport> StandardLocationImports { get; set; }
        public DbSet<Domain.Entities.Provider> Providers { get; set; }
        public DbSet<Domain.Entities.ProviderStandard> ProviderStandards { get; set; }
        public DbSet<Domain.Entities.ProviderStandardLocation> ProviderStandardLocations { get; set; }
        public DbSet<Domain.Entities.StandardLocation> StandardLocations { get; set; }

        private const string AzureResource = "https://database.windows.net/";
        private readonly CourseDeliveryConfiguration _configuration;
        private readonly AzureServiceTokenProvider _azureServiceTokenProvider;


        public CourseDeliveryDataContext()
        {
        }

        public CourseDeliveryDataContext(DbContextOptions options) : base(options)
        {
            
        }

        public CourseDeliveryDataContext(IOptions<CourseDeliveryConfiguration> config, DbContextOptions options, AzureServiceTokenProvider azureServiceTokenProvider) :base(options)
        {
            _configuration = config.Value;
            _azureServiceTokenProvider = azureServiceTokenProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
            
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
            modelBuilder.ApplyConfiguration(new ProviderImport());
            modelBuilder.ApplyConfiguration(new StandardLocationImport());
            modelBuilder.ApplyConfiguration(new ProviderStandardImport());
            modelBuilder.ApplyConfiguration(new ProviderStandardLocationImport());
            modelBuilder.ApplyConfiguration(new Provider());
            modelBuilder.ApplyConfiguration(new StandardLocation());
            modelBuilder.ApplyConfiguration(new ProviderStandard());
            modelBuilder.ApplyConfiguration(new ProviderStandardLocation());
            base.OnModelCreating(modelBuilder);
        }
    }
}