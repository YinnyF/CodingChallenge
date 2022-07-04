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
        public async Task GetCountriesAsync_ResponseNotFound_ReturnsNull()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            _fakeHttpResponseMessage = new HttpResponseMessage {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(string.Empty)
            };

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            // Here we are mocking the http request by mocking the message handler
            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            var result = await _countryClient.GetCountriesAsync();
            
            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCountriesAsync_ResponseNoContent_ReturnsNull()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NoContent,
                Content = new StringContent(string.Empty)
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
            result.Should().BeNull();
        }

        [Fact]
        public void GetCountriesAsync_ResponseInternalServerError_ThrowsHttpRequestException()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(string.Empty)
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
            act.Should().Throw<HttpRequestException>();
        }

        [Fact]
        public async Task GetCountriesAsync_ResponseOk_ReturnsListOfCountries()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            // Make a list of Country objects and serialize it
            IList<Country> fakeCountries = new List<Country>()
            {
                new Country() { Name = "Test1" },
                new Country() { Name = "Test2" },
                new Country() { Name = "Test3" }
            };

            _fakeHttpResponseMessage = new HttpResponseMessage {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(fakeCountries))
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
            result.Should().BeOfType<List<Country>>().Which.Should().BeEquivalentTo(fakeCountries);
        }

        [Fact]
        public async Task GetCountryByAlpha2CodeAsync_ResponseNotFound_ReturnsNull()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            string alpha2Code = "ZZ"; // arbitrary code, with correct format

            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.NotFound,
                Content = new StringContent(string.Empty)
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
        public void GetCountryByAlpha2CodeAsync_ResponseInternalServerError_ThrowsHttpRequestException()
        {
            // Arrange - initialise objects, set behaviour of _fakeHttpMessageHandler
            string alpha2Code = "";

            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.InternalServerError,
                Content = new StringContent(string.Empty)
            };

            _fakeHttpMessageHandler = new Mock<FakeHttpMessageHandler> { CallBase = true };
            _fakeHttpMessageHandler.Setup(f => f.Send(It.IsAny<HttpRequestMessage>()))
                .Returns(_fakeHttpResponseMessage);

            _httpClient = new HttpClient(_fakeHttpMessageHandler.Object);

            _fakeHttpClientFactory = new Mock<IHttpClientFactory>();
            _fakeHttpClientFactory.Setup(s => s.CreateClient(It.IsAny<string>())).Returns(_httpClient);

            _countryClient = new CountryClient(_fakeHttpClientFactory.Object);

            // Act
            Func<Task> act = async () => await _countryClient.GetCountryByAlpha2CodeAsync(alpha2Code);

            // Assert
            act.Should().Throw<HttpRequestException>();
        }

        [Fact]
        public async Task GetCountryByAlpha2CodeAsync_ResponseOk_ReturnsCountry()
        {
            // Arrange
            string alpha2Code = "GB"; // arbitrary code, with correct format
            Country fakeCountry = new Country() { Name = "Test1", Alpha2Code = alpha2Code };
            
            _fakeHttpResponseMessage = new HttpResponseMessage
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(JsonConvert.SerializeObject(fakeCountry))
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
            result.Should().BeOfType<Country>().Which.Should().BeEquivalentTo(fakeCountry);
        }

        // TODO: Refactor mocked httpclient set up with parameters: StatusCode and fakeResponseContent
    }
}