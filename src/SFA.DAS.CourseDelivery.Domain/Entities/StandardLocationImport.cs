using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.Entities
{
    public class StandardLocationImport : StandardLocationBase
    {
        public static implicit operator StandardLocationImport(CourseLocation courseLocation)
        {
            return new StandardLocationImport
            {
                LocationId = courseLocation.Id,
                Name = courseLocation.Name,
                Email = courseLocation.Email,
                Phone = courseLocation.Phone,
                Website = courseLocation.Website,
                Address1 = courseLocation.Address.Address1,
                Address2 = courseLocation.Address.Address2,
                County = courseLocation.Address.County,
                Town = courseLocation.Address.Town,    
                Postcode = courseLocation.Address.Postcode,
                Lat = courseLocation.Address.Lat,
                Long = courseLocation.Address.Long
            }; 
                
        }
    }
}