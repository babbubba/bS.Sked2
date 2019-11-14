using System;
using System.Collections.Generic;
using System.Text;

namespace bS.Sked2.Shared
{
    public static class StringsExtensions
    {
        public static bool IsNullOrWhiteSpace(this string value)
        {
            return string.IsNullOrWhiteSpace(value);
        }
    }
}
