using System;
using System.Collections.Generic;

namespace Upstart.Domain.Extensions
{
    public class CustomExceptionErrorList : Exception
    {
        public List<string> Errors { get; set; }

        public CustomExceptionErrorList(List<string> _errors)
        {
            Errors = _errors;
        }
    }
}
