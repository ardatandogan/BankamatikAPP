using Microsoft.AspNetCore.Mvc;
using Bankamatik.DataAccess.Repositories;
using Bankamatik.Core.DTOs;
using Bankamatik.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Bankamatik.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly AccountRepository _accountRepository;

        public AccountController(IConfiguration configuration)
        {
            _accountRepository = new AccountRepository(configuration);
        }

        // GET: api/account?userId=5
        [HttpGet]
        public IActionResult GetAccounts([FromQuery] int? userId)
        {
            // DTO yerine entity filtre olarak veriliyor
            var filter = new Account { UserID = (int)userId };
            var accounts = _accountRepository.GetAccounts(filter);
            return Ok(accounts);
        }

        // GET: api/account/5
        [HttpGet("{id}")]
        public IActionResult GetAccountById(int id)
        {
            var account = _accountRepository.GetAccountById(id);
            if (account == null)
                return NotFound();

            return Ok(account);
        }

        // POST: api/account
        [HttpPost]
        public IActionResult CreateAccount([FromBody] AccountDTO accountDto)
        {
            if (accountDto == null)
                return BadRequest();

            var account = new Account
            {
                UserID = accountDto.UserID ?? 0,
                Balance = accountDto.Balance ?? 0,
                CreatedAt = accountDto.CreatedAt ?? DateTime.MinValue
            };

            _accountRepository.InsertAccount(account);
            return Ok("Account created successfully.");
        }

        // PUT: api/account/5
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] AccountDTO accountDto)
        {
            if (accountDto == null || id != accountDto.AccountID)
                return BadRequest("Invalid account data.");

            if (!accountDto.UserID.HasValue)
                return BadRequest("UserID cannot be null.");

            var account = new Account
            {
                AccountID = accountDto.AccountID,
                UserID = accountDto.UserID.Value,
                Balance = accountDto.Balance ?? 0, // null ise 0 atandı, istersen başka değer verilebilir
                CreatedAt = accountDto.CreatedAt ?? DateTime.MinValue
            };

            _accountRepository.UpdateAccount(account);

            return Ok("Account updated successfully.");
        }


        
        // DELETE: api/account/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            try
            {
                // Account entity oluşturmaya gerek yok, direkt stored procedure çağrılır
                _accountRepository.DeleteAccountWithTransactions(id);
                return Ok("Account and related transactions deleted successfully.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error deleting account: {ex.Message}");
            }
        }

    }
}
