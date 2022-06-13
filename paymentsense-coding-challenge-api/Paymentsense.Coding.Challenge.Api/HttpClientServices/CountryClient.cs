﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.HttpClientServices
{
    public class CountryClient
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
        
            // TODO: other HTTP responses -  404 NotFound or perhaps there are no countries to show?
            if (response.StatusCode == HttpStatusCode.NotFound)
            {
        
            }
        
            if (response.StatusCode == HttpStatusCode.NoContent)
            {
        
            }
        
            var responseContent = await response.Content.ReadAsStringAsync();
        
            IList<Country> countries = JsonConvert.DeserializeObject<IList<Country>>(responseContent);

            return countries;
        }
    }
}