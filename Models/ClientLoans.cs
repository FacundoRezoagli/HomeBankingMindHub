﻿namespace HomeBankingMindHub.Models
{
    public class ClientLoans
    {
        public long Id { get; set; }
        public double Amount { get; set; }
        public string Payment { get; set; }
        public long ClientId { get; set; }
        public Client Client { get; set; }
        public long LoanId { get; set; }
        public Loan Loan { get; set; }
    }
}