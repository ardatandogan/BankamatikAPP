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

        public List<Account> GetAccountsByUserId(int? userId)
        {
            var filter = new Account();
            if (userId.HasValue)
                filter.UserID = userId.Value;

            return _accountRepository.GetAccounts(filter);
        }

        public Account? GetAccountById(int id)
        {
            return _accountRepository.GetAccountById(id);
        }

        public void CreateAccount(Account account)
        {
            if (account.UserID <= 0)
                throw new ArgumentException("UserID is required.");

            if (account.Balance < 0)
                throw new ArgumentException("Initial balance cannot be negative.");

            _accountRepository.InsertAccount(account);
        }

        public void UpdateAccount(Account account)
        {
            if (account.AccountID <= 0)
                throw new ArgumentException("Valid AccountID is required.");

            _accountRepository.UpdateAccount(account);
        }

        public void DeleteAccount(int accountId)
        {
            var account = new Account { AccountID = accountId };
            _accountRepository.DeleteAccount(account);
        }
    }
}
