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
        public ClientsController(IClientService clientService) 
        {
            _clientService = clientService;
        }
        private readonly IClientService _clientService;
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
                    return NotFound();

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
                    return StatusCode(403, "No pueden haber campos vacios");
                Client user = _clientService.FindByEmail(client.Email);
                if (user != null)
                    return StatusCode(403, "Email está en uso");

                _clientService.createClient(client);
                return Created("", client);
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
                return Ok(new ClientDTO(client));
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
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (email == string.Empty)
                    return Forbid();
               
               string result = _clientService.createAccount(email);
                if (result == "ok")
                {
                    return StatusCode(201, "Cuenta creada con exito");
                }
                else
                {
                    return StatusCode(403, result);
                }
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
                return Ok(_clientService.GetClientAccounts(email));
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
                string result = _clientService.createCard(email, card);
                if (result == "ok")
                {
                    return StatusCode(201, "Tarjeta creada con exito");
                }
                else
                {
                    return StatusCode(403, result);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}