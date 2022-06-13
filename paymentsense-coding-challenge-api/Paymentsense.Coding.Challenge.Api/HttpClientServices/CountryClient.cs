using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.HttpClientServices
{
    public class CountryClient : ICountryClient
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CountryClient(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public async Task<IList<Country>> GetCountriesAsync()
        {
            // make a HTTP GET request to "https://restcountries.com/v2/all"
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync("https://restcountries.com/v2/all");
        
            // other HTTP responses -  404 NotFound or perhaps there are no countries to show?
            if (response.StatusCode == HttpStatusCode.NotFound || response.StatusCode == HttpStatusCode.NoContent)
            {
                throw new ArgumentException();
            }

            var responseContent = await response.Content.ReadAsStringAsync();
        
            IList<Country> countries = JsonConvert.DeserializeObject<IList<Country>>(responseContent);

            return countries;
        }

        public async Task<Country> GetCountryByAlpha2CodeAsync(string alpha2Code)
        {
            var httpClient = _httpClientFactory.CreateClient();
            var response = await httpClient.GetAsync($"https://restcountries.com/v2/alpha/{alpha2Code}");

            // other HTTP responses -  404 NotFound
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
                return null;
            }

            var responseContent = await response.Content.ReadAsStringAsync();

            Country country = JsonConvert.DeserializeObject<Country>(responseContent);

            return country;
        }
    }
}