using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using Moq;
using Paymentsense.Coding.Challenge.Api.HttpClientServices;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.Services
{
    public class CountryServicesTests
    {
        private ICountryService _countryService;
        private Mock<ICountryClient> _countryClient;

        [Fact]
        public async Task GetCountriesAsync_CallsGetCountriesAsync()
        {
            // Arrange
            _countryClient = new Mock<ICountryClient>();
            _countryClient.Setup(s => s.GetCountriesAsync());

            _countryService = new CountryService(_countryClient.Object);

            // Act
            await _countryService.GetCountriesAsync();

            // Assert
            _countryClient.Verify(c => c.GetCountriesAsync());
        }

        [Fact]
        public async Task GetCountriesAsync_ClientReturnsListOfCountries_ReturnsListOfCountries()
        {
            // Arrange
            _countryClient = new Mock<ICountryClient>();

            IList<Country> fakeCountries = new List<Country>()
            {
                new Country() { Name = "Test1" },
                new Country() { Name = "Test2" },
                new Country() { Name = "Test3" }
            };

            _countryClient.Setup(s => s.GetCountriesAsync()).ReturnsAsync(fakeCountries);

            _countryService = new CountryService(_countryClient.Object);

            // Act
            var result = await _countryService.GetCountriesAsync();

            // Assert
            result.Should().BeOfType<List<Country>>().Which.Should().BeEquivalentTo(fakeCountries);
        }

        [Fact]
        public async Task GetCountryByAlpha2CodeAsync_CallsGetCountryByAlpha2CodeAsync()
        {
            // Arrange
            string alpha2Code = "";
            _countryClient = new Mock<ICountryClient>();
            _countryClient.Setup(s => s.GetCountryByAlpha2CodeAsync(alpha2Code));

            _countryService = new CountryService(_countryClient.Object);

            // Act
            await _countryService.GetCountryByAlpha2CodeAsync(alpha2Code);

            // Assert
            _countryClient.Verify(c => c.GetCountryByAlpha2CodeAsync(alpha2Code));
        }

        [Fact]
        public async Task GetCountryByAlpha2CodeAsync_ClientReturnsCountry_ReturnsCountry()
        {
            // Arrange
            _countryClient = new Mock<ICountryClient>();
            // fyi not concerned if code is valid or not here
            string validAlpha2Code = "GB";
            Country fakeCountry = new Country() { Name = "Test1" };

            _countryClient.Setup(s => s.GetCountryByAlpha2CodeAsync(validAlpha2Code)).ReturnsAsync(fakeCountry);

            _countryService = new CountryService(_countryClient.Object);

            // Act
            var result = await _countryService.GetCountryByAlpha2CodeAsync(validAlpha2Code);

            // Assert
            result.Should().BeOfType<Country>().Which.Should().BeEquivalentTo(fakeCountry);
        }
    }
}