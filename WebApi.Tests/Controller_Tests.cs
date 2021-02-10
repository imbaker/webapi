using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using System.Net;
using System.Web.Http.Results;
using WebApi.Controllers;
using WebApi.Models.Domain;
using WebApi.Models.Domain.Guardian;
using WebApi.Models.Dto;
using WebApi.Repositories;
using Xunit;

namespace WebApi.Tests
{
    public class Controller_Tests
    {
        [Fact]
        public void Get_Returns100_IfIdEquals1()
        {
            var repository = Substitute.For<IJsonRepository>();
            var newsRepository = new NewsRepository();
            var mapper = Substitute.For<IMapper>();

            var controller = new PolicyController(repository, newsRepository, mapper);

            var policy = new Policy() { Id = 1, LastUpdated = new System.DateTime(2020, 1, 1) };

            var policyDto = new PolicyDto() { Id = 1, Firstname = "Ian", LastUpdated = new System.DateTime(2020, 01, 01) };

            repository.GetPolicy(1).Returns((Policy)null);
            repository.GetPolicy(Arg.Is<int>(x => x == 0)).Returns(policy);
            mapper.Map<Policy, PolicyDto>(policy).Returns<PolicyDto>(policyDto);
            mapper.Map<Policy, PolicyDto>(default).Returns<PolicyDto>((PolicyDto)null);

            // Act
            var sut = controller.GetConcert(1);

            // Assert
            sut.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public void Get_ReturnsBadRequest_IfIdEquals400()
        {
            var controller = new PolicyController();

            var result = controller.GetConcert(400) as StatusCodeResult;

            result.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        }

        [Fact]
        public void PassingTest()
        {
            Assert.Equal(4, 2 + 2);
        }

        [Fact]
        public void GetExample_WhenAssignedToIsInvalid_ReturnsBadRequest()
        {
            // Arrange
            var assignedTo = "Invalid";
            var controller = new PolicyController();

            // Act
            var response = controller.GetExample(1, assignedTo);
            var responseObject = response as BadRequestErrorMessageResult;

            // Assert
            response.Should().BeOfType(typeof(BadRequestErrorMessageResult));
            responseObject.Message.Should().Be($"Invalid assignedTo value: {assignedTo}");
        }

        [Theory]
        [InlineData("CurrentUser", "CurrentUser is set")]
        [InlineData("Workgroup", "Workgroup is set")]
        [InlineData("CurrentUser, Workgroup", "Both are set")]
        public void GetExample_WhenAssignedToIsValid_ReturnsContent(string assignedTo, string expectedResponse)
        {
            // Arrange
            var newsRepository = Substitute.For<INewsRepository>();
            newsRepository.GetExample(new List<string>()).Returns((Response)null);

            var controller = new PolicyController(null, newsRepository, null);

            // Act
            var response = controller.GetExample(1, assignedTo);
            var responseObject = response as OkNegotiatedContentResult<string>;

            // Assert
            response.Should().BeOfType(typeof(OkNegotiatedContentResult<string>));
            responseObject.Content.Should().Be(expectedResponse);
        }
    }
}