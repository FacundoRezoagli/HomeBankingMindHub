using HomeBankingMindHub.Models;
using System.Text.Json.Serialization;
using System.Transactions;
namespace HomeBankingMindHub.DTOs
{
    public class TransactionDTO
    {
        [JsonIgnore]
        public long Id { get; set; }
        public string Type { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }

        public TransactionDTO(Models.Transaction transaction) 
        {
            Id = transaction.Id;
            Type = transaction.Type;
            Amount = transaction.Amount;
            Description = transaction.Description;
            Date = transaction.Date;
        }
    }
}
