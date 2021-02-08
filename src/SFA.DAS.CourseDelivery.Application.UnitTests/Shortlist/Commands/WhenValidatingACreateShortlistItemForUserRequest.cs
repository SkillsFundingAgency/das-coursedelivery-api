using System;
using System.Threading.Tasks;
using AutoFixture.NUnit3;
using FluentAssertions;
using NUnit.Framework;
using SFA.DAS.CourseDelivery.Application.Shortlist.Commands.CreateShortlistItemForUser;

namespace SFA.DAS.CourseDelivery.Application.UnitTests.Shortlist.Commands
{
    public class WhenValidatingACreateShortlistItemForUserRequest
    {
        [Test, AutoData]
        public async Task Then_If_All_Required_Fields_Are_Populated_Then_Request_Is_Valid(
            CreateShortlistItemForUserRequest request,
            CreateShortlistItemForUserValidator validator)
        {
            //Act
            var actual = await validator.ValidateAsync(request);
            
            //Assert
            actual.IsValid().Should().BeTrue();
        }

        [Test, AutoData]
        public async Task Then_If_The_ShortlistId_Is_Empty_Guid_Then_The_Request_Is_Invalid(
            CreateShortlistItemForUserRequest request,
            CreateShortlistItemForUserValidator validator)
        {
            //Arrange
            request.ShortlistUserId = Guid.Empty;
            
            //Act
            var actual = await validator.ValidateAsync(request);
            
            //Assert
            actual.IsValid().Should().BeFalse();
            actual.ValidationDictionary.Should().ContainKey(nameof(request.ShortlistUserId));
        }

        [Test, AutoData]
        public async Task Then_If_The_CourseId_Is_Zero_Then_The_Request_Is_Invalid(
            CreateShortlistItemForUserRequest request,
            CreateShortlistItemForUserValidator validator)
        {
            //Arrange
            request.CourseId = 0;
            
            //Act
            var actual = await validator.ValidateAsync(request);
            
            //Assert
            actual.IsValid().Should().BeFalse();
            actual.ValidationDictionary.Should().ContainKey(nameof(request.CourseId));
        }
        
        [Test, AutoData]
        public async Task Then_If_The_Level_Is_Zero_Then_The_Request_Is_Invalid(
            CreateShortlistItemForUserRequest request,
            CreateShortlistItemForUserValidator validator)
        {
            //Arrange
            request.Level = 0;
            
            //Act
            var actual = await validator.ValidateAsync(request);
            
            //Assert
            actual.IsValid().Should().BeFalse();
            actual.ValidationDictionary.Should().ContainKey(nameof(request.Level));
        }
        
        [Test, AutoData]
        public async Task Then_If_The_ProviderUkprn_Is_Zero_Then_The_Request_Is_Invalid(
            CreateShortlistItemForUserRequest request,
            CreateShortlistItemForUserValidator validator)
        {
            //Arrange
            request.ProviderUkprn = 0;
            
            //Act
            var actual = await validator.ValidateAsync(request);
            
            //Assert
            actual.IsValid().Should().BeFalse();
            actual.ValidationDictionary.Should().ContainKey(nameof(request.ProviderUkprn));
        }
        
        [Test, AutoData]
        public async Task Then_If_The_SubjectSectorArea_Is_Empty_Then_The_Request_Is_Invalid(
            CreateShortlistItemForUserRequest request,
            CreateShortlistItemForUserValidator validator)
        {
            //Arrange
            request.SectorSubjectArea = string.Empty;
            
            //Act
            var actual = await validator.ValidateAsync(request);
            
            //Assert
            actual.IsValid().Should().BeFalse();
            actual.ValidationDictionary.Should().ContainKey(nameof(request.SectorSubjectArea));
        }
        
        [Test, AutoData]
        public async Task Then_If_The_SubjectSectorArea_Is_Null_Then_The_Request_Is_Invalid(
            CreateShortlistItemForUserRequest request,
            CreateShortlistItemForUserValidator validator)
        {
            //Arrange
            request.SectorSubjectArea = null;
            
            //Act
            var actual = await validator.ValidateAsync(request);
            
            //Assert
            actual.IsValid().Should().BeFalse();
            actual.ValidationDictionary.Should().ContainKey(nameof(request.SectorSubjectArea));
        }
    }
}