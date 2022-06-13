using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Paymentsense.Coding.Challenge.Api.Services;

namespace Paymentsense.Coding.Challenge.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PaymentsenseCodingChallengeController : ControllerBase
    {
        private readonly CountryService _countryService;

        public PaymentsenseCodingChallengeController(CountryService countryService)
        {
            _countryService = countryService;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var countries = await _countryService.GetCountriesAsync();

            return Ok(countries);
        }

        [HttpGet("{Alpha2Code}")]
        public async Task<IActionResult> GetByAlpha2Code(string alpha2Code)
        {
            // return Ok($"hello {alpha2Code}");
            var country = await _countryService.GetCountryByAlpha2CodeAsync(alpha2Code);

            return Ok(country);
        }
    }
}
