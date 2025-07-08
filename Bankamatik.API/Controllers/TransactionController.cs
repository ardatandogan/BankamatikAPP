using Microsoft.AspNetCore.Mvc;
using Bankamatik.DataAccess.Repositories;
using Bankamatik.Core.Entities;
using Microsoft.Extensions.Configuration;

namespace Bankamatik.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionController : ControllerBase
    {
        private readonly TransactionRepository _transactionRepository;

        public TransactionController()
        {
            _transactionRepository = new TransactionRepository();
        }

        // GET: api/transaction
        [HttpGet]
        [HttpGet]
        public IActionResult GetAllTransactions([FromQuery] int? accountId)
        {
            var transaction = new Transaction { AccountID = accountId };
            var transactions = _transactionRepository.GetTransactions(transaction);
            return Ok(transactions);
        }


        // GET: api/transaction/5
        [HttpGet("{id}")]
        public IActionResult GetTransactionById(int id)
        {
            var transactionParam = new Transaction { TransactionID = id };
            var transaction = _transactionRepository.GetTransactionById(transactionParam);

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

            _transactionRepository.InsertTransaction(transaction);

            return Ok("Transaction inserted successfully.");
        }

        // PUT: api/transaction/5
        [HttpPut("{id}")]
        public IActionResult UpdateTransaction(int id, [FromBody] Transaction transaction)
        {
            if (transaction == null || id != transaction.TransactionID)
                return BadRequest();

            _transactionRepository.UpdateTransaction(transaction);

            return Ok("Transaction updated successfully.");
        }

        // DELETE: api/transaction/5
        [HttpDelete("{id}")]
        public IActionResult DeleteTransaction(int id)
        {
            var transaction = new Transaction { TransactionID = id };
            _transactionRepository.DeleteTransaction(transaction);
            return Ok("Transaction deleted successfully.");
        }

    }
}
