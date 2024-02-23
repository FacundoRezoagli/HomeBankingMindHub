using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.Services
{
    public interface IClientService
    {
        List<ClientDTO> getAllClients();
        ClientDTO getClientById(long id);
        public Client FindByEmail(string email);
        void createClient(ClientRegisterDTO client);
        string createAccount(string email);
        IEnumerable<Account> GetClientAccounts(string email);
        string createCard(string email, CardCreateDTO card);
    }
}
