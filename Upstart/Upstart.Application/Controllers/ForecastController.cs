using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Threading.Tasks;
using Upstart.Application.DTO;
using Upstart.Domain.Interfaces.Services;
using Upstart.Domain.VO;

namespace Upstart.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    [EnableCors("AnotherPolicy")]
    public class ForecastController : Controller
    {
        private readonly IForecastService _forecastService;

        public ForecastController(IForecastService forecastService)
        {
            _forecastService = forecastService;
        }

        /// <summary>
        /// Get forecast from the next 7 days
        /// </summary>
        /// <param name="street">Ex.: 4600 Silver Hill Rd</param>
        /// <param name="city">Ex.: Washington</param>
        /// <param name="state">Ex.: DC</param>
        /// <param name="zipcode">Ex.: 20233</param>
        /// <response code="200">Returns list of forecast</response>
        /// <response code="500">If address is incorrect or not available</response>
        [HttpGet("address")]
        [ProducesResponseType(typeof(ForecastVO), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(ErrorDto), (int)HttpStatusCode.InternalServerError)]
        public async Task<IActionResult> GetAsync(string street, string city, string state, string zipcode)
        {
            var forecastPeriod = await _forecastService.GetForecast(street, city, state, zipcode);
            return StatusCode((int)HttpStatusCode.OK, forecastPeriod);
        }
    }
}
