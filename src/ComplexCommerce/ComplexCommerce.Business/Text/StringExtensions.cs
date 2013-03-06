using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComplexCommerce.Business.Text
{
    public static class StringExtensions
    {
        public static bool ContainsUpper(this string text)
        {
            foreach (char c in text)
            {
                if (char.IsUpper(c)) return true;
            }
            return false;
        }

        public static bool ContainsLower(this string text)
        {
            foreach (char c in text)
            {
                if (char.IsLower(c)) return true;
            }
            return false;
        }
    }
}
