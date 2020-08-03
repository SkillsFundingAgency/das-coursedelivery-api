using System.Linq;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;

namespace SFA.DAS.CourseDelivery.Domain.UnitTests.Entities
{
    public class WhenCastingFromProviderStandardApiTypeToProviderStandardImport
    {
        [Test, AutoData]
        public void Then_Maps_The_Fields(ImportTypes.Provider provider)
        {
            var courseStandard = provider.Standards.FirstOrDefault();
            
            var actual = new ProviderStandardImport().Map(courseStandard,provider.Ukprn);

            actual.Ukprn.Should().Be(provider.Ukprn);
            actual.StandardInfoUrl.Should().Be(courseStandard.StandardInfoUrl);
            actual.StandardId.Should().Be(courseStandard.StandardCode);
            actual.Email.Should().Be(courseStandard.Contact.Email);
            actual.Phone.Should().Be(courseStandard.Contact.Phone);
            actual.ContactUrl.Should().Be(courseStandard.Contact.ContactUsUrl);
        }
    }
}