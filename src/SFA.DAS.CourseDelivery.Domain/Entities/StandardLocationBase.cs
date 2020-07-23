namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class StandardLocationBase
    {
        public long LocationId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Phone { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Town { get; set; }
        public string Postcode { get; set; }
        public string County { get; set; }
        public double Lat { get; set; }
        public double Long { get; set; }
    }
}