using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    internal class Client
    {
        private readonly int id;
        private readonly BankAccount account;
        private double totalAmountTransacted;
        private readonly Random random;
        private bool _stop;
        private int numberOfTransactions;

        public Client(BankAccount _account, int _id)
        {
            account = _account;
            id = _id;
            totalAmountTransacted = 0;
            numberOfTransactions = 0;
        }

        public void Run()
        {
            Random random = new Random();

            while (!_stop)
            {
                bool deposit = random.Next(2) == 0;
                double amount = random.NextDouble() * 1000;

                lock (account) // lock the bank account to ensure mutual exclusion
                {
                    if (deposit)
                    {
                        account.Deposit(amount);
                        totalAmountTransacted += amount;
                        Console.WriteLine($"Client {id}: deposited {amount:C2}, balance is {account.GetBalance():C2}");
                    }
                    else
                    {
                        try
                        {
                            account.Withdraw(amount);
                            totalAmountTransacted -= amount;
                            Console.WriteLine($"Client {id}: withdrew {amount:C2}, balance is {account.GetBalance():C2}");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Client {id}: withdrawal failed ({ex.Message}), balance is {account.GetBalance():C2}");
                        }
                    }
                }

                Thread.Sleep(random.Next(1000));
            }
        }

        public void Stop()
        {
            _stop = true;
        }

        public BankAccount GetBankAccount()
        {
            return account;
        }

        public double GetTotalAmountTransactioned()
        {
            return totalAmountTransacted;
        }

        public int GetNumberOfTransactions()
        {
            return numberOfTransactions;
        }
    }
}
