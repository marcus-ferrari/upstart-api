using Newtonsoft.Json;

namespace Upstart.Infrastructure.Model
{
    public class ForecastRoot
    {
        [JsonProperty("properties")]
        public ForecastProperties ForecastProperties { get; set; }
    }

    public class ForecastProperties
    {
        [JsonProperty("forecast")]
        public string ForecastUrl { get; set; }
    }
}
