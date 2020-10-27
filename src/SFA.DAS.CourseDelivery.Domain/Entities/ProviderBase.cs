namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class ProviderBase
    {
        public long Id { get; set; }
        public int Ukprn { get; set; }
        public string Name { get; set; }
        public decimal? LearnerSatisfaction { get; set; }
        public decimal? EmployerSatisfaction { get; set; }
        public string TradingName { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Postcode { get ; set ; }
        public double Lat { get ; set ; }
        public double Long { get ; set ; }
    }
}