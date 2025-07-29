using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bankamatik.Business.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly AccountRepository _accountRepository;
        private readonly LogService _logService;

        public TransactionService(TransactionRepository transactionRepository, AccountRepository accountRepository, LogService logService)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
            _logService = logService;
        }

        public List<Transaction> GetTransactions(Transaction filter)
        {
            var result = _transactionRepository.GetTransactions(filter);
            return result ?? new List<Transaction>();
        }

        public List<Transaction> GetTransactionsByUserId(int userId)
        {
            var transactions = new List<Transaction>();

            var accounts = _accountRepository.GetAccounts(new Account { UserID = userId });

            foreach (var acc in accounts)
            {
                var trans = _transactionRepository.GetTransactions(new Transaction { AccountID = acc.AccountID });
                transactions.AddRange(trans);
            }

            return transactions
                .GroupBy(t => t.TransactionID)
                .Select(g => g.First())
                .OrderByDescending(t => t.TransactionDate)
                .ToList();
        }

        public Transaction? GetTransactionById(Transaction transaction)
        {
            if (transaction == null || transaction.TransactionID <= 0)
                return null;

            return _transactionRepository.GetTransactions(transaction)?.FirstOrDefault();
        }

        public void CreateTransaction(Transaction transaction)
        {
            if (transaction.FromAccountID <= 0 || transaction.ToAccountID <= 0 || transaction.Amount <= 0)
                throw new ArgumentException("Invalid transaction details.");

            var fromAccount = _accountRepository.GetAccounts(new Account { AccountID = transaction.FromAccountID })?.FirstOrDefault();
            var toAccount = _accountRepository.GetAccounts(new Account { AccountID = transaction.ToAccountID })?.FirstOrDefault();

            if (fromAccount == null || toAccount == null)
                throw new InvalidOperationException("One or both accounts not found.");

            if (fromAccount.Balance < transaction.Amount)
                throw new InvalidOperationException("Insufficient balance.");

            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;

            _accountRepository.UpdateAccount(fromAccount);
            _accountRepository.UpdateAccount(toAccount);
            _transactionRepository.InsertTransaction(transaction);

            _logService.InsertLog(new Log
            {
                UserID = transaction.AccountID,
                ActionType = "Transfer",
                Description = $"Transferred {transaction.Amount} from Account {transaction.FromAccountID} to Account {transaction.ToAccountID}",
                CreatedAt = DateTime.Now
            });
        }

        public void UpdateTransaction(Transaction transaction)
        {
            if (transaction.TransactionID <= 0)
                throw new ArgumentException("Valid TransactionID is required.");

            _transactionRepository.UpdateTransaction(transaction);

            _logService.InsertLog(new Log
            {
                UserID = transaction.AccountID,
                ActionType = "Update",
                Description = $"Transaction {transaction.TransactionID} updated.",
                CreatedAt = DateTime.Now
            });
        }

        public void DeleteTransaction(Transaction transaction)
        {
            _transactionRepository.DeleteTransaction(transaction);

            _logService.InsertLog(new Log
            {
                UserID = transaction.AccountID,
                ActionType = "Delete",
                Description = $"Transaction {transaction.TransactionID} deleted.",
                CreatedAt = DateTime.Now
            });
        }

        public void WithdrawMoney(int accountId, decimal amount, int userId)
        {
            if (accountId <= 0 || amount <= 0)
                throw new ArgumentException("Valid Account ID and amount are required.");

            var account = _accountRepository.GetAccounts(new Account { AccountID = accountId })?.FirstOrDefault();
            if (account == null)
                throw new InvalidOperationException("Account not found.");

            if (account.Balance < amount)
                throw new InvalidOperationException("Insufficient balance.");

            account.Balance -= amount;
            _accountRepository.UpdateAccount(account);

            _logService.InsertLog(new Log
            {
                UserID = userId,
                ActionType = "Withdraw",
                Description = $"Withdrawn {amount} from Account {accountId}",
                CreatedAt = DateTime.Now
            });
        }

        public void DepositMoney(int accountId, decimal amount, int userId)
        {
            if (accountId <= 0 || amount <= 0)
                throw new ArgumentException("Valid Account ID and amount are required.");

            var account = _accountRepository.GetAccounts(new Account { AccountID = accountId })?.FirstOrDefault();
            if (account == null)
                throw new InvalidOperationException("Account not found.");

            account.Balance += amount;
            _accountRepository.UpdateAccount(account);

            _logService.InsertLog(new Log
            {
                UserID = userId,
                ActionType = "Deposit",
                Description = $"Deposited {amount} to Account {accountId}",
                CreatedAt = DateTime.Now
            });
        }
    }
}
