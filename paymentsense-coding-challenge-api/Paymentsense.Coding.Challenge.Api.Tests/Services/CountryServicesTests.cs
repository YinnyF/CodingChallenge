using System.Threading.Tasks;
using Moq;
using Paymentsense.Coding.Challenge.Api.HttpClientServices;
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
    }
}