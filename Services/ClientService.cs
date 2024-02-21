using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;

namespace HomeBankingMindHub.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client FindByEmail(string email)
        {
            return _clientRepository.FindByEmail(email);
        }

        public List<ClientDTO> getAllClients()
        {
            var clients = _clientRepository.GetAllClients();
            var clientsDTO = new List<ClientDTO>();
            foreach (Client client in clients)
            {
                var newClientDTO = new ClientDTO(client);
                clientsDTO.Add(newClientDTO);
            }
            return clientsDTO;
        }

        public ClientDTO getClientById(long id)
        {
            Client client = _clientRepository.FindById(id);
            ClientDTO clientDTO = new ClientDTO(client);
            if (client == null)
            {
                return null;
            }
            return clientDTO;
        }

        public void Save(Client client)
        {
            _clientRepository.Save(client);
        }
    }
}
