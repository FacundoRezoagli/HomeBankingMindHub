using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Repositories;

namespace HomeBankingMindHub.Services
{
    public class AccountService : IAccountService
    {
        private readonly IAccountRepository _accountRepository;

        public AccountService( IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public AccountDTO FindById(long id)
        {
            Account account = _accountRepository.FindById(id);
            if (account == null)
            {
                return null;
            }
            return new AccountDTO(account);
        }

        IEnumerable<AccountDTO> IAccountService.GetAllAccounts()
        {
            var accounts = _accountRepository.GetAllAccounts();
            var accountsDTO = new List<AccountDTO>();
            foreach (Account account in accounts)
            {
                var newAccountDTO = new AccountDTO(account);
                accountsDTO.Add(newAccountDTO);
            }
            return accountsDTO;
        }
    }
}
