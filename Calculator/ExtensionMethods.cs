using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public static class ExtensionMethods
    {
        public static bool IsBracket(this char e)
        {
            return e == '(' || e == ')';
        }

        public static bool IsOpenBracket(this char e)
        {
            return e == '(';
        }

        public static bool IsNumber(this char e)
        {
            return char.IsNumber(e);
        }

        public static bool IsDecimal(this char e)
        {
            return e == ',' || char.IsNumber(e);
        }
    }
}
