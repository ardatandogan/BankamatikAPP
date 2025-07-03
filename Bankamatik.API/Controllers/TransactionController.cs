using Microsoft.AspNetCore.Mvc;
using Bankamatik.DataAccess.Repositories;
using Bankamatik.Core.Entities;

namespace Bankamatik.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionController(IConfiguration configuration)
        {
            _transactionRepository = new TransactionRepository(configuration);
        }

        // GET: api/transaction
        [HttpGet]
        public IActionResult GetAllTransactions([FromQuery] int? accountId)
        {
            var transactions = _transactionRepository.GetTransactions(accountId);
            return Ok(transactions);
        }

        // GET: api/transaction/5
        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            var transaction = _transactionRepository.GetTransactionById(id);
            if (transaction == null)
                return NotFound();

            return Ok(transaction);
        }

        // POST: api/transaction
        [HttpPost]
        public IActionResult CreateTransaction([FromBody] Transaction transaction)
        {
            if (transaction == null)
                return BadRequest();

            _transactionRepository.InsertTransaction(
                transaction.FromAccountID,
                transaction.ToAccountID,
                transaction.Amount
            );

            return Ok("Transaction inserted successfully.");
        }

        // PUT: api/transaction/5
        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            if (transaction == null || id != transaction.TransactionID)
                return BadRequest();

            _transactionRepository.UpdateTransaction(
                id,
                transaction.FromAccountID,
                transaction.ToAccountID,
                transaction.Amount,
                transaction.TransactionDate
            );

            return Ok("Transaction updated successfully.");
        }

        // DELETE: api/transaction/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            _transactionRepository.DeleteTransaction(id);
            return Ok("Transaction deleted successfully.");
        }
    }
}
