using HomeBankingMindHub.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Handlers;

namespace HomeBankingMindHub.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IClientRepository _clientRepository;
        public AuthController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] ClientRegisterDTO client)
        {
            try
            {
                if (String.IsNullOrEmpty(client.Email) || String.IsNullOrEmpty(client.Password))
                    return StatusCode(403, "datos inválidos");

                Client user = _clientRepository.FindByEmail(client.Email);
                var encryptionHandler = new EncryptionHandler();
                byte[] cHash;
                byte[] cSalt;
                encryptionHandler.EncryptPassword(client.Password, out cHash, out cSalt);

                if (user == null || !encryptionHandler.ValidatePassword(client.Password,cHash,cSalt))
                    return Unauthorized();

                var claims = new List<Claim>
                {
                    new Claim("Client", user.Email),
                };

                var claimsIdentity = new ClaimsIdentity(
                    claims,
                    CookieAuthenticationDefaults.AuthenticationScheme
                    );

                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    new ClaimsPrincipal(claimsIdentity));

                return Ok();

            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            try
            {
                await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);
                return Ok();
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}