using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Upstart.Domain.Interfaces.ExternalServices;
using Upstart.Domain.VO;
using Upstart.Infrastructure.Model;

namespace Upstart.Infrastructure.ExternalServices
{
    public class GeoCensusService : IGeoLocationService
    {
        static readonly HttpClient client = new HttpClient();
        const string BASE_ADDRESS = "https://geocoding.geo.census.gov/geocoder";

        public async Task<CoordinatesVO> GetCoordinatesByAddressAsync(AddressVO address)
        {
            var response = await client.GetAsync($"{BASE_ADDRESS}/locations/address?street={address.Street}&city={address.City}&state={address.State}&zip={address.ZipCode}&benchmark=Public_AR_Census2020&format=json");

            if(response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var addressInfo = JsonConvert.DeserializeObject<Root>(await response.Content.ReadAsStringAsync());

                var coordinates = new CoordinatesVO();
                coordinates.Latitude = addressInfo.Result.AddressMatches[0].Coordinates.X;
                coordinates.Longitude = addressInfo.Result.AddressMatches[0].Coordinates.Y;
                return coordinates;
            }
            else
            {
                throw new Exception("Error trying to get coordinate");
            }
        }
    }
}
