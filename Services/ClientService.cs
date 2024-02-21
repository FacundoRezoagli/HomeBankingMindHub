using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;

namespace HomeBankingMindHub.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;

        public ClientService(IClientRepository clientRepository, IAccountRepository accountRepository)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
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

        public void Save(ClientRegisterDTO client)
        {
            Client newClient = new Client(client);
            _clientRepository.Save(newClient);
            //AHORA QUE EL CLIENTE TIENE ID LO TRAEMOS PARA AGREGARLE UNA CUENTA
            Client c = FindByEmail(client.Email);
            Account a = new Account(c);
            _accountRepository.Save(a);
        }
    }
}
