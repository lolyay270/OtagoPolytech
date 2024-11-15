using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Calculator
{
    public class Calc
    {
        public Calc()
        {

        }

        public double Start()
        {
            Console.WriteLine("Enter the first number:");
            double firstNum = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the second number:");
            double secondNum = double.Parse(Console.ReadLine());

            Console.WriteLine("Enter the operator (+, -, *, /):");
            string op = Console.ReadLine();

            double result = 0;

            switch (op)
            {
                case "+":
                    result = Add(firstNum, secondNum);
                    break;
                case "-":
                    result = Subtract(firstNum, secondNum);
                    break;
                case "*":
                    result = Multiply(firstNum, secondNum);
                    break;
                case "/":
                    result = Divide(firstNum, secondNum);
                    break;
                default:
                    Console.WriteLine("Invalid operator.");
                    break;
            }

            return result;
        }

        public double Add(double firstNum, double secondNum)
        {
            return firstNum + secondNum;
        }

        public double Subtract(double firstNum, double secondNum)
        {
            return firstNum - secondNum;
        }

        public double Multiply(double firstNum, double secondNum)
        {
            return firstNum * secondNum;
        }

        public double Divide(double firstNum, double secondNum)
        {
            if (secondNum == 0)
            {
                Console.WriteLine("Cannot divide by zero");
                return 0;
            }
            else
            {
                return firstNum / secondNum;
            }
        }
    }

    //[TestClass]
    //public class CalcTests
    //{
    //    [TestMethod]
    //    public void AddReturnsSum()
    //    {
    //        Calc calc = new();
    //        double num1 = 3.5, num2 = 7.61, expected = 11.11;
    //        double actual = calc.Add(num1, num2);
    //        Assert.AreEqual(expected, actual);
    //    }

    //    [TestMethod]
    //    public void DivideReturnsDivided()
    //    {
    //        //24.5 / 3.5 = 7.0 
    //        Calc calc = new();
    //        double num1 = 24.5, num2 = 3.5, expected = 7.0;
    //        double actual = calc.Divide(num1, num2);
    //        Assert.AreEqual(expected , actual);
    //    }

    //    [TestMethod]
    //    public void DivideByZero()
    //    {
    //        Calc calc = new();
    //        double num1 = 3.0, num2 = 0.0, expected = 0.0;
    //        double actual = calc.Divide(num1, num2);
    //        Assert.AreEqual(expected, actual);
    //    }
    //}
}
