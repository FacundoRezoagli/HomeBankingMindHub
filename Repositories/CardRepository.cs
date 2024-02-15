using HomeBankingMindHub.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;

namespace HomeBankingMindHub.Repositories
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(HomeBankingContext repositoryContext) : base(repositoryContext) { }

        public Card FindById(long id)
        {
            return FindByCondition(card => card.Id == id)
                .FirstOrDefault();
        }

        public IEnumerable<Card> GetAllCards()
        {
            return FindAll()
                .ToList();
        }

        public void Save(Card card)
        {
            Create(card);
            SaveChanges();
        }

        public IEnumerable<Card> GetCardsByClient(long clientId)
        {
            return FindByCondition(card => card.ClientId == clientId)
            .ToList();
        }

        public IEnumerable<Card> GetCardsByClientAndType(long clientId, string type)
        {
            return FindByCondition(card => card.ClientId == clientId && card.Type == type)
                .ToList();
        }

        public bool IsCreated(long clientId, string type, string color)
        {
            IEnumerable<Card> cards = new List<Card>();
            cards = FindByCondition(card => card.ClientId == clientId && card.Type == type && card.Color == color)
                .ToList();
            bool ret = cards.Count() != 0 ? true : false;
            return ret;
        }
    }
}