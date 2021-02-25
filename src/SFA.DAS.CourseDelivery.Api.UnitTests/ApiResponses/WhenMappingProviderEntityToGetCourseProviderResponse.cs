using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Api.ApiResponses;
using SFA.DAS.CourseDelivery.Domain.Entities;
using SFA.DAS.CourseDelivery.Domain.Models;
using SFA.DAS.Testing.AutoFixture;

namespace SFA.DAS.CourseDelivery.Api.UnitTests.ApiResponses
{
    public class WhenMappingProviderEntityToGetCourseProviderResponse
    {
        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Fields(ProviderLocation provider)
        {
            var actual = GetProviderDetailResponse.Map(provider);

            actual.Ukprn.Should().Be(provider.Ukprn);
            actual.Name.Should().Be(provider.Name);
            actual.TradingName.Should().Be(provider.TradingName);
            actual.ProviderAddress.Should().BeEquivalentTo(provider.Address);
            actual.Email.Should().Be(provider.Email);
            actual.Phone.Should().Be(provider.Phone);
            actual.ContactUrl.Should().Be(provider.ContactUrl);
            actual.StandardInfoUrl.Should().Be(provider.StandardInfoUrl);
        }

        [Test, RecursiveMoqAutoData]
        public void Then_The_Delivery_Modes_Are_Mapped_If_Available(ProviderLocation providerLocation)
        {
            var actual = GetProviderDetailResponse.Map(providerLocation);

            actual.DeliveryTypes.Count.Should().Be(providerLocation.DeliveryTypes.Count);
        }
        
        [Test, RecursiveMoqAutoData]
        public void Then_Only_All_Ages_Are_Returned(ProviderLocation provider)
        {
            //Arrange
            provider.AchievementRates = new List<AchievementRate>
            {
                new AchievementRate
                {
                    Age = Age.SixteenToEighteen,
                    ApprenticeshipLevel = ApprenticeshipLevel.AllLevels
                },
                new AchievementRate
                {
                    Age = Age.AllAges,
                    ApprenticeshipLevel = ApprenticeshipLevel.AllLevels
                },
                new AchievementRate
                {
                    Age = Age.AllAges,
                    ApprenticeshipLevel = ApprenticeshipLevel.Three
                }
            };
            
            //Act
            var actual = GetProviderDetailResponse.Map(provider, (short)Age.AllAges);
            
            //Assert
            actual.AchievementRates.Count.Should().Be(2);
        }

        [Test, RecursiveMoqAutoData]
        public void Then_Maps_Feedback_Ratings_And_Attributes(ProviderLocation providerLocation)
        {
            var actual = GetProviderDetailResponse.Map(providerLocation);

            actual.FeedbackRatings.Count.Should().Be(providerLocation.FeedbackRating.Count);
            actual.FeedbackAttributes.Count.Should().Be(providerLocation.FeedbackAttributes.Count);
        }
    }
}