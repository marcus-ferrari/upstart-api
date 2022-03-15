using System;
using System.Collections.Generic;

namespace Upstart.Infrastructure.Model
{
    public class Period
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsDaytime { get; set; }
        public int Temperature { get; set; }
        public string TemperatureUnit { get; set; }
        public string TemperatureTrend { get; set; }
        public string WindSpeed { get; set; }
        public string WindDirection { get; set; }
        public string Icon { get; set; }
        public string ShortForecast { get; set; }
        public string DetailedForecast { get; set; }
    }

    public class ForecastPeriodProperties
    {
        public DateTime GeneratedAt { get; set; }
        public List<Period> Periods { get; set; }
    }

    public class ForecastPeriodModel
    {
        public ForecastPeriodProperties Properties { get; set; }
    }

    public class ForecastError
    {
        public string CorrelationId { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public int Status { get; set; }
        public string Detail { get; set; }
        public string Instance { get; set; }
    }



}
