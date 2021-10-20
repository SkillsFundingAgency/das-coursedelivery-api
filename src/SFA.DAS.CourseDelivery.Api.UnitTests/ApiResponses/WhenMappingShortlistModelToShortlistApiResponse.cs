using System.Collections.Generic;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Models;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingShortlistModelToShortlistApiResponse
    {
        [Test, AutoData]
        public void Then_The_Fields_Are_Correctly_Mapped(Shortlist source)
        {
            var actual = (GetShortlistItemResponse) source;
            
            actual.Id.Should().Be(source.Id);
            actual.ShortlistUserId.Should().Be(source.ShortlistUserId);
            actual.ProviderDetails.Should().BeEquivalentTo(GetProviderDetailResponse.Map(source.ProviderLocation));
            actual.CourseId.Should().Be(source.StandardId);
            actual.LocationDescription.Should().Be(source.LocationDescription);
            actual.CreatedDate.Should().Be(source.CreatedDate);
        }

        [Test, AutoData]
        public void Then_The_DeliveryTypes_Are_Filtered_If_The_Distance_Exceeds_The_Radius_And_There_Is_A_Location(
            Shortlist source,
            DeliveryType deliveryType1,
            DeliveryType deliveryType2,
            DeliveryType deliveryType3)
        {
            //Arrange
            deliveryType1.Radius = 5;
            deliveryType1.DistanceInMiles = 10;
            deliveryType2.Radius = 5;
            deliveryType2.DistanceInMiles = 4;
            deliveryType3.Radius = 5;
            deliveryType3.DistanceInMiles = 4;
            source.ProviderLocation.DeliveryTypes = new List<DeliveryType>
            {
                deliveryType1,
                deliveryType2,
                deliveryType3
            };
            
            //Act
            var actual = (GetShortlistItemResponse) source;
            
            //Assert
            actual.ProviderDetails.DeliveryTypes.Should().BeEquivalentTo(new List<DeliveryType>
            {
                deliveryType2,
                deliveryType3
            });
        }

        [Test, AutoData]
        public void Then_If_There_Are_No_DeliveryModes_After_Filtering_And_There_Is_A_Location_Then_Not_Found_Delivery_Mode_Added(
            Shortlist source,
            DeliveryType deliveryType1)
        {
            //Arrange
            deliveryType1.Radius = 5;
            deliveryType1.DistanceInMiles = 10;
            source.ProviderLocation.DeliveryTypes = new List<DeliveryType>
            {
                deliveryType1
            };
            
            //Act
            var actual = (GetShortlistItemResponse) source;
            
            //Assert
            actual.ProviderDetails.DeliveryTypes.Should().BeEquivalentTo(new List<DeliveryType>
            {
                new DeliveryType
                {
                    DeliveryModes = "NotFound"
                }
            });
        }

        [Test, AutoData]
        public void Then_If_No_Location_Then_Delivery_Modes_Are_Not_Filtered(
            Shortlist source,
            DeliveryType deliveryType1,
            DeliveryType deliveryType2,
            DeliveryType deliveryType3)
        {
            //Arrange
            source.LocationDescription = string.Empty;
            deliveryType1.Radius = 5;
            deliveryType1.DistanceInMiles = 10;
            source.ProviderLocation.DeliveryTypes = new List<DeliveryType>
            {
                deliveryType1,
                deliveryType2,
                deliveryType3
            };
            
            //Act
            var actual = (GetShortlistItemResponse) source;
            
            //Assert
            actual.ProviderDetails.DeliveryTypes.Should().BeEquivalentTo(new List<DeliveryType>
            {
                deliveryType1,
                deliveryType2,
                deliveryType3
            });
        }
    }
}