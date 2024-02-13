namespace HomeBankingMindHub.Repositories
{
    public interface IAccountRepository
    {
        IEnumerable<Account> GetAccountsByClient(long clientId);
        IEnumerable<Account> GetAccountByNumber(string accountNumber);
        IEnumerable<Account> GetAllAccounts();
        Account FindById(long id);
        void Save(Account account);
    }
}
