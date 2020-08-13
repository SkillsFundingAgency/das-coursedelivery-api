using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using CsvHelper;
using SFA.DAS.CourseDelivery.Domain.Interfaces;

namespace SFA.DAS.CourseDelivery.Infrastructure.StreamHelper
{
    public class ZipArchiveHelper : IZipArchiveHelper
    {
        public IEnumerable<T> ExtractModelFromCsvFileZipStream<T>(Stream stream, string filePath)
        {
            using(var zip = new ZipArchive(stream, ZipArchiveMode.Read, true))
            {
                var entry = zip.Entries.FirstOrDefault(m => m.FullName.EndsWith(filePath, StringComparison.CurrentCultureIgnoreCase));

                if (entry == null)
                {
                    return null;
                }

                using (var reader = new StreamReader(entry.Open())) 
                using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
                {
                    return csv.GetRecords<T>().ToList();
                }
            }
        }
    }
}