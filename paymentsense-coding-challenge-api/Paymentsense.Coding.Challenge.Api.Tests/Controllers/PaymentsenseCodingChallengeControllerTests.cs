using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paymentsense.Coding.Challenge.Api.Controllers;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Controllers
{
    public class PaymentsenseCodingChallengeControllerTests
    {
        private PaymentsenseCodingChallengeController _controller;
        private Mock<ICountryService> _countryService;

        [Fact]
        public async Task Get_CountryServiceReturnsCountries_ReturnsOkWithResult()
        {
            // Arrange
            _countryService = new Mock<ICountryService>();

            IList<Country> fakeCountries = new List<Country>()
            {
                new Country() { Name = "Test1" },
                new Country() { Name = "Test2" },
                new Country() { Name = "Test3" }
            };

            _countryService.Setup(s => s.GetCountriesAsync()).ReturnsAsync(fakeCountries);

            _controller = new PaymentsenseCodingChallengeController(_countryService.Object);

            // Act
            var actionResult = await _controller.Get();

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(fakeCountries);
        }

        [Fact]
        public async Task Get_CountryServiceReturnsNull_ReturnsNotFound()
        {
            // Arrange
            _countryService = new Mock<ICountryService>();
            _countryService.Setup(s => s.GetCountriesAsync());

            _controller = new PaymentsenseCodingChallengeController(_countryService.Object);

            // Act
            var actionResult = await _controller.Get();

            // Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Theory]
        [InlineData("GBR")]
        [InlineData("G")]
        [InlineData("")]
        public async Task GetByAlpha2Code_WithCodeIncorrectLength_ReturnsBadRequest(string invalidCode)
        {
            // Arrange
            _countryService = new Mock<ICountryService>();
            _controller = new PaymentsenseCodingChallengeController(_countryService.Object);

            // Act
            var actionResult = await _controller.GetByAlpha2Code(invalidCode);

            // Assert
            actionResult.Should().BeOfType<BadRequestResult>();
        }

        [Theory]
        [InlineData("11")]
        [InlineData("a1")]
        [InlineData("1a")]
        [InlineData("&&")]
        public async Task GetByAlpha2Code_WithCodeIncorrectFormat_ReturnsBadRequest(string invalidCode)
        {
            // Arrange
            _countryService = new Mock<ICountryService>();
            _controller = new PaymentsenseCodingChallengeController(_countryService.Object);

            // Act
            var actionResult = await _controller.GetByAlpha2Code(invalidCode);

            // Assert
            actionResult.Should().BeOfType<BadRequestResult>();
        }

        [Fact]
        public async Task GetByAlpha2Code_CountryServiceReturnsNull_ReturnsNotFound()
        {
            // Arrange
            _countryService = new Mock<ICountryService>();
            // fyi not concerned if code is valid or not here
            string invalidAlpha2Code = "zz";

            // returns null when the method is called
            _countryService.Setup(s => s.GetCountryByAlpha2CodeAsync(invalidAlpha2Code));

            _controller = new PaymentsenseCodingChallengeController(_countryService.Object);

            // Act
            var actionResult = await _controller.GetByAlpha2Code(invalidAlpha2Code);

            // Assert
            actionResult.Should().BeOfType<NotFoundResult>();
        }

        [Fact]
        public async Task GetByAlpha2Code_CountryServiceReturnsCountry_ReturnsCountry()
        {
            // Arrange
            _countryService = new Mock<ICountryService>();
            // fyi not concerned if code is valid or not here
            string validAlpha2Code = "GB";
            Country fakeCountry = new Country() { Name = "Test1", Alpha2Code = validAlpha2Code };

            _countryService.Setup(s => s.GetCountryByAlpha2CodeAsync(validAlpha2Code)).ReturnsAsync(fakeCountry);

            _controller = new PaymentsenseCodingChallengeController(_countryService.Object);

            // Act
            var actionResult = await _controller.GetByAlpha2Code(validAlpha2Code);

            // Assert
            actionResult.Should().BeOfType<OkObjectResult>().Which.Value.Should().Be(fakeCountry);
        }
    }
}
