using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Upstart.Domain.Extensions;
using Upstart.Domain.Interfaces.ExternalServices;
using Upstart.Domain.Services;
using Upstart.Domain.VO;
using Xunit;

namespace Upstart.Tests.Services
{
    public class ForecastServiceTest
    {
        Mock<IGeoLocationService> _geoLocationServiceMock;
        Mock<IExternalForecastService> _externalForecastServiceMock;

        public ForecastServiceTest()
        {
            PrepareIGeoLocationServiceMock();
            PrepareIExternalForecastServiceMock();
        }

        private void PrepareIGeoLocationServiceMock()
        {
            var coordinates = new CoordinatesVO()
            {
                Latitude = 38.845985m,
                Longitude = -76.92744m
            };
            _geoLocationServiceMock = new Mock<IGeoLocationService>();
            _geoLocationServiceMock.Setup(x => x.GetCoordinatesByAddressAsync(It.IsAny<AddressVO>()))
                .Returns(Task.FromResult(coordinates));
        }

        private void PrepareIExternalForecastServiceMock()
        {
            ForecastVO forecastVO = new ForecastVO();
            _externalForecastServiceMock = new Mock<IExternalForecastService>();
            _externalForecastServiceMock.Setup(x => x.GetForecast(It.IsAny<decimal>(), It.IsAny<decimal>()))
                .Returns(Task.FromResult(forecastVO));
        }

        [Theory(DisplayName = "Should return forecast when all properties are correct")]
        [InlineData("4600 Silver Hill Rd", "Washington", "DC", "20233")]
        [InlineData("4600 Silver Hill Rd", "Washington", "DC", null)]
        public void ShouldReturnForecastWhenAllPropertiesAreCorrect(string street, string city, string state, string zipcode)
        {
            //arrange
            var forecastService = new ForecastService(_geoLocationServiceMock.Object, _externalForecastServiceMock.Object);

            //action
            var result = forecastService.GetForecast(street, city, state, zipcode).Result;

            //assert
            Assert.IsType<ForecastVO>(result);
        }


        [Fact(DisplayName = "Should throws exception when street is null async")]
        public async Task ShouldThrowsExceptionWhenStreetIsNullAsync()
        {
            //arrange
            var forecastService = new ForecastService(_geoLocationServiceMock.Object, _externalForecastServiceMock.Object);
            string street = null;
            string city = "Washington";
            string state = "DC";
            string zipcode = "20233";

            //action
            Func<Task> result = async () => await forecastService.GetForecast(street, city, state, zipcode);

            //assert
            await Assert.ThrowsAsync<CustomExceptionErrorList>(result);
        }

        [Fact(DisplayName = "Should throws exception when city is null async")]
        public async Task ShouldThrowsExceptionWhenCityIsNullAsync()
        {
            //arrange
            var forecastService = new ForecastService(_geoLocationServiceMock.Object, _externalForecastServiceMock.Object);
            string street = "4600 Silver Hill Rd";
            string city = null;
            string state = "DC";
            string zipcode = "20233";

            //action
            Func<Task> result = async () => await forecastService.GetForecast(street, city, state, zipcode);

            //assert
            await Assert.ThrowsAsync<CustomExceptionErrorList>(result);
        }

        [Fact(DisplayName = "Should throws exception when state is null async")]
        public async Task ShouldThrowsExceptionWhenStateIsNullAsync()
        {
            //arrange
            var forecastService = new ForecastService(_geoLocationServiceMock.Object, _externalForecastServiceMock.Object);
            string street = "4600 Silver Hill Rd";
            string city = "Washington";
            string state = null;
            string zipcode = "20233";

            //action
            Func<Task> result = async () => await forecastService.GetForecast(street, city, state, zipcode);

            //assert
            await Assert.ThrowsAsync<CustomExceptionErrorList>(result);
        }

        [Theory(DisplayName = "Should throws exception when state has different length async")]
        [InlineData("4600 Silver Hill Rd", "Washington", "DCT", "20233")]
        [InlineData("4600 Silver Hill Rd", "Washington", "D", "20233")]
        public async Task ShouldThrowsExceptionWhenStateHasDifferentLengthAsync(string street, string city, string state, string zipcode)
        {
            //arrange
            var forecastService = new ForecastService(_geoLocationServiceMock.Object, _externalForecastServiceMock.Object);

            //action
            Func<Task> result = async () => await forecastService.GetForecast(street, city, state, zipcode);

            //assert
            await Assert.ThrowsAsync<CustomExceptionErrorList>(result);
        }

    }
}
