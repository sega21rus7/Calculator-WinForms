using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Calculator
{
    public class MyCalc
    {
        public string Line { get; private set; }
        public List<string> Expression { get; private set; }
        public BinaryOperations OperationForTwo { get; private set; }
        public UnaryOperations OperationForOne { get; private set; }

        public MyCalc()
        {
            OperationForOne = new UnaryOperations();
            OperationForTwo = new BinaryOperations();
            Expression = new List<string>();
        }

        public double ComputeOfOneValue(string line, string operationName)
        {
            var value = double.Parse(line);
            double result = 0;
            if (OperationForOne.Dictionary.ContainsKey(operationName))
            {
                result = OperationForOne.Dictionary[operationName](value);
                if (double.IsNaN(result))
                    throw new Exception("Результат - не число!");
            }
            return result;
        }

        public double ComputeOfTwoValue(string line)
        {
            Line = line;
            LineToExpression();
            ExpressionHandler();
            if (OperationForTwo.NumberStack.Count == 0) throw new Exception("Вы ничего не ввели!");
            return OperationForTwo.NumberStack.Pop();
        }

        private void LineToExpression()
        {
            Line = Regex.Replace(Line, @"[ \r\n\t]", "");
            var temporary = new StringBuilder();
            foreach (var item in Line) // по исходной строке
            {
                if (item.IsDecimal()) temporary.Append(item);
                else if (OperationForTwo.FuncDictionary.ContainsKey(item.ToString())
                    || item.IsBracket())
                {
                    if (temporary.ToString() != string.Empty)
                    {
                        Expression.Add(temporary.ToString());
                        temporary.Clear();
                    }
                    Expression.Add(item.ToString());
                }
                else throw new Exception("Введены неверные данные");
            }
            if (temporary.ToString() != string.Empty) Expression.Add(temporary.ToString());
        }

        private void ExpressionHandler()
        {
            char symbol;
            foreach (var item in Expression) // по списку
            {
                if (OperationForTwo.FuncDictionary.ContainsKey(item)) // операция
                    OperationHandler(item);
                else if (char.TryParse(item, out symbol) &&
                    char.Parse(item).IsBracket()) // скобка
                {
                    BracketHandler(item);
                }
                else // число
                    NumberHandler(item);
            }
            while (true)
            {
                if (OperationForTwo.OperationStack.Count > 0)
                        MakeOperation();
                else break;
            }
        }

        private void MakeOperation()
        {
            if (OperationForTwo.NumberStack.Count > 0 &&
                OperationForTwo.OperationStack.Count > 0)
            {
                var secondNumber = OperationForTwo.NumberStack.Pop();
                var firstNumber = OperationForTwo.NumberStack.Pop();
                var operation = OperationForTwo.OperationStack.Pop();
                var result = OperationForTwo.FuncDictionary[operation](firstNumber, secondNumber);
                OperationForTwo.NumberStack.Push(result);
            }
            else throw new Exception("Введены неверные данные");
        }

        private void OperationHandler(string item)
        {
            while (true)
            {
                if (OperationForTwo.OperationStack.Count == 0) // Если стек пуст
                {
                    OperationForTwo.OperationStack.Push(item);
                    break;
                }
                else // иначе
                {
                    var showTopItem = char.Parse(OperationForTwo.OperationStack.Peek());
                    if (showTopItem.IsBracket()) // если скобка на вершине стека
                    {
                        if (showTopItem.IsOpenBracket()) OperationForTwo.OperationStack.Push(item);
                        else MakeOperation();
                        break;
                    }
                    // если приоритет данной операции <= приоритету верхнего элемента стека
                    else if (OperationForTwo.PriorityDictionary[item] <=
                        OperationForTwo.PriorityDictionary[showTopItem.ToString()])
                    {
                        MakeOperation();
                    }
                    else // иначе
                    {
                        OperationForTwo.OperationStack.Push(item);
                        break;
                    }
                }
            }
        }

        private void BracketHandler(string item)
        {
            while (true)
            {
                if ((char.Parse(item).IsOpenBracket()))
                {
                    OperationForTwo.OperationStack.Push(item);
                    break;
                }
                else
                {
                    if (char.Parse(OperationForTwo.OperationStack.Peek()).IsOpenBracket())
                    {
                        OperationForTwo.OperationStack.Pop();
                        break;
                    }
                    else MakeOperation();
                }
            }
        }

        private void NumberHandler(string item)
        {
            var value = double.Parse(item);
            OperationForTwo.NumberStack.Push(value);
        }
    }
}
