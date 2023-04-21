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
        private int numberOfErrors;
        private readonly Security security;
        private static object balanceLock = new object();

        public BankAccount(double _balance)
        {
            balance = _balance;
            numberOfTransactions = 0;
            //numberOfErrors = 0;
            security = new Security();
        }

        public void Withdraw(double amount)
        {
            lock (balanceLock)
            {
                if (amount > balance)
                {
                    throw new Exception("Insufficient funds");
                }
                balance -= amount;
                numberOfTransactions++;
            }
        }

        public void Deposit(double amount)
        {
            lock (balanceLock)
            {
                balance += amount;
                numberOfTransactions++;
            }
        }

        public double GetBalance()
        {
            return balance;
        }

        public void Transaction(double amount, int clientId)
        {
            security.MakePreTransactionStamp(balance, clientId);
            balance = balance + amount;
            numberOfTransactions++;
            security.MakePostTransactionStamp(balance, clientId);
            if (!security.VerifyLastTransaction(amount))
            {
                numberOfErrors++;
            }
        }

        public int GetNumberOfTransactions()
        {
            return numberOfTransactions;
        }

        public int GetNumberOfErrors()
        {
            return numberOfErrors;
        }
    }
}
