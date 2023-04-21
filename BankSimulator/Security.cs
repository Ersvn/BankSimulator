using System.Collections.Generic;

namespace BankSimulator
{
    public class Security
    {
        private List<Stamp> transactionStamps;
        private int NumberOfErrors;

        public Security()
        {
            transactionStamps = new List<Stamp>();
            NumberOfErrors = 0;
        }

        public void AddPreTransactionStamp(double balance, int clientId)
        {
            transactionStamps.Add(new Stamp(balance, clientId, StampType.PreTransaction));
        }

        public void AddPostTransactionStamp(double balance, int clientId)
        {
            transactionStamps.Add(new Stamp(balance, clientId, StampType.PostTransaction));
        }

        public bool VerifyLastTransaction(double amount)
        {
            if (transactionStamps.Count < 2)
            {
                return false;
            }

            var lastStamp = transactionStamps[transactionStamps.Count - 1];
            var previousStamp = transactionStamps[transactionStamps.Count - 2];

            if (lastStamp.Type != StampType.PostTransaction || previousStamp.Type != StampType.PreTransaction)
            {
                return false;
            }

            if (lastStamp.ClientId != previousStamp.ClientId)
            {
                return false;
            }

            double balanceChange = lastStamp.Balance - previousStamp.Balance;

            if (balanceChange != amount)
            {
                NumberOfErrors++;
                return false;
            }

            return true;
        }

        public int GetNumberOfErrors()
        {
            return NumberOfErrors;
        }
    }
}

