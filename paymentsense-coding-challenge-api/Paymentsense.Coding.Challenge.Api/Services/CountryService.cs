﻿using System.Collections.Generic;
using Paymentsense.Coding.Challenge.Api.Models;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.HttpClientServices;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;

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

            return countriesGetResult;

            // return new List<Country>() { new Country() { Name = "Test" } };
        }

        public async Task<Country> GetCountryByAlpha2CodeAsync(string alpha2Code)
        {
            var countryGetResult = await _countryClient.GetCountryByAlpha2CodeAsync(alpha2Code);

            return countryGetResult;

            // return new Country() { Alpha2Code = alpha2Code };
        }
    }
}