using System;
using System.Threading;

namespace BankSimulator
{
    public class Program
    {
        private static BankAccount bankAccount;
        private static Security secureIt;
        private static List<Client> clients = new List<Client>();

        static void Main(string[] args)
        {
            bankAccount = new BankAccount(1000); // initialize bank account with start balance

            int numClients = 10; // sets the amount of clients

            // create and start the client threads
            for (int i = 0; i < numClients; i++)
            {
                Client client = new Client(bankAccount, i);
                clients.Add(client);
                Thread thread = new Thread(client.Run);
                thread.Start();
            }

            StartClients();
            Wait();
            StopClients();
            GatherResults();

            Console.ReadKey();
        }

        private static void StartClients()
        {
            // start the client threads
            foreach (Client client in clients)
            {
                Thread thread = new Thread(client.Run);
                thread.Start();
            }
        }

        private static void Wait()
        {
            // wait 5 sec

            Thread.Sleep(5000);
        }

        private static void StopClients()
        {
            // stops the client threads
            foreach (Client client in clients)
            {
                client.Stop();
            }
        }

        private static void GatherResults()
        {
            // gathers the result
            double totalAmountTransactioned = 0;
            int totalNumberOfTransactions = 0;
            int totalNumberOfErrors = 0;

            foreach (Client client in clients)
            {
                totalAmountTransactioned += client.GetTotalAmountTransactioned();
                totalNumberOfTransactions += bankAccount.GetNumberOfTransactions();
                if (secureIt != null)
                {
                    totalNumberOfErrors += secureIt.GetNumberOfErrors();
                }
            }
            #region Transaction information
            Console.WriteLine(" ");
            Console.WriteLine("Number of transactions: " + totalNumberOfTransactions);
            Console.WriteLine("Number of errors: " + totalNumberOfErrors);
            Console.WriteLine("All transactions of Clients sums into: " + totalAmountTransactioned + ", balance on account " + bankAccount.GetBalance());
            Console.ReadLine();
            #endregion
        }
    }
}
