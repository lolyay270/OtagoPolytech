using System;
using System.Drawing;

namespace Prog2_Theory_Exam
{
    internal class Program
    {
        private static BankAccount bankAccount = new("100 - 000 - 000", "Jane Doe", 1000.50);
        static void Main(string[] args)
        {
            bankAccount.Deposit(500.00);
            Console.WriteLine($"New balance is {bankAccount.Balance:c2}");
            bankAccount.Withdraw(150.50);
            Console.WriteLine($"New balance is {bankAccount.Balance:c2}");


            List<string> iconicBrands = new List<string>
            {
                "Apple",
                "Coca-Cola", 
                "Nike",
                "McDonald's",
                "Google",
                "Amazon",
                "Microsoft"
            };

            var query1 = from brand in iconicBrands
                        where brand[0] == 'a' || brand[0] == 'A'
                        select brand;

            //var query2 = from employee in Employees
            //            where employee.department == "Engineering"
            //            select employee.salary;

            int n = 5, sum = 0;

            while (n > 0)
            {
                Console.WriteLine($"Countdown: {n}");
                n--;
            }
            Console.WriteLine(sum);
        }
    }

    public class BankAccount
    {
        //fields
        private string accountNumber, accountHolderName;
        private double balance;

        //constructor
        public BankAccount(string accountNumber, string accountHolderName, double balance)
        {
            this.accountNumber = accountNumber;
            this.accountHolderName = accountHolderName;
            this.balance = balance;
        }

        //encapsulation
        public double Balance { get => balance; set => balance = value; }

        //methods
        public void Deposit(double amount)
        {
            if (amount < 0)
            {
                Console.WriteLine("Invalid deposit amount");
            }
            else
            {
                balance += amount;
            }
        }

        public void Withdraw(double amount)
        {
            if (amount < 0 || amount > balance)
            {
                Console.WriteLine("Invalid withdrawal amount");
            }
            else
            {
                balance -= amount;
            }
        }
    }
}