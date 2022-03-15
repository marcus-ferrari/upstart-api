using System;
using System.Collections.Generic;

namespace Upstart.Domain.VO
{
    public class ForecastVO
    {
        public DateTime GeneratedAt { get; set; }
        public List<ForecastPeriodVO> Periods { get; set; }
    }

    public class ForecastPeriodVO
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDaytime { get; set; }
        public string Temperature { get; set; }
        public object TemperatureTrend { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string Icon { get; set; }
        public string ShortForecast { get; set; }
        public string DetailedForecast { get; set; }

    }

}
