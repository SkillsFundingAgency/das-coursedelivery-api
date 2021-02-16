using System;
using Microsoft.AspNetCore.Mvc;

namespace SFA.DAS.CourseDelivery.Api.ApiRequests
{
    public class GetProvidersByStandardRequest
    {
        
        [FromQuery] 
        public Age Age { get; set; } = 0;
        [FromQuery] 
        public Level Level { get; set; } = 0;
        [FromQuery] 
        public double? Lat { get; set; } = null;
        [FromQuery] 
        public double? Lon { get; set; } =  null;
        [FromQuery] 
        public SortOrder SortOrder { get; set; } = SortOrder.Distance;
        [FromQuery] 
        public string SectorSubjectArea { get; set; } = "";
        [FromQuery] 
        public Guid ShortlistUserId { get; set; } = Guid.Empty;
    }
}