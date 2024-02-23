using HomeBankingMindHub.DTOs;
using HomeBankingMindHub.Models;
using HomeBankingMindHub.Repositories;
using HomeBankingMindHub.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace HomeBankingMindHub.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoansController : ControllerBase
    {
        //CONSTRUCTOR
        #region
        public LoansController (ILoansService loansService)
        {
            _loansService = loansService;
        }
        private ILoansService _loansService;
        #endregion

        [HttpPost]
        public IActionResult Post([FromBody] LoanApplicationDTO loanApplication)
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if(email == null)
            {
                return Forbid();
            }

            string result = _loansService.registerLoan( loanApplication, email);
            if(result == "ok")
            {
                return Ok();
            }
            else
            {
                return StatusCode(403, result);
            }
        }

        [HttpGet]
        public IActionResult Get()
        {
            string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
            if (email == string.Empty)
            {
                return Forbid();
            }
            return Ok(_loansService.GetAllLoans());
        }
    }
}
