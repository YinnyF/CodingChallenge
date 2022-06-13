using System.Collections.Generic;
using System.Threading.Tasks;
using Paymentsense.Coding.Challenge.Api.Models;

namespace Paymentsense.Coding.Challenge.Api.HttpClientServices
{
    public interface ICountryClient
    {
        Task<IList<Country>> GetCountriesAsync();
        Task<Country> GetCountryByAlpha2CodeAsync(string alpha2Code);
    }
}