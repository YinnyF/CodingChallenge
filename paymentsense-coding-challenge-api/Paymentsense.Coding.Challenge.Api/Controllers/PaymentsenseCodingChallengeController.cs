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
        public async Task<IActionResult> Get()
        {
            try
            {
                var countries = await _countryService.GetCountriesAsync();

                return Ok(countries);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }
        }

        [HttpGet("{Alpha2Code}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Country))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetByAlpha2Code(string alpha2Code)
        {
            // validate the Alpha2Code parameter - check if letters only, and 2 chars?
            if (alpha2Code.Length != 2 || !alpha2Code.All(Char.IsLetter))
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
