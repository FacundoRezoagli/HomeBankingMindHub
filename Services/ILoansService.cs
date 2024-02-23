using HomeBankingMindHub.DTOs;

namespace HomeBankingMindHub.Services
{
    public interface ILoansService
    {
        IEnumerable<LoanDTO> GetAllLoans();
        string registerLoan(LoanApplicationDTO loanApplication, string email);
    }
}
