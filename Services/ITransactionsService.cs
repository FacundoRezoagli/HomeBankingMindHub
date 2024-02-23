using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.Services
{
    public interface ITransactionsService
    {
        public string Transaction(TransferDTO transferDTO, string email);
    }
}
