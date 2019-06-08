using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Calculator;

namespace Calculator.Tests
{
    [TestClass]
    public class MyCalcTest
    {
        static void Compute(string line, double expectedValue)
        {
            var calc = new MyCalc();
            var result = calc.ComputeOfTwoValue(line);
            Assert.AreEqual(expectedValue, result);
        }
        [TestMethod]
        public void СomputeExpression1()
        {
            Compute("(2,2+3,8)*(9,8+0,2)/6+2", 12);
        }
        [TestMethod]
        public void СomputeExpression2()
        {
            Compute("((2+3+7)*2)+2", 26);
        }
        [TestMethod]
        public void СomputeExpression3()
        {
            Compute("((3,784+1,216)/2,5)^4", 16);
        }
        [TestMethod]
        public void СomputeExpression4()
        {
            Compute("(((((2,2+3,3))))+15)", 20.5);
        }
        [TestMethod]
        public void СomputeExpression5()
        {
            Compute("((2+2)*0,1+0,1)/5", 0.1);
        }


    }
}
