using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using Moq;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.ProviderCourseImport.Services;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.ImportTypes;
using SFA.DAS.CourseDelivery.Domain.Interfaces;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.ProviderCourseImport.Services
{
    public class WhenImportingProviderLocationInformation
    {
        [Test, RecursiveMoqAutoData]
        public async Task Then_Postcodes_Are_Added_To_The_Providers_That_Are_Registered_And_Lat_Lon_Updated(
            List<Domain.Entities.Provider> providerEntities,
            string expectedPostcode,
            PostcodeLookup postcodeLookup,
            List<ContactDetail> contactDetails,
            [Frozen] Mock<IRoatpApiService> roatpApiService,
            [Frozen] Mock<IProviderRepository> providerRepository,
            [Frozen] Mock<IProviderRegistrationRepository> providerRegistrationRepository,
            [Frozen] Mock<IProviderRegistrationImportRepository> providerRegistrationImportRepository,
            [Frozen] Mock<IPostcodeApiService> postcodeApiService,
            ProviderRegistrationAddressImportService service)
        {
            //Arrange
            contactDetails = contactDetails.Select(c =>
            {
                c.ContactType = "P";
                c.ContactAddress.PostCode = expectedPostcode;
                return c;
            }).ToList();
            postcodeLookup.Result = postcodeLookup.Result.Select(c =>
            {
                c.Query = expectedPostcode;
                c.Result.Postcode = expectedPostcode;
                return c;
            }).ToList();
            var providerRegistrationLookup = new ProviderRegistrationLookup
            {
                Results = new List<ProviderResult>()
            };
            foreach (var provider in providerEntities)
            {
                providerRegistrationLookup.Results.Add(new ProviderResult
                {
                    Ukprn = provider.Ukprn,
                    ContactDetails = contactDetails
                });
            }
            providerRepository.Setup(x => x.GetAllRegistered()).ReturnsAsync(providerEntities);
            roatpApiService
                .Setup(x => x.GetProviderRegistrationLookupData(It.IsAny<List<int>>()))
                .ReturnsAsync(providerRegistrationLookup);
            postcodeApiService.Setup(x => x.GetPostcodeData(It.Is<PostcodeLookupRequest>(c=>c.Postcodes.TrueForAll(y=>y.Equals(expectedPostcode)))))
                .ReturnsAsync(postcodeLookup);
            
            //Act
            await service.ImportAddressData();

            //Assert
            foreach (var provider in providerRegistrationLookup.Results)
            {
                providerRegistrationImportRepository.Verify(x=>x.UpdateAddress(provider.Ukprn, It.Is<ContactAddress>(c=>c.PostCode == expectedPostcode), It.IsAny<double>(), It.IsAny<double>()), Times.Once());    
            }
            providerRegistrationRepository.Verify(x=>x.UpdateAddressesFromImportTable(), Times.Once);
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_If_No_Postcode_Data_Then_Lat_Lon_Are_Not_Updated(
            List<Domain.Entities.Provider> providerEntities,
            string expectedPostcode,
            PostcodeLookup postcodeLookup,
            List<ContactDetail> contactDetails,
            [Frozen] Mock<IRoatpApiService> roatpApiService,
            [Frozen] Mock<IProviderRepository> providerRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderRegistrationImportRepository> providerRegistrationImportRepository,
            [Frozen] Mock<IProviderRegistrationRepository> providerRegistrationRepository,
            [Frozen] Mock<IPostcodeApiService> postcodeApiService,
            ProviderRegistrationAddressImportService service)
        {
            //Arrange
            contactDetails = contactDetails.Select(c =>
            {
                c.ContactType = "P";
                c.ContactAddress.PostCode = expectedPostcode;
                return c;
            }).ToList();
            postcodeLookup.Result = postcodeLookup.Result.Select(c =>
            {
                c.Query = expectedPostcode;
                c.Result = null;
                return c;
            }).ToList();
            var providerRegistrationLookup = new ProviderRegistrationLookup
            {
                Results = new List<ProviderResult>()
            };
            foreach (var provider in providerEntities)
            {
                providerRegistrationLookup.Results.Add(new ProviderResult
                {
                    Ukprn = provider.Ukprn,
                    ContactDetails = contactDetails
                });
            }
            providerRepository.Setup(x => x.GetAllRegistered()).ReturnsAsync(providerEntities);
            roatpApiService
                .Setup(x => x.GetProviderRegistrationLookupData(It.IsAny<List<int>>()))
                .ReturnsAsync(providerRegistrationLookup);
            postcodeApiService.Setup(x => x.GetPostcodeData(It.Is<PostcodeLookupRequest>(c=>c.Postcodes.TrueForAll(y=>y.Equals(expectedPostcode)))))
                .ReturnsAsync(postcodeLookup);
            
            //Act
            await service.ImportAddressData();
            
            //Assert
            providerRegistrationImportRepository.Verify(x=>x.UpdateAddress(It.IsAny<int>(), It.Is<ContactAddress>(c=>c.PostCode == expectedPostcode), 0, 0), Times.Exactly(providerEntities.Count));
        }

        [Test, RecursiveMoqAutoData]
        public async Task Then_If_No_ProviderAdress_Then_Not_Updated(
            List<Domain.Entities.Provider> providerEntities,
            string expectedPostcode,
            PostcodeLookup postcodeLookup,
            List<ContactDetail> contactDetails,
            [Frozen] Mock<IRoatpApiService> roatpApiService,
            [Frozen] Mock<IProviderRepository> providerRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IProviderRegistrationImportRepository> providerRegistrationImportRepository,
            [Frozen] Mock<IPostcodeApiService> postcodeApiService,
            ProviderRegistrationAddressImportService service)
        {
            //Arrange
            contactDetails = contactDetails.Select(c =>
            {
                c.ContactType = "P";
                c.ContactAddress = null;
                return c;
            }).ToList();
            postcodeLookup.Result = postcodeLookup.Result.Select(c =>
            {
                c.Query = expectedPostcode;
                c.Result = null;
                return c;
            }).ToList();
            var providerRegistrationLookup = new ProviderRegistrationLookup
            {
                Results = new List<ProviderResult>()
            };
            foreach (var provider in providerEntities)
            {
                providerRegistrationLookup.Results.Add(new ProviderResult
                {
                    Ukprn = provider.Ukprn,
                    ContactDetails = contactDetails
                });
            }
            providerRepository.Setup(x => x.GetAllRegistered()).ReturnsAsync(providerEntities);
            roatpApiService
                .Setup(x => x.GetProviderRegistrationLookupData(It.IsAny<List<int>>()))
                .ReturnsAsync(providerRegistrationLookup);
            postcodeApiService.Setup(x => x.GetPostcodeData(It.Is<PostcodeLookupRequest>(c=>c.Postcodes.TrueForAll(y=>y ==(expectedPostcode)))))
                .ReturnsAsync(postcodeLookup);
            
            //Act
            await service.ImportAddressData();
            
            //Assert
            providerRegistrationImportRepository.Verify(x=>x.UpdateAddress(It.IsAny<int>(), It.IsAny<ContactAddress>(), It.IsAny<double>(), It.IsAny<double>()), Times.Never());
        }


        [Test, RecursiveMoqAutoData]
        public async Task Then_The_Audit_Record_Is_Updated(
            List<Domain.Entities.Provider> providerEntities,
            string expectedPostcode,
            PostcodeLookup postcodeLookup,
            List<ContactDetail> contactDetails,
            [Frozen] Mock<IRoatpApiService> roatpApiService,
            [Frozen] Mock<IProviderRepository> providerRepository,
            [Frozen] Mock<IProviderImportRepository> providerImportRepository,
            [Frozen] Mock<IPostcodeApiService> postcodeApiService,
            [Frozen] Mock<IImportAuditRepository> importAuditRepository,
            ProviderRegistrationAddressImportService service)
        {
            //Arrange
            contactDetails = contactDetails.Select(c =>
            {
                c.ContactType = "P";
                c.ContactAddress.PostCode = expectedPostcode;
                return c;
            }).ToList();
            postcodeLookup.Result = postcodeLookup.Result.Select(c =>
            {
                c.Query = expectedPostcode;
                c.Result.Postcode = expectedPostcode;
                return c;
            }).ToList();
            var providerRegistrationLookup = new ProviderRegistrationLookup
            {
                Results = new List<ProviderResult>()
            };
            foreach (var provider in providerEntities)
            {
                providerRegistrationLookup.Results.Add(new ProviderResult
                {
                    Ukprn = provider.Ukprn,
                    ContactDetails = contactDetails
                });
            }
            providerRepository.Setup(x => x.GetAllRegistered()).ReturnsAsync(providerEntities);
            roatpApiService
                .Setup(x => x.GetProviderRegistrationLookupData(It.IsAny<List<int>>()))
                .ReturnsAsync(providerRegistrationLookup);
            postcodeApiService.Setup(x => x.GetPostcodeData(It.Is<PostcodeLookupRequest>(c=>c.Postcodes.TrueForAll(y=>y.Equals(expectedPostcode)))))
                .ReturnsAsync(postcodeLookup);
            
            //Act
            await service.ImportAddressData();
            
            //Assert
            importAuditRepository.Verify(x=>x.Insert(
                It.Is<ImportAudit>(c=>
                    c.ImportType.Equals(ImportType.ProviderAddressData) 
                    && c.RowsImported.Equals(providerEntities.Count))), Times.Once);
        }
    }
}