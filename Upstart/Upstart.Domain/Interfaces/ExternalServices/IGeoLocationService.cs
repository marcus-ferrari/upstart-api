using System.Threading.Tasks;
using Upstart.Domain.VO;

namespace Upstart.Domain.Interfaces.ExternalServices
{
    public interface IGeoLocationService
    {
        Task<CoordinatesVO> GetCoordinatesByAddressAsync(AddressVO address);
    }
}
