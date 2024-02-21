using HomeBankingMindHub.Models;
using HomeBankingMindHub.Utils;
using System.Net;

public class Account
{
    public long Id { get; set; }
    public string Number { get; set; }
    public DateTime CreationDate { get; set; }
    public double Balance { get; set; }
    public Client Client { get; set; }
    public long ClientId { get; set; }
    public ICollection<Transaction> Transactions { get; set; }
    public Account(Client client)
    {
        CreationDate = DateTime.Now;
        Balance = 0;
        Number = Utils.GenerateAccountNumber();
        ClientId = client.Id;
        this.Transactions = new List<Transaction>();
    }
    public Account(long ClientId, DateTime CreationDate, string Number, double Balance)
    {
        this.ClientId = ClientId;
        this.CreationDate = CreationDate;
        this.Balance = Balance;
        this.Number = Number;
        this.Transactions = new List<Transaction>();
    }
}