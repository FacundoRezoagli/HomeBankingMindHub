using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;

namespace HomeBankingMindHub.Services
{
    public interface IClientService
    {
        List<ClientDTO> getAllClients();
        ClientDTO getClientById(long id);
        public Client FindByEmail(string email);
        void Save(ClientRegisterDTO client);
    }
}
