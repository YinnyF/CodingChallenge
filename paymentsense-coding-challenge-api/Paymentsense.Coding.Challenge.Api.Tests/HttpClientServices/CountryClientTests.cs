using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json;
using Paymentsense.Coding.Challenge.Api.HttpClientServices;
using Paymentsense.Coding.Challenge.Api.Models;
using Xunit;

namespace Paymentsense.Coding.Challenge.Api.Tests.HttpClientServices
{
    public class CountryClientTests
    {
        private Mock<FakeHttpMessageHandler> _fakeHttpMessageHandler;
        private Mock<IHttpClientFactory> _fakeHttpClientFactory;
        private HttpClient _httpClient;
        private HttpResponseMessage _fakeHttpResponseMessage;
        private ICountryClient _countryClient;

        [Fact]
        public async Task GetCountriesAsync_ResponseOk_ReturnsListOfCountries()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            string fakeResponseContent = "[{\"name\":\"Yemen\",\"topLevelDomain\":[\".ye\"],\"alpha2Code\":\"YE\",\"alpha3Code\":\"YEM\",\"callingCodes\":[\"967\"],\"capital\":\"Sana'a\",\"altSpellings\":[\"YE\",\"Yemeni Republic\",\"al-Jumhūriyyah al-Yamaniyyah\"],\"subregion\":\"Western Asia\",\"region\":\"Asia\",\"population\":29825968,\"latlng\":[15.0,48.0],\"demonym\":\"Yemeni\",\"area\":527968.0,\"gini\":36.7,\"timezones\":[\"UTC+03:00\"],\"borders\":[\"OMN\",\"SAU\"],\"nativeName\":\"اليَمَن\",\"numericCode\":\"887\",\"flags\":{\"svg\":\"https://flagcdn.com/ye.svg\",\"png\":\"https://flagcdn.com/w320/ye.png\"},\"currencies\":[{\"code\":\"YER\",\"name\":\"Yemeni rial\",\"symbol\":\"﷼\"}],\"languages\":[{\"iso639_1\":\"ar\",\"iso639_2\":\"ara\",\"name\":\"Arabic\",\"nativeName\":\"العربية\"}],\"translations\":{\"br\":\"Iêmen\",\"pt\":\"Iémen\",\"nl\":\"Jemen\",\"hr\":\"Jemen\",\"fa\":\"یمن\",\"de\":\"Jemen\",\"es\":\"Yemen\",\"fr\":\"Yémen\",\"ja\":\"イエメン\",\"it\":\"Yemen\",\"hu\":\"Jemen\"},\"flag\":\"https://flagcdn.com/ye.svg\",\"regionalBlocs\":[{\"acronym\":\"AL\",\"name\":\"Arab League\",\"otherNames\":[\"جامعة الدول العربية\",\"Jāmiʻat ad-Duwal al-ʻArabīyah\",\"League of Arab States\"]}],\"cioc\":\"YEM\",\"independent\":true},{\"name\":\"Zambia\",\"topLevelDomain\":[\".zm\"],\"alpha2Code\":\"ZM\",\"alpha3Code\":\"ZMB\",\"callingCodes\":[\"260\"],\"capital\":\"Lusaka\",\"altSpellings\":[\"ZM\",\"Republic of Zambia\"],\"subregion\":\"Eastern Africa\",\"region\":\"Africa\",\"population\":18383956,\"latlng\":[-15.0,30.0],\"demonym\":\"Zambian\",\"area\":752618.0,\"gini\":57.1,\"timezones\":[\"UTC+02:00\"],\"borders\":[\"AGO\",\"BWA\",\"COD\",\"MWI\",\"MOZ\",\"NAM\",\"TZA\",\"ZWE\"],\"nativeName\":\"Zambia\",\"numericCode\":\"894\",\"flags\":{\"svg\":\"https://flagcdn.com/zm.svg\",\"png\":\"https://flagcdn.com/w320/zm.png\"},\"currencies\":[{\"code\":\"ZMW\",\"name\":\"Zambian kwacha\",\"symbol\":\"ZK\"}],\"languages\":[{\"iso639_1\":\"en\",\"iso639_2\":\"eng\",\"name\":\"English\",\"nativeName\":\"English\"}],\"translations\":{\"br\":\"Zâmbia\",\"pt\":\"Zâmbia\",\"nl\":\"Zambia\",\"hr\":\"Zambija\",\"fa\":\"زامبیا\",\"de\":\"Sambia\",\"es\":\"Zambia\",\"fr\":\"Zambie\",\"ja\":\"ザンビア\",\"it\":\"Zambia\",\"hu\":\"Zambia\"},\"flag\":\"https://flagcdn.com/zm.svg\",\"regionalBlocs\":[{\"acronym\":\"AU\",\"name\":\"African Union\",\"otherNames\":[\"الاتحاد الأفريقي\",\"Union africaine\",\"União Africana\",\"Unión Africana\",\"Umoja wa Afrika\"]}],\"cioc\":\"ZAM\",\"independent\":true},{\"name\":\"Zimbabwe\",\"topLevelDomain\":[\".zw\"],\"alpha2Code\":\"ZW\",\"alpha3Code\":\"ZWE\",\"callingCodes\":[\"263\"],\"capital\":\"Harare\",\"altSpellings\":[\"ZW\",\"Republic of Zimbabwe\"],\"subregion\":\"Southern Africa\",\"region\":\"Africa\",\"population\":14862927,\"latlng\":[-20.0,30.0],\"demonym\":\"Zimbabwean\",\"area\":390757.0,\"gini\":50.3,\"timezones\":[\"UTC+02:00\"],\"borders\":[\"BWA\",\"MOZ\",\"ZAF\",\"ZMB\"],\"nativeName\":\"Zimbabwe\",\"numericCode\":\"716\",\"flags\":{\"svg\":\"https://flagcdn.com/zw.svg\",\"png\":\"https://flagcdn.com/w320/zw.png\"},\"currencies\":[{\"code\":\"ZMW\",\"name\":\"Zambian kwacha\",\"symbol\":\"K\"}],\"languages\":[{\"iso639_1\":\"en\",\"iso639_2\":\"eng\",\"name\":\"English\",\"nativeName\":\"English\"},{\"iso639_1\":\"sn\",\"iso639_2\":\"sna\",\"name\":\"Shona\",\"nativeName\":\"chiShona\"},{\"iso639_1\":\"nd\",\"iso639_2\":\"nde\",\"name\":\"Northern Ndebele\",\"nativeName\":\"isiNdebele\"}],\"translations\":{\"br\":\"Zimbabwe\",\"pt\":\"Zimbabué\",\"nl\":\"Zimbabwe\",\"hr\":\"Zimbabve\",\"fa\":\"زیمباوه\",\"de\":\"Simbabwe\",\"es\":\"Zimbabue\",\"fr\":\"Zimbabwe\",\"ja\":\"ジンバブエ\",\"it\":\"Zimbabwe\",\"hu\":\"Zimbabwe\"},\"flag\":\"https://flagcdn.com/zw.svg\",\"regionalBlocs\":[{\"acronym\":\"AU\",\"name\":\"African Union\",\"otherNames\":[\"الاتحاد الأفريقي\",\"Union africaine\",\"União Africana\",\"Unión Africana\",\"Umoja wa Afrika\"]}],\"cioc\":\"ZIM\",\"independent\":true}]";
            
