using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    internal class Client
    {
        private readonly int id;
        private readonly BankAccount account;
        private readonly Random random;
        private bool operating;
        private double totalAmountTransactioned;

        public Client(int id, BankAccount account)
        {
            this.id = id;
            this.account = account;
            this.random = new Random();
            this.operating = true;
            this.totalAmountTransactioned = 0;
        }

        public void Run()
        {
            while (operating)
            {
                bool deposit = random.NextDouble() < 0.5;
                double amount = random.Next(1, 101);
                if (deposit)
                {
                    account.Deposit(amount);
                    totalAmountTransactioned += amount;
                }
                else
                {
                    account.Withdraw(amount);
                    totalAmountTransactioned -= amount;
                }
                Thread.Sleep(100);
            }
        }

        public void Stop()
        {
            operating = false;
        }

        public double GetTotalAmountTransactioned()
        {
            return totalAmountTransactioned;
        }

    }
}
