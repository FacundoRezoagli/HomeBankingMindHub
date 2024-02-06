using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAllAccounts();
        void Save(Account account);
        Account FindById(long id);
    }
}
