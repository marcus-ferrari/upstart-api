using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Globalization;
using System.Net.Http;
using System.Threading.Tasks;
using Upstart.Domain.Interfaces.ExternalServices;
using Upstart.Domain.VO;
using Upstart.Infrastructure.Model;

namespace Upstart.Infrastructure.ExternalServices
{
    public class WeatherGovForecastService : IExternalForecastService
    {
        static readonly HttpClient client = new HttpClient();
        const string BASE_ADDRESS_POINTS = "https://api.weather.gov/points";
        const string USER_AGENT_VALUE = "UserAgent_MFerrari";

        private readonly IMapper _mapper;

        public WeatherGovForecastService(IMapper mapper)
        {
            _mapper = mapper;
        }
        public async Task<ForecastVO> GetForecast(decimal latitude, decimal longitude)
        {
            var forecastUrl = await GetForecastUrl(latitude, longitude);
            var forecastVOs = await GetForecastList(forecastUrl);

            return forecastVOs;
        }


        private async Task<ForecastVO> GetForecastList(string url)
        {
            var response = await client.GetAsync(url); 
            var content = await response.Content.ReadAsStringAsync();
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                ForecastPeriodModel forecastResponse = JsonConvert.DeserializeObject<ForecastPeriodModel>(content);
                ForecastVO forecastVO = _mapper.Map<ForecastVO>(forecastResponse.Properties);

                return forecastVO;
            }
            else
            {
                ForecastError error = JsonConvert.DeserializeObject<ForecastError>(content);
                throw new Exception($"Error: {error.Detail}");
            }
        }

        private async Task<string> GetForecastUrl(decimal latitude, decimal longitude)
        {
            var latitudeStr = latitude.ToString(new CultureInfo("en-US"));
            var longitudeStr = longitude.ToString(new CultureInfo("en-US"));

            client.DefaultRequestHeaders.UserAgent.ParseAdd(USER_AGENT_VALUE);
            var response = await client.GetAsync($"{BASE_ADDRESS_POINTS}/{longitudeStr},{latitudeStr}");
            var content = await response.Content.ReadAsStringAsync();

            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                var forecastResponse = JsonConvert.DeserializeObject<ForecastRoot>(content);
                var forecastUrl = forecastResponse.ForecastProperties.ForecastUrl;

                return forecastUrl;
            }
            else
            {
                ForecastError error = JsonConvert.DeserializeObject<ForecastError>(content);
                throw new Exception($"Error: {error.Detail}");
            }
        }
    }
}