            _fakeHttpResponseMessage = new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent($"{fakeResponseContent}")
            };

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            var result = await _countryClient.GetCountriesAsync();

            // Assert
            IList<Country> expectedCountries = JsonConvert.DeserializeObject<IList<Country>>(fakeResponseContent);
            result.Should().BeOfType<List<Country>>().Which.Should().BeEquivalentTo(expectedCountries);
        }

        [Fact]
        public void GetCountriesAsync_ResponseNotFound_ThrowsSystemException()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            _fakeHttpResponseMessage = new HttpResponseMessage {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent($"{null}")
            };

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            // var httpClient = _fakeHttpClientFactory.Object.CreateClient();
            // httpClient.Should().Be(_httpClient);

            // var httpResponseMessage = _fakeHttpMessageHandler.Object.Send(new HttpRequestMessage(new HttpMethod("get"), ""));
            // httpResponseMessage.StatusCode.Should().Be(HttpStatusCode.NotFound);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            Func<Task> act = async () => await _countryClient.GetCountriesAsync();
            
            // Assert
            act.Should().Throw<SystemException>();
        }

        [Fact]
        public void GetCountriesAsync_ResponseNoContent_ThrowsSystemException()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent($"{null}")
            };


            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            Func<Task> act = async () => await _countryClient.GetCountriesAsync();

            // Assert
            act.Should().Throw<SystemException>();
        }

        [Fact]
        public async Task GetCountryByAlpha2CodeAsync_ResponseOk_ReturnsCountry()
        {
            // Arrange
            string alpha2Code = "GB"; // arbitrary code, with correct format
            string fakeResponseContent = "{\"name\":\"United Kingdom of Great Britain and Northern Ireland\",\"topLevelDomain\":[\".uk\"],\"alpha2Code\":\"GB\",\"alpha3Code\":\"GBR\",\"callingCodes\":[\"44\"],\"capital\":\"London\",\"altSpellings\":[\"GB\",\"UK\",\"Great Britain\"],\"subregion\":\"Northern Europe\",\"region\":\"Europe\",\"population\":67215293,\"latlng\":[54.0,-2.0],\"demonym\":\"British\",\"area\":242900.0,\"gini\":35.1,\"timezones\":[\"UTC-08:00\",\"UTC-05:00\",\"UTC-04:00\",\"UTC-03:00\",\"UTC-02:00\",\"UTC\",\"UTC+01:00\",\"UTC+02:00\",\"UTC+06:00\"],\"borders\":[\"IRL\"],\"nativeName\":\"United Kingdom\",\"numericCode\":\"826\",\"flags\":{\"svg\":\"https://flagcdn.com/gb.svg\",\"png\":\"https://flagcdn.com/w320/gb.png\"},\"currencies\":[{\"code\":\"GBP\",\"name\":\"British pound\",\"symbol\":\"£\"}],\"languages\":[{\"iso639_1\":\"en\",\"iso639_2\":\"eng\",\"name\":\"English\",\"nativeName\":\"English\"}],\"translations\":{\"br\":\"Reino Unido\",\"pt\":\"Reino Unido\",\"nl\":\"Verenigd Koninkrijk\",\"hr\":\"Ujedinjeno Kraljevstvo\",\"fa\":\"بریتانیای کبیر و ایرلند شمالی\",\"de\":\"Vereinigtes Königreich\",\"es\":\"Reino Unido\",\"fr\":\"Royaume-Uni\",\"ja\":\"イギリス\",\"it\":\"Regno Unito\",\"hu\":\"Nagy-Britannia\"},\"flag\":\"https://flagcdn.com/gb.svg\",\"cioc\":\"GBR\",\"independent\":true}";
            
            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent($"{fakeResponseContent}")
            };

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            var result = await _countryClient.GetCountryByAlpha2CodeAsync(alpha2Code);

            // Assert
            Country expectedCountry = JsonConvert.DeserializeObject<Country>(fakeResponseContent);
            result.Should().BeOfType<Country>().Which.Should().BeEquivalentTo(expectedCountry);
        }

        [Fact]
        public async Task GetCountryByAlpha2CodeAsync_ResponseNotFound_ReturnsNull()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            string alpha2Code = "ZZ"; // arbitrary code, with correct format

            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent($"{null}")
            };

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            var result = await _countryClient.GetCountryByAlpha2CodeAsync(alpha2Code);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCountryByAlpha2CodeAsync_ResponseBadRequest_ReturnsNull()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            string alpha2Code = "";

            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.BadRequest,
                Content = new StringContent($"{null}")
            };

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            var result = await _countryClient.GetCountryByAlpha2CodeAsync(alpha2Code);

            // Assert
            result.Should().BeNull();
        }

        // TODO: Refactor client set up with parameters: StatusCode and fakeResponseContent
    }
}