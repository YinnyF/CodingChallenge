using System.Collections.Generic;
using Paymentsense.Coding.Challenge.Api.Models;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.HttpClientServices;
using System;

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

        public async Task<Country> GetCountryByAlpha2CodeAsync(string alpha2Code)
        {
            // TODO: validate the Alpha2Code parameter - check if letters only, and 2 chars?

            var countryGetResult = await _countryClient.GetCountryByAlpha2CodeAsync(alpha2Code);

            // TODO: check if the response fails (400 BadRequest)


            return countryGetResult;

            // return new Country() { Alpha2Code = alpha2Code };
        }
    }
}