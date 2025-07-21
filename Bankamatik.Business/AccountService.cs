using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace Bankamatik.Business.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;

        public AccountService(AccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public List<Account> GetAccountsByUserId(Account account)
        {
            var filter = new Account();
            if (account.UserID != null)
            {
                filter.UserID = account.UserID;
            }
            return _accountRepository.GetAccounts(filter);
        }
 
        public void CreateAccount(Account account)
        {
            try
            {
                if (account.UserID <= 0)
                    throw new ArgumentException("UserID is required.");

                if (account.Balance < 0)
                    throw new ArgumentException("Initial balance cannot be negative.");

                _accountRepository.InsertAccount(account);
            }
            catch (Exception ex)
            {
                throw new Exception("Error creating account: " + ex.Message, ex);
            }
        }

        public void UpdateAccount(Account account)
        {
            try
            {
                if (account.AccountID <= 0)
                    throw new ArgumentException("Valid AccountID is required.");

                _accountRepository.UpdateAccount(account);
            }
            catch (Exception ex)
            {
                // Hata işleme
                throw new Exception("Error updating account: " + ex.Message, ex);
            }
        }


        public bool DeleteAccount(int accountId)
        {
            _accountRepository.DeleteTransactionsByAccountId(accountId);
            var account = new Account { AccountID = accountId };
            int affectedRows = _accountRepository.DeleteAccount(account);
            return affectedRows > 0;

        }
    }
}
