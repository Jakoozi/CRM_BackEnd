using System;
using System.Collections.Generic;
using System.Text;
using Xend.CRM.ModelLayer.Enums;

namespace Xend.CRM.ModelLayer.Exceptions
{
    public class ErrorDetails
    {
        public ResponseStatus Status { get; set; }
        public string Message { get; set; }
    }
}
