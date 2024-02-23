using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;

namespace HomeBankingMindHub.Services
{
    public class LoansService : ILoansService
    {
        //CONSTRUCTOR
        #region
        public LoansService(
            IClientRepository clientRepository,
            IAccountRepository accountRepository,
            ILoanRepository loanRepository,
            IClientLoanRepository clientLoanRepository,
            ITransactionRepository transactionRepository
            )
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _loanRepository = loanRepository;
            _clientLoanRepository = clientLoanRepository;
            _transactionRepository = transactionRepository;
        }
        private IClientRepository _clientRepository;
        private IAccountRepository _accountRepository;
        private ILoanRepository _loanRepository;
        private IClientLoanRepository _clientLoanRepository;
        private ITransactionRepository _transactionRepository;
        #endregion

        public string registerLoan(LoanApplicationDTO loanApplication, string email)
        {
            Client user = _clientRepository.FindByEmail(email);
            Loan loan = _loanRepository.FindById(loanApplication.LoanId);
            if (loan == null) return "El tipo de prestamo solicitado no existe";

            if (loanApplication.Amount < 0)
            {
                return "Monto invalido";
            }

            if (loanApplication.Amount > loan.MaxAmount)
            {
                return "El prestamo de tipo " + loan.Name.ToString().ToLower() + " tiene un monto maximo de " + loan.MaxAmount.ToString();
            }

            if (loanApplication.Payments == null) return "Se debe indicar la cantidad de pagos";

            Account account = _accountRepository.GetAccountByNumber(loanApplication.ToAccountNumber);

            if (account == null) return "Cuenta inexistente";

            IEnumerable<Account> ClientAccounts = _accountRepository.GetAccountsByClient(user.Id);

            bool authorizedFlag = false;

            foreach (Account a in ClientAccounts)
            {
                if (a.Id == account.Id) authorizedFlag = true;
            }

            if (!authorizedFlag)
            {
                return "La cuenta que indico para solicitar el prestamo no le pertenece";
            }

            var cl = new ClientLoan()
            {
                Amount = loanApplication.Amount * 1.2,
                Payments = loanApplication.Payments,
                ClientId = user.Id,
                LoanId = loan.Id
            };
            user.ClientLoans.Add(cl);
            _clientLoanRepository.Save(cl);

            account.Balance += loanApplication.Amount;
            _accountRepository.Save(account);

            var tr = new Transaction()
            {
                Amount = loanApplication.Amount,
                Type = TransactionType.CREDIT.ToString(),
                Description = "Loan taken on " + DateTime.Now.ToString(),
                Date = DateTime.Now,
                AccountId = account.Id,
            };

            _transactionRepository.Save(tr);

            return "ok";
        }

        public IEnumerable<LoanDTO> GetAllLoans()
        {
            IEnumerable<Loan> loans = _loanRepository.GetAllLoans();

            List<LoanDTO> loansDTO = new List<LoanDTO>();
            foreach (Loan loan in loans)
            {
                LoanDTO newLoanDTO = new LoanDTO(loan);
                loansDTO.Add(newLoanDTO);
            }

            return loansDTO;
        }
    }
}
