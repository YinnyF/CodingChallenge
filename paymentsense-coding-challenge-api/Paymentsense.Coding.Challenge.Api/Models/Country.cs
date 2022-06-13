using System.Collections;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Paymentsense.Coding.Challenge.Api.Models
{
    public class Country
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("alpha2Code")]
        public string Alpha2Code { get; set; }

        [JsonPropertyName("capital")]
        public string Capital { get; set; }

        [JsonPropertyName("population")]
        public long Population { get; set; }

        [JsonPropertyName("numericCode")]
        public int NumericCode { get; set; }

        [JsonPropertyName("currencies")]
        public IList<Currency>? Currencies { get; set; }

        [JsonPropertyName("languages")]
        public IList<Language>? Languages { get; set; }

        [JsonPropertyName("flag")]
        public string Flag { get; set; }
    }

    public class Currency
    {
        public string code { get; set; }
        public string name { get; set; }

        public string symbol { get; set; }
    }

    public class Language
    {
        public string name { get; set; }
    }

}