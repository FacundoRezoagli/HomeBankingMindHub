using System;
using System.Linq;

namespace HomeBankingMindHub.Models
{
    public class DBInitializer
    {
        public static void Initialize(HomeBankingContext context)
        {
            if (!context.Clients.Any())  //CLIENTS
            {
                var clients = new Client[]
                {
                    new Client { Email = "vcoronado@gmail.com", FirstName="Victor", LastName="Coronado", Password="123456"}
                };
                context.Clients.AddRange(clients);
                context.SaveChanges();
            }
            if (!context.Account.Any())  //ACCOUNTS
            {
                var accountVictor = context.Clients.FirstOrDefault(c => c.Email == "vcoronado@gmail.com");
                if (accountVictor != null)
                {
                    var accounts = new Account[]
                    {
                        new Account {ClientId = accountVictor.Id, CreationDate = DateTime.Now, Number = "VIN001", Balance = 0 }
                    };
                    foreach (Account account in accounts)
                    {
                        context.Account.Add(account);
                    }
                    context.SaveChanges();
                }
            }
            if (!context.Transactions.Any())
            {
                var account1 = context.Account.FirstOrDefault(c => c.Number == "VIN001");
                if (account1 != null)
                {
                    var transactions = new Transaction[]
                    {
                        new Transaction
                        {
                            AccountId = account1.Id,
                            Amount = 10000,
                            Date = DateTime.Now.AddHours(-5),
                            Description = "Transferencia reccibida",
                            Type = TransactionType.CREDIT.ToString()
                        },
                        new Transaction
                        {
                            AccountId = account1.Id,
                            Amount = -2000,
                            Date = DateTime.Now.AddHours(-6),
                            Description = "Compra en tienda mercado libre",
                            Type = TransactionType.DEBIT.ToString()
                        },
                        new Transaction
                        {
                            AccountId = account1.Id,
                            Amount = -3000,
                            Date = DateTime.Now.AddHours(-7),
                            Description = "Compra en tienda xxxx",
                            Type = TransactionType.DEBIT.ToString()
                        },
                    };

                    foreach (Transaction transaction in transactions)
                    {
                        context.Transactions.Add(transaction);
                    }
                    context.SaveChanges();
                }
            }
        }
    }
}

