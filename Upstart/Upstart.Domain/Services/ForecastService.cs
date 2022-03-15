using System.Threading.Tasks;
using Upstart.Domain.Interfaces.ExternalServices;
using Upstart.Domain.Interfaces.Services;
using Upstart.Domain.VO;

namespace Upstart.Domain.Services
{
    public class ForecastService : IForecastService
    {
        private readonly IGeoLocationService _geoLocationService;
        private readonly IExternalForecastService _forecastService;

        public ForecastService(IGeoLocationService geoLocationService, IExternalForecastService forecastService)
        {
            _geoLocationService = geoLocationService;
            _forecastService = forecastService;
        }

        public async Task<ForecastVO> GetForecast(string street, string city, string state, string zipcode)
        {
            var address = new AddressVO(street, city, state, zipcode);

            var coordinates = await _geoLocationService.GetCoordinatesByAddressAsync(address);
            var forecastPeriod = await _forecastService.GetForecast(coordinates.Latitude, coordinates.Longitude);

            return forecastPeriod;
        }
    }
}
