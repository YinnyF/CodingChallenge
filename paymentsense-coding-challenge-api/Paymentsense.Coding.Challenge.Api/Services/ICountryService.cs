using System.Collections.Generic;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.Services
{
    public interface ICountryService
    {
        Task<IList<Country>> GetCountriesAsync();
        Task<Country> GetCountryByAlpha2CodeAsync(string alpha2Code);
    }
}