using System.Threading.Tasks;
using Upstart.Domain.VO;

namespace Upstart.Domain.Interfaces.Services
{
    public interface IForecastService
    {
        Task<ForecastVO> GetForecast(string street, string city, string state, string zipcode);
    }
}
