using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.Repositories
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetCardsByClient(long clientId);
        IEnumerable<Card> GetAllCards();
        Card FindById(long id);
        void Save(Card card);
        IEnumerable<Card> GetCardsByClientAndType(long clientId, string type);
    }
}
