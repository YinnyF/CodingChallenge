using System.Collections.Generic;
using Paymentsense.Coding.Challenge.Api.Models;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.HttpClientServices;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public class CountryService
    {
        private readonly CountryClient _countryClient;

        public CountryService(CountryClient countryClient)
        {
            _countryClient = countryClient;
        }

        public async Task<IList<Country>> GetCountriesAsync()
        {
            var countriesGetResult = await _countryClient.GetCountriesAsync();

            // TODO: if the response fails (404 NotFound) or (202 No Content)

            return countriesGetResult;

            // return new List<Country>() { new Country() { Name = "Test" } };
        }
    }
}