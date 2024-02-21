using HomeBankingMindHub.DTOs;

namespace HomeBankingMindHub.Models
{
    public class Card
    {
        public long Id { get; set; }
        public string CardHolder { get; set; }
        public string Type { get; set; }
        public string Color { get; set; }
        public string Number { get; set; }
        public int Cvv { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ThruDate { get; set; }
        public long ClientId { get; set; }
        public Client Client { get; set; }
        public Card() { }
        public Card(CardCreateDTO card, Client client)
        {
            Type = card.Type;
            Color = card.Color;
            CardHolder = client.FirstName + " " + client.LastName;
            Cvv = Utils.Utils.GenerateCardCVV();
            FromDate = DateTime.Now;
            ThruDate = DateTime.Now.AddYears(5);
            Number = Utils.Utils.GenerateCardNumber();
            ClientId = client.Id;
        }
        public Card(long ClientId, string CardHolder, string Type, string Color, string Number, int Cvv, DateTime FromDate, DateTime ThruDate)
        {
            this.ClientId = ClientId;
            this.CardHolder = CardHolder;
            this.Type = Type;
            this.Color = Color;
            this.Number = Number;
            this.Cvv = Cvv;
            this.FromDate = FromDate;
            this. ThruDate = ThruDate;
        }
    }
}
