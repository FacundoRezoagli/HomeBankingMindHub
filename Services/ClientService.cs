using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;

namespace HomeBankingMindHub.Services
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly ICardRepository _cardRepository;

        public ClientService(IClientRepository clientRepository, IAccountRepository accountRepository, ICardRepository cardRepository)
        {
            _clientRepository = clientRepository;
            _accountRepository = accountRepository;
            _cardRepository = cardRepository;
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

        public void createClient(ClientRegisterDTO client)
        {
            Client newClient = new Client(client);
            _clientRepository.Save(newClient);
            //AHORA QUE EL CLIENTE TIENE ID LO TRAEMOS PARA AGREGARLE UNA CUENTA
            Client c = FindByEmail(client.Email);
            Account a = new Account(c);
            _accountRepository.Save(a);
        }

        public string createAccount(string email)
        {
            Client user = _clientRepository.FindByEmail(email);
            if (_accountRepository.GetAccountsByClient(user.Id).Count() == 3)
                return "Solo esta permitido tener hasta 3 cuentas";

            Account ac = new Account(user);
            _accountRepository.Save(ac);
            return "ok";
        }

        public IEnumerable<Account> GetClientAccounts(string email)
        {
            return _accountRepository.GetAccountsByClient(FindByEmail(email).Id);
        }

        public string createCard(string email, CardCreateDTO card)
        {
            Client user = _clientRepository.FindByEmail(email);
            if (card.Type != "CREDIT" && card.Type != "DEBIT")
            {
                return "Tipo de tarjeta invalido";
            }

            if (card.Color != "SILVER" && card.Color != "GOLD" && card.Color != "TITANIUM")
            {
                return "Tipo de tarjeta invalido";
            }

            if (_cardRepository.IsCreated(user.Id, card.Type, card.Color))
            {
                return "Ya posee una tarjeta de tipo " + card.Type.ToString() + " " + card.Color.ToString();
            }

            var c = new Card(card, user);
            _cardRepository.Save(c);
            return "ok";
        }
    }
}
