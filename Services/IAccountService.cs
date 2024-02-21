using HomeBankingMindHub.DTOs;

namespace HomeBankingMindHub.Services
{
    public interface IAccountService
    {
        AccountDTO FindById(long id);
        IEnumerable<AccountDTO> GetAllAccounts();
    }
}
