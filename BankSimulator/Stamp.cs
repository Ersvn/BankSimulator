using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSimulator
{
    public enum StampType
    {
        PreTransaction,
        PostTransaction
    }

    public class Stamp
    {
        public double Balance { get; }
        public int ClientId { get; }
        public StampType Type { get; }

        public Stamp(double balance, int clientId, StampType type)
        {
            Balance = balance;
            ClientId = clientId;
            Type = type;
        }
    }
}
