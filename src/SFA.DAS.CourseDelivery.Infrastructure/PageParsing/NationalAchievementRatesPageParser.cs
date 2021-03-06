using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AngleSharp;
using AngleSharp.Dom;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.CourseDelivery.Infrastructure.Configuration;

namespace SFA.DAS.CourseDelivery.Infrastructure.PageParsing
{
    public class NationalAchievementRatesPageParser : INationalAchievementRatesPageParser
    {
        public async Task<string> GetCurrentDownloadFilePath()
        {
            var yearTo = DateTime.Today.Year;
            var yearFrom = DateTime.Today.AddYears(-1).Year;
            var config = AngleSharp.Configuration.Default.WithDefaultLoader();
            var context = BrowsingContext.New(config);
            IDocument document = null;
            var pageFound = false;
            while (!pageFound)
            {
                document = await context.OpenAsync(string.Format(Constants.NationalAchievementRatesPageUrl, yearFrom, yearTo));
                if (document.StatusCode != HttpStatusCode.NotFound)
                {
                    pageFound = true;
                }
                else
                {
                    yearTo--;
                    yearFrom--;    
                }
            }
            
            var downloadHref = document
                .QuerySelectorAll($"a:contains('{yearFrom} to {yearTo} apprenticeship NARTs overall CSV')")
                .First()
                .GetAttribute("Href");
            
            var uri = new Uri(downloadHref);

            return uri.AbsoluteUri;
        }
    }
}