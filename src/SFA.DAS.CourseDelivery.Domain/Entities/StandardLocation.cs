namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class StandardLocation : StandardLocationBase
    {
        public virtual ProviderStandardLocation ProviderStandardLocation { get ; set ; }

        public static implicit operator StandardLocation(StandardLocationImport standardLocationImport)
        {
            return new StandardLocation
            {
                Address1 = standardLocationImport.Address1,
                Address2 = standardLocationImport.Address2,
                County = standardLocationImport.County,
                Email = standardLocationImport.Email,
                Lat = standardLocationImport.Lat,
                Long = standardLocationImport.Long,
                Name = standardLocationImport.Name,
                Phone = standardLocationImport.Phone,
                Postcode = standardLocationImport.Postcode,
                Town = standardLocationImport.Town,
                Website = standardLocationImport.Website,
                LocationId = standardLocationImport.LocationId
            };
        }
    }
}