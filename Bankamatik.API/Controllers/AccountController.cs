using Microsoft.AspNetCore.Mvc;
using Bankamatik.DataAccess.Repositories;
using Bankamatik.Core.Entities;

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

        // GET: api/account
        [HttpGet]
        public IActionResult GetAccounts([FromQuery] int? userId)
        {
            var accounts = _accountRepository.GetAccounts(userId);
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
        public IActionResult CreateAccount([FromBody] Account account)
        {
            if (account == null)
                return BadRequest();

            _accountRepository.InsertAccount(account.UserID, account.Balance);
            return Ok("Account created successfully.");
        }

        // PUT: api/account/5
        [HttpPut("{id}")]
        public IActionResult UpdateAccount(int id, [FromBody] Account account)
        {
            if (account == null || id != account.AccountID)
                return BadRequest();

            // Eğer CreatedAt tarihini güncellemek istemiyorsan, default(DateTime) ise null say
            DateTime? createdAt = null;
            if (account.CreatedAt != default(DateTime))
                createdAt = account.CreatedAt;

            _accountRepository.UpdateAccount(
                id,
                userId: account.UserID,
                balance: account.Balance,
                createdAt: createdAt
            );

            return Ok("Account updated successfully.");
        }



        // DELETE: api/account/5
        [HttpDelete("{id}")]
        public IActionResult DeleteAccount(int id)
        {
            _accountRepository.DeleteAccount(id);
            return Ok("Account deleted successfully.");
        }
    }
}
