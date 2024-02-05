using HomeBankingMindHub.Models;

using System;



namespace HomeBankingMindHub.dtos

{

    public class AccountDTO

    {

        public long Id { get; set; }

        public string Number { get; set; }

        public DateTime CreationDate { get; set; }

        public double Balance { get; set; }

    }

}