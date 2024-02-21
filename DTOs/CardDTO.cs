using HomeBankingMindHub.Models;
using System;

namespace HomeBankingMindHub.DTOs
{
    public class CardDTO
    {
        public long Id { get; set; }
        public string CardHolder { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Number { get; set; }
        public int Cvv { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public CardDTO(Card card) 
        {
            Id = card.Id;
            CardHolder = card.CardHolder;
            Type = card.Type;
            Color = card.Color;
            Number = card.Number;
            Cvv = card.Cvv;
            FromDate = card.FromDate;
            ThruDate = card.ThruDate;
        }
    }
}