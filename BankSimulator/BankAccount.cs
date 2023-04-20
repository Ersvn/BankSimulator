using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    public class BankAccount
    {
        private double balance;
        private int numberOfTransactions;
        private Security security;
        private readonly object balanceLock = new object();

        public BankAccount(double initialBalance)
        {
            balance = initialBalance;
            numberOfTransactions = 0;
            security = new Security();
        }

        public void Transaction(decimal amount, int clientId)
        {
            
        }



        public double GetBalance()
        {
            return balance;
        }

        public int GetNumberOfTransactions()
        {
            return numberOfTransactions;
        }

        

        public void Deposit(double amount)
        {
            lock (balanceLock)
            {
                double prevBalance = balance;
                balance += amount;
                double newBalance = balance;
                //security.CheckTransaction(prevBalance, newBalance, "Deposit");
            }
        }

        public void Withdraw(double amount)
        {
            lock (balanceLock)
            {
                double prevBalance = balance;
                balance -= amount;
                double newBalance = balance;
                //security.CheckTransaction(prevBalance, newBalance, "Withdrawal");
            }
        }

        public void PrintTransactionErrors()
        {
            security.PrintErrors();
        }
    }

}
