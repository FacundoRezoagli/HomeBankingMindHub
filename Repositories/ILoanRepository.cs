using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.Repositories
{
    public interface ILoanRepository
    {
        IEnumerable<Loan> GetAllLoans();
        Loan FindById(long id);
    }
}
