using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.Common.ExtensionMethods
{
    public static class DoubleExtensionMethods
    {
        public static DateTime ToDateTime(this double milliseconds)
        {
            DateTime epochDateTime = new DateTime(1970, 1, 1);
            return epochDateTime.AddMilliseconds(milliseconds);
        }
    }
}
