using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace Bankamatik.Business.Services
{
    public class TransactionService
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly AccountRepository _accountRepository;

        public TransactionService(TransactionRepository transactionRepository, AccountRepository accountRepository)
        {
            _transactionRepository = transactionRepository;
            _accountRepository = accountRepository;
        }

        public List<Transaction> GetTransactions(int? accountId = null)
        {
            return _transactionRepository.GetTransactions(accountId);
        }

        public Transaction? GetTransactionById(int id)
        {
            return _transactionRepository.GetTransactionById(id);
        }

        public void CreateTransaction(Transaction transaction)
        {
            if (transaction.Amount <= 0)
                throw new ArgumentException("Transaction amount must be positive.");

            if (transaction.FromAccountID == transaction.ToAccountID)
                throw new ArgumentException("Cannot transfer to the same account.");

            var fromAccount = _accountRepository.GetAccountById(transaction.FromAccountID);
            var toAccount = _accountRepository.GetAccountById(transaction.ToAccountID);

            if (fromAccount == null || toAccount == null)
                throw new InvalidOperationException("One or both accounts not found.");

            if (fromAccount.Balance < transaction.Amount)
                throw new InvalidOperationException("Insufficient balance.");

            fromAccount.Balance -= transaction.Amount;
            toAccount.Balance += transaction.Amount;

            _accountRepository.UpdateAccount(fromAccount);
            _accountRepository.UpdateAccount(toAccount);
            _transactionRepository.InsertTransaction(transaction);
        }

        public void UpdateTransaction(Transaction transaction)
        {
            if (transaction.TransactionID <= 0)
                throw new ArgumentException("TransactionID is required.");

            _transactionRepository.UpdateTransaction(transaction);
        }

        public void DeleteTransaction(int id)
        {
            _transactionRepository.DeleteTransaction(id);
        }
    }
}
