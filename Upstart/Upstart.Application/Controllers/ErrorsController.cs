using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Upstart.Application.DTO;
using Upstart.Domain.Extensions;

namespace Upstart.Application.Controllers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    [AllowAnonymous]
    public class ErrorsController : ControllerBase
    {
        [Route("error")]
        public ErrorDto Error()
        {
            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();
            var exception = context.Error;
            var code = (int)HttpStatusCode.InternalServerError;
            Response.StatusCode = code;


            var returnException = new ErrorDto();
            returnException.StatusCode = code;

            if (exception is CustomExceptionErrorList)
            {
                var customException = (CustomExceptionErrorList)exception;
                returnException.Errors.AddRange(customException.Errors);
            }
            else
            {
                returnException.Errors.Add(exception.Message);
            }

            return returnException;
        }
    }
}
