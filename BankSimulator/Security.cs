using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    public class Security
    {
        private readonly List<Stamp> Stamps;
        public int NumberOfErrors;

        public Security()
        {
            Stamps = new List<Stamp>();
            NumberOfErrors = 0;
        }

        public void MakePreTransactionStamp(double balance, int clientId)
        {
            Stamps.Add(new Stamp(balance, clientId, StampType.PreTransaction));
        }

        public void MakePostTransactionStamp(double balance, int clientId)
        {
            Stamps.Add(new Stamp(balance, clientId, StampType.PostTransaction));
        }

        public bool VerifyLastTransaction(double amount)
        {
            if (Stamps.Count < 2)
            {
                return false;
            }

            Stamp lastStamp = Stamps[Stamps.Count - 1];
            Stamp previousStamp = Stamps[Stamps.Count - 2];

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

