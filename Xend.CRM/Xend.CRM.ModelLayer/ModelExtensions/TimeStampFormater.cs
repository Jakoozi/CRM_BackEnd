using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.ModelLayer.ModelExtensions
{
    public static class TimeStampFormater
    {
        public static double ToTimeStamp(this DateTime dateInstance)
        {
            DateTime epochDateTime = new DateTime(1970, 1, 1);
            return (dateInstance - epochDateTime).TotalMilliseconds;
        }
    }
}
