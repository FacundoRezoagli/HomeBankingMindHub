using System;
using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.DTOs
{
    public class AccountDTO
    {
        public long Id { get; set; }
        public string Number { get; set; }
        public DateTime CreationDate { get; set; }
        public double Balance { get; set; }
        public ICollection<Transaction> Transactions { get; set; }
    }
}
