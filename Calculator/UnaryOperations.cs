using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator
{
    public class UnaryOperations
    {
        public Dictionary<string, Func<double, double>> Dictionary { get; private set; }

        public UnaryOperations()
        {
            Dictionary = new Dictionary<string, Func<double, double>>()
            {
                {"±", x => -x},
                {"%", x => x / 100},
                {"√", x => Math.Sqrt(x)},
                {"sin", x => Math.Sin(x)},
                {"cos", x => Math.Cos(x)},
                {"tan", x => Math.Tan(x)},
                {"ln", x => Math.Log(x)},
                {"log", x => Math.Log10(x)},
                {"1/x", x =>
                    {
                        if(x == 0) throw new DivideByZeroException();
                        return 1/x;
                    }},
                {"eˣ", x => Math.Exp(x)},
                {"|x|", x => Math.Abs(x)},
                {"x!", x => HandleFactorial(x)},
                {"f:1", x => Math.Round(x, 1)},
                {"f:2", x => Math.Round(x, 2)},
                {"f:3", x => Math.Round(x, 3)},
            };
        }

        private double HandleFactorial(double x)
        {
            var intVariable = 0;
            var str = x.ToString();
            if (x < 0) throw new Exception("Нельзя взять факториал от числа < 0!");
            if (int.TryParse(str, out intVariable))
            {
                var result = Factorial(int.Parse(str));
                if (result == 0)
                    throw new Exception(
                        string.Format(
                        "Нельзя взять факториал от {0}, т.к. число слишком большое!",
                        x));
                return result;
            }
            else throw new Exception("Нельзя взять факториал от десятичной дроби!");
        }

        private int Factorial(int number)
        {
            var result = 1;
            if (number >= 0)
                for (int i = 1; i <= number; i++) result *= i;
            return result;
        }
    }
}
