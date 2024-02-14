using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HomeBankingMindHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        //CONSTRUCTOR
        #region
        public LoansController (
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

        [HttpPost]
        public IActionResult Post([FromBody] LoanApplicationDTO loanApplication)
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;

            Client user = _clientRepository.FindByEmail(email);

            if (email == string.Empty)
            {
                return Forbid();
            }

            Loan loan = _loanRepository.FindById(loanApplication.LoanId);

            if (loan == null) return Forbid();
            if(loanApplication.Amount < 0 || loanApplication.Amount > loan.MaxAmount) return StatusCode(400);
            if(loanApplication.Payments == null) return StatusCode(400);

            Account account = _accountRepository.GetAccountByNumber(loanApplication.ToAccountNumber);

            if (account == null) return Forbid();

            IEnumerable<Account> ClientAccounts = _accountRepository.GetAccountsByClient(user.Id);

            bool authorizedFlag = false;

            foreach(Account a in ClientAccounts)
            {
                if(a.Id == account.Id) authorizedFlag = true;
            }

            if(!authorizedFlag)
            {
                return Forbid();
            }

            var cl = new ClientLoan()
            {
                Amount = loanApplication.Amount * 1.2,
                Payments = loanApplication.Payments,
                ClientId = user.Id,
                LoanId = loan.Id
            };
            user.ClientLoans.Add(cl);
            //_clientRepository.Save(user);
            _clientLoanRepository.Save(cl);

            return Ok();
        }

        [HttpGet]
        public IActionResult Get()
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;

            Client user = _clientRepository.FindByEmail(email);

            if (email == string.Empty)
            {
                return Forbid();
            }
            IEnumerable<Loan> loans = _loanRepository.GetAllLoans();

            var loansDTO = new List<LoanDTO>();
            foreach (Loan loan in loans) 
            {
                var newLoanDTO = new LoanDTO
                {
                    Id = loan.Id,
                    Name = loan.Name,
                    MaxAmount = loan.MaxAmount,
                    Payments = loan.Payments
                };
                loansDTO.Add(newLoanDTO);
            }

            return Ok(loansDTO);
        }
    }
}
