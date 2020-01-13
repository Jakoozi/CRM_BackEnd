using System;
using System.Collections.Generic;
using System.Text;

namespace Xend.CRM.Common.ExtensionMethods
{
    public static class NotNullExtension
    {
        public static T NotNull<T>(this T @this) where T : class
        {
            if (@this == null)
            {
                string className = @this.GetType().Name;
                throw new InvalidOperationException($"{className} cannot be empty");
            }
            return @this;
        }
    }
}
