using System;
using System.Linq;

namespace SampleBundleApi.Misc
{
    public static class StringHelper
    {
        public static bool IsDigitsOnly(string str)
        {
            return str.All(c => c >= '0' && c <= '9');
        }

        public static bool StringCompare(this string x, string y)
        {
            return string.Compare(x, y, StringComparison.OrdinalIgnoreCase) == 0;
        }
    }
}