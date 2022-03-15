using System.Collections.Generic;

namespace Upstart.Application.DTO
{
    public class ErrorDto
    {
        public int StatusCode { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}
