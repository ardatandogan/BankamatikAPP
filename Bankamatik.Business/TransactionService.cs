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

        public List<Transaction> GetTransactions(Transaction transaction)
        {
            return _transactionRepository.GetTransactions(transaction);
        }

        public Transaction? GetTransactionById(Transaction transaction)
        {
            return _transactionRepository.GetTransactionById(transaction);
        }

        public void CreateTransaction(Transaction transaction)
        {
           

            var fromAccount = _accountRepository.GetAccountById(new Account { AccountID = transaction.FromAccountID });
            var toAccount = _accountRepository.GetAccountById(new Account { AccountID = transaction.ToAccountID });

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
            try
            {
                if (transaction.TransactionID <= 0)
                    throw new ArgumentException("TransactionID is required.");

                _transactionRepository.UpdateTransaction(transaction);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Validation error: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error during update: {ex.Message}");
            }
        }
        public void DeleteTransaction(Transaction transaction)
        {
                _transactionRepository.DeleteTransaction(transaction);
        }

        public void DeleteTransactionsByAccountId(int accountId)
        {
            _transactionRepository.DeleteTransactionsByAccountId(accountId);
        }



    }
}
