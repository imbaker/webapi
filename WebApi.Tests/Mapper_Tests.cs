using AutoMapper;
using FluentAssertions;
using NSubstitute;
using System.Collections.Generic;
using WebApi.Mappers;
using WebApi.Models;
using WebApi.Models.Domain;
using WebApi.Models.Dto;
using WebApi.Models.Mappers;
using Xunit;

namespace WebApi.Tests
{
    public class Mapper_Tests
    {
        [Fact]
        public void Mapper_WhenMapperIsInitiated_ConfigurationIsValid()
        {
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PolicyProfile>());

            config.AssertConfigurationIsValid();
        }

        [Fact]
        public void Mapper_WhenMappingDto_MapsAllValuesCorrectly()
        {
            // Arrange
            var policyDto = new PolicyDto
            {
                Firstname = "Firstname",
                Surname = "Surname",
                Id = 1,
                PolicyNo = "PolicyNo",
                LastUpdated = new System.DateTime(2020, 7, 1),
                Addresses = new List<AddressDto>
                {
                    new AddressDto() { Line1 = "Line1.1", Postcode = "Postcode.1" },
                    new AddressDto() { Line1 = "Line1.2", Postcode = "Postcode.2" }
                }
            };
            var config = new MapperConfiguration(cfg => cfg.AddProfile<PolicyProfile>());
            var mapper = config.CreateMapper();

            // Act
            var sut = mapper.Map<PolicyDto, Policy>(policyDto);

            // Assert
            Assert.Equal("Firstname", sut.Firstname);
            Assert.Equal("Surname", sut.Surname);
            Assert.Null(sut.Fullname);
            Assert.Equal(2, sut.Addresses.Count);
            Assert.Collection(
                sut.Addresses,
                first =>
                {
                    Assert.Equal("Line1.1", first.Line1);
                    Assert.Equal("Postcode.1", first.Postcode);
                },
                second =>
                {
                    Assert.Equal("Line1.2", second.Line1);
                    Assert.Equal("Postcode.2", second.Postcode);
                }
            );
        }

        [Fact]
        public void Mapper_WhenMappingPolicyTransaction_MapsAllValuesCorrectly()
        {
            // Arrange
            var mapper = new PolicyTransactionMapper();

            var address = new AddressDto() { Line1 = "Line1", Line2 = "Line2", Town = "Town" };

            var modelFrom = new PolicyTransaction() { PolicyId = "PolicyId" };
            modelFrom.Addresses.Add(address);

            Models.Domain.IPolicyTransaction modelTo = Substitute.For<Models.Domain.IPolicyTransaction>();
            modelTo.Addresses = new List<Address>();

            // Act
            mapper.Map(modelFrom, modelTo);

            // Assert
            Assert.Equal("PolicyId", modelTo.PolicyId);
            Assert.Single(modelTo.Addresses);
            Assert.Collection(modelTo.Addresses,
                first =>
                {
                    Assert.Equal("Line1", first.Line1);
                    Assert.Equal("Line2", first.Line2);
                    Assert.Equal("Town", first.Town);
                });
        }

        [Fact]
        public void Mapper_WhenMappingPolicyTransaction_MapsAllValuesCorrectly_Fluent()
        {
            // Arrange
            var mapper = new PolicyTransactionMapper();

            var address = new AddressDto() { Line1 = "Line1", Line2 = "Line2", Line3 = "Line3", Town = "Town" };

            var modelFrom = new PolicyTransaction() { PolicyId = "PolicyId" };
            modelFrom.Addresses.Add(address);

            Models.Domain.IPolicyTransaction modelTo = Substitute.For<Models.Domain.IPolicyTransaction>();
            modelTo.Addresses = new List<Address>();

            // Act
            mapper.Map(modelFrom, modelTo);

            // Assert
            modelTo.Addresses.Should().SatisfyRespectively(
                first =>
                {
                    first.Line1.Should().Be("Line1");
                    first.Line2.Should().Be("Line2");
                    first.Line3.Should().BeNull();
                    first.Town.Should().Be("Town");
                });
        }
    }
}