using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.Repositories
{
    public interface IClientLoanRepository
    {
        void Save(ClientLoan clientLoan);
    }
}
