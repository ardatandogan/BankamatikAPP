using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Bankamatik.Business.Services
{
    public class AccountService
    {
        private readonly AccountRepository _accountRepository;
        private readonly LogService _logService;

        public AccountService(AccountRepository accountRepository, LogService logService)
        {
            _accountRepository = accountRepository;
            _logService = logService;
        }

        public List<Account> GetAccountsByUserId(Account account)
        {
            Account? filter = null;

            if (account != null && account.UserID.HasValue && account.UserID.Value > 0)
            {
                filter = new Account { UserID = account.UserID.Value };
            }

            var result = _accountRepository.GetAccounts(filter);
            return result ?? new List<Account>();
        }

        public Account? GetAccountByAccountId(Account account)
        {
            Account? filter = new Account();
            if (account.AccountID != 0)
            {
                filter.AccountID = account.AccountID;
            }
            return _accountRepository.GetAccounts(filter)?.FirstOrDefault();
        }

        public void CreateAccount(Account account)
        {
            try
            {
                if (!account.UserID.HasValue || account.UserID.Value <= 0)
                    throw new ArgumentException("UserID is required.");

                if (account.Balance < 0)
                    throw new ArgumentException("Initial balance cannot be negative.");

                _accountRepository.InsertAccount(account);

                _logService.InsertLog(new Log
                {
                    UserID = account.UserID.Value,
                    ActionType = "Insert",
                    Description = $"Account created with Currency: {account.ParaCinsi} - With Balance: {account.Balance}",
                    CreatedAt = DateTime.Now
                });
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

                _logService.InsertLog(new Log
                {
                    UserID = account.UserID ?? 0,
                    ActionType = "Update",
                    Description = $"Account updated with AccountID: {account.AccountID}",
                    CreatedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                throw new Exception("Error updating account: " + ex.Message, ex);
            }
        }

        public bool DeleteAccount(int accountId)
        {
            // Öncelikle silinecek hesabın UserID'sini alalım ki log için kullanabilelim.
            var accountToDelete = GetAccountByAccountId(new Account { AccountID = accountId });

            _accountRepository.DeleteTransactionsByAccountId(accountId);
            var account = new Account { AccountID = accountId };
            int affectedRows = _accountRepository.DeleteAccount(account);

            if (affectedRows > 0 && accountToDelete != null)
            {
                _logService.InsertLog(new Log
                {
                    UserID = accountToDelete.UserID ?? 0,
                    ActionType = "Delete",
                    Description = $"Account deleted with AccountID: {accountId}",
                    CreatedAt = DateTime.Now
                });
            }

            return affectedRows > 0;
        }
    }
}
