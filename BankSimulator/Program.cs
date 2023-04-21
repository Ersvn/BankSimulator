using System;
using System.Collections.Generic;
using System.Threading;

namespace BankSimulator
{
    public class Program
    {
        private static BankAccount bankAccount = new BankAccount(1000);
        private static Security secureIt = new Security();
        private static List<Client> clients = new List<Client>();
        private const int WaitTimeInMilliseconds = 5000;

        static void Main(string[] args)
        {
            int numClients = 10; // sets the amount of clients

            // create and start the client threads
            StartClient(numClients);
            Wait();
            StopClients();
            GatherResults();

            Console.ReadKey();
        }

        private static void StartClient(int numClients)
        {
            for (int i = 0; i < numClients; i++)
            {
                Client client = new Client(bankAccount, i);
                clients.Add(client);
                Thread thread = new Thread(client.Run);
                thread.Start();
            }
        }

        private static void Wait()
        {
            Thread.Sleep(WaitTimeInMilliseconds);
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
                Interlocked.Add(ref totalNumberOfTransactions, bankAccount.GetNumberOfTransactions());
                if (secureIt != null)
                {
                    Interlocked.Add(ref totalNumberOfErrors, secureIt.GetNumberOfErrors());
                }
            }
            #region Transaction information
            Console.WriteLine("\n\n"+ "Number of transactions: " + totalNumberOfTransactions);
            Console.WriteLine("Number of errors: " + totalNumberOfErrors);
            Console.WriteLine("All transactions of Clients sums into: " + totalAmountTransactioned + ", balance on account " + bankAccount.GetBalance() + "\n");
            Console.ReadLine();
            #endregion
        }
    }
}
