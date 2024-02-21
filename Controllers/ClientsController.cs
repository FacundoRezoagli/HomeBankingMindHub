using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;
using HomeBankingMindHub.Services;
using Microsoft.AspNetCore.Mvc;

namespace HomeBankingMindHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {

        
        //CONSTRUCTOR
        #region
        public ClientsController(IClientService clientService, IAccountRepository accountRepository, ICardRepository cardRepository) 
        {
            _clientService = clientService;
            _accountRepository = accountRepository; 
            _cardRepository = cardRepository; 
        }
        private readonly IClientService _clientService;
        private IAccountRepository _accountRepository;
        private ICardRepository _cardRepository;
        #endregion

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(_clientService.getAllClients());
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid id");
            }
            try
            {
                ClientDTO clientDTO = _clientService.getClientById(id);
                if (clientDTO == null)
                {
                    return NotFound();
                }
                return Ok(clientDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult Post([FromBody] ClientRegisterDTO client)
        {
            try
            {
                //VALIDACIONES
                if (String.IsNullOrEmpty(client.Email) || String.IsNullOrEmpty(client.Password) || String.IsNullOrEmpty(client.FirstName) || String.IsNullOrEmpty(client.LastName))
                    return StatusCode(403, "datos inválidos");
                Client user = _clientService.FindByEmail(client.Email);
                if (user != null)
                    return StatusCode(403, "Email está en uso");

                Client newClient = new Client(client);
                _clientService.Save(newClient);

                //AHORA QUE EL CLIENTE TIENE ID LO TRAEMOS PARA AGREGARLE UNA CUENTA
                Client c = _clientService.FindByEmail(client.Email);
                Account a = new Account(c);
                _accountRepository.Save(a);
                return Created("", newClient);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
        
        [HttpGet("current")]
        public IActionResult GetCurrent()
        {
            try
            {
                //VALIDACIONES
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                    return Forbid();
                Client client = _clientService.FindByEmail(email);
                if (client == null)
                    return Forbid();

                ClientDTO clientDTO = new ClientDTO(client);
                return Ok(clientDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/accounts")]
        public IActionResult CreateAccount()
        {
            try
            {
                //VALIDACIONES
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                    return Forbid();
                Client user = _clientService.FindByEmail(email);
                if(_accountRepository.GetAccountsByClient(user.Id).Count() == 3) 
                    return StatusCode(403);

                Account ac = new Account(user);
                _accountRepository.Save(ac);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("current/accounts")]
        public IActionResult GetAccounts()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                {
                    return Forbid();
                }
                Client user = _clientService.FindByEmail(email);
                IEnumerable<Account> accounts = _accountRepository.GetAccountsByClient(user.Id);
                return Ok(accounts);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/cards")]
        public IActionResult CreateCard([FromBody] CardCreateDTO card)
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                    return Forbid();
                Client user = _clientService.FindByEmail(email);

                if (card.Type != "CREDIT" && card.Type != "DEBIT")
                {
                    return StatusCode(400);
                }

                if(_cardRepository.IsCreated(user.Id, card.Type, card.Color))
                {
                    return StatusCode(403, "Ya posee una tarjeta de tipo " + card.Type.ToString() + " " + card.Color.ToString());
                }

                var c = new Card(card, user);
                _cardRepository.Save(c);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}