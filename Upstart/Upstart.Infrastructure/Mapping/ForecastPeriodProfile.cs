using AutoMapper;
using Upstart.Domain.VO;
using Upstart.Infrastructure.Model;

namespace Upstart.Infrastructure.Mapping
{
    public class ForecastPeriodProfile : Profile
    {
        public ForecastPeriodProfile()
        {
            CreateMap<ForecastPeriodProperties, ForecastVO>().ReverseMap();

            CreateMap<Period, ForecastPeriodVO>()
                .ForMember(x => x.Temperature, y =>  y.MapFrom(z => z.Temperature + "°" + z.TemperatureUnit))
                .ReverseMap();
        }
    }
}
