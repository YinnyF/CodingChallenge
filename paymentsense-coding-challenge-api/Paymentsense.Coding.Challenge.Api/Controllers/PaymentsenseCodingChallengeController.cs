using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Models;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class PaymentsenseCodingChallengeController : ControllerBase
    {
        private readonly ICountryService _countryService;

        public PaymentsenseCodingChallengeController(ICountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IList<Country>))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        // Not required
        // [ResponseCache(Duration = 60, Location = ResponseCacheLocation.Any, NoStore = false)]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryService.GetCountriesAsync();

            if (countries != null)
            {
                return Ok(countries);
            }

            return NotFound();

        }

        [HttpGet("{Alpha2Code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Country))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAlpha2Code(string alpha2Code)
        {
            if (alpha2Code.Length != 2 || !alpha2Code.All(char.IsLetter))
            {
                return BadRequest();
            }

            var country = await _countryService.GetCountryByAlpha2CodeAsync(alpha2Code);

            if (country == null)
            {
                return NotFound();
            }

            return Ok(country);
            // return Ok($"hello {alpha2Code}");
        }
    }
}
