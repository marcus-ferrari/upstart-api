using System.Threading.Tasks;
using Upstart.Domain.VO;

namespace Upstart.Domain.Interfaces.ExternalServices
{
    public interface IExternalForecastService
    {
        Task<ForecastVO> GetForecast(decimal latitude, decimal longitude);
    }
}
