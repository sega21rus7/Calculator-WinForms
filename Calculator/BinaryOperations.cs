using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Calculator
{
    public class BinaryOperations
    {
        public Dictionary<string, Func<double, double, double>> FuncDictionary { get; private set; }
        public Dictionary<string, int> PriorityDictionary { get; private set; }
        public Stack<double> NumberStack { get; private set; }
        public Stack<string> OperationStack { get; private set; }

        public BinaryOperations()
        {
            CreateFunc();
            CreatePriority();
            NumberStack = new Stack<double>();
            OperationStack = new Stack<string>();
        }

        private void CreateFunc()
        {
            FuncDictionary = new Dictionary<string, Func<double, double, double>>()
            {
                {"+", (x, y) => x + y},
                {"-", (x, y) => x - y},
                {"*", (x, y) => x * y},
                {"/", (x, y) =>
                    {
                        if (y == 0) throw new DivideByZeroException();
                        return x / y;
                    }
                },
                {"^", (x, y) => Math.Pow(x, y)}
            };
        }

        private void CreatePriority()
        {
            PriorityDictionary = new Dictionary<string, int>()
            {
                {"+", 1},
                {"-", 1},
                {"*", 2},
                {"/", 2},
                {"^", 3}
            };

        }
    }
}
