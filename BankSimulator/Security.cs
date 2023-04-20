using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    public class Security
    {
        private decimal prevBalance;
        private decimal newBalance;
        private int numErrors;
        private readonly object securityLock = new object();
        private double preTransactionBalance;
        private double postTransactionBalance;
        private int clientId;
        private TransactionType transactionType;
        private int numberOfIncorrectTransactions = 0;

        public void MakePreTransactionStamp(double balance, int clientId)
        {
            this.preTransactionBalance = balance;
            this.clientId = clientId;
            this.transactionType = TransactionType.Deposit; // assuming deposit by default
        }
        public void MakePostTransactionStamp(double balance, int clientId)
        {
            this.postTransactionBalance = balance;
            if (preTransactionBalance != postTransactionBalance)
            {
                numberOfIncorrectTransactions++;
            }
        }
        

        public void CheckTransaction(decimal prevBalance, decimal newBalance, string transactionType)
        {
            lock (securityLock)
            {
                this.prevBalance = prevBalance;
                this.newBalance = newBalance;
                this.transactionType = transactionType;
                if (prevBalance + newBalance != prevBalance * 2)
                {
                    numErrors++;
                }
            }
        }

        public void PrintErrors()
        {
            lock (securityLock)
            {
                Console.WriteLine("Number of transaction errors: " + numErrors);
            }
        }
    }
    public enum TransactionType
    {
        Deposit,
        Withdrawal
    }
}
