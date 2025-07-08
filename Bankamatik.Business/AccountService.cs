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
        //*
        public List<Account> GetAccountsByUserId(Account account)
        {
            var filter = new Account();
            if (account.UserID != null)  
                filter.UserID = account.UserID;

            return _accountRepository.GetAccounts(filter);
        }


        //* 
        public Account? GetAccountById(Account account)
        {
            return _accountRepository.GetAccountById(account);
        }

        //userid zorunlu - balance negaitf olamaz
        public void CreateAccount(Account account)
        {
            //try catch*
            if (account.UserID <= 0)
                throw new ArgumentException("UserID is required.");

            if (account.Balance < 0)
                throw new ArgumentException("Initial balance cannot be negative.");

            _accountRepository.InsertAccount(account);
        }

        //acc id 0dan büyük
        public void UpdateAccount(Account account)
        {
            //try catch
            if (account.AccountID <= 0)
                throw new ArgumentException("Valid AccountID is required.");

            _accountRepository.UpdateAccount(account);
        }

        public void DeleteAccount(int accountId)

            //try catch ve parametre
        {
            var account = new Account { AccountID = accountId };
            _accountRepository.DeleteAccount(account);
        }
    }
}
