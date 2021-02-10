using FluentAssertions;
using Flurl.Http.Testing;
using NSubstitute;
using System.Net.Http;
using WebApi.Models;
using WebApi.Models.Domain;
using WebApi.Models.Domain.Guardian;
using WebApi.Repositories;
using Xunit;

namespace WebApi.Tests
{
    public class Repository_Tests
    {
        private HttpTest httpTest;

        public Repository_Tests()
        {
            httpTest = new HttpTest();
        }

        [Fact]
        public void Repository_GetPolicyWithValidParam_ReturnsAValue()
        {
            // Arrange
            var repository = new JsonRepository();

            // Act
            var sut = repository.GetPolicy(1);

            // Assert
            sut.Id.Should().Be(1);
            sut.Firstname.Should().Be("Ian");
            sut.Surname.Should().Be("Baker");
        }

        [Fact]
        public void Repository_GetPolicyWithInvalidParam_ReturnsMull()
        {
            // Arrange
            var repository = new JsonRepository();

            // Act
            var sut = repository.GetPolicy(10);

            // Assert
            sut.Should().Be(null);
        }

        [Fact]
        public void Repository_AddPolicy_ReturnsPositiveId()
        {
            // Arrange
            var repository = new JsonRepository();
            var policy = new Policy() { Firstname = "First", Surname = "Surname" };

            // Act
            var sut = repository.AddPolicy(policy);

            // Assert
            sut.Should().BePositive();
        }

        [Fact]
        public void Repository_AddPolicy_ValueIsStored()
        {
            // Arrange
            var repository = new JsonRepository();
            var policy = new Policy() { Firstname = "First", Surname = "Surname" };
            var index = repository.AddPolicy(policy);

            // Act
            var sut = repository.GetPolicy(index);

            // Assert
            sut.Firstname.Should().Be("First");
            sut.Surname.Should().Be("Surname");
        }

        [Theory]
        [InlineData("None", 0)]
        [InlineData("Policy", 2)]
        [InlineData("Unexpected", -1)]
        public void Repository_MatchingSection_ReturnsCorrectValue(string param, int result)
        {
            // Arrange

            // Act
            var sut = JsonRepository.TryMapTaskSection(param);

            // Assert
            sut.Should().Be(result);
        }

        [Fact]
        public void Repository_CheckGet_ReturnsCorrectValues()
        {
            // Arrange
            var repository = new ConcertRepository();

            // Act
            var sut = repository.GetById(1);

            // Assert
            sut.Id.Should().Be(1);
        }

        [Fact]
        public void Repository_CheckGet_ReturnsNullIfIdIsInvalid()
        {
            // Arrange
            var repository = new ConcertRepository();

            // Act
            var sut = repository.GetById(100);

            // Assert
            sut.Should().BeNull();
        }

        [Fact]
        public void Repository_AddItem_SetsIdCorrectly()
        {
            // Arrange
            var repository = new ConcertRepository();
            var concert = new Concert()
            {
                Artist = "New Artist",
            };

            // Act
            var sut = repository.Insert(concert);

            // Assert
            sut.Should().Be(5);
        }

        [Fact]
        public void Repository_AddItem_IsSuccessfullySaved()
        {
            // Arrange
            var repository = new ConcertRepository();
            var concert = new Concert()
            {
                Artist = "New Artist",
            };
            var originalCount = repository.Count();

            // Act
            var result = repository.Insert(concert);

            // Assert
            repository.Count().Should().Be(originalCount + 1);
        }

        [Fact]
        public async void Repository_IdIsNotFound()
        {
            var repository = new NewsRepository();
            httpTest.RespondWith("error", 500);
            //httpTest.RespondWithJson(new Body() { Response = new Response() { Content = new Content() { ApiUrl = "ApiUrl" } } }, 200);

            var sut = await repository.Get("1");

            sut.Content.ApiUrl.Should().Be("xApiUrl");
        }
    }
}