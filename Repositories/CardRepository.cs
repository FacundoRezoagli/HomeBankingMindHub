using HomeBankingMindHub.Models;
using Microsoft.EntityFrameworkCore;

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
    }
}