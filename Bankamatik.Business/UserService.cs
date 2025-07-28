using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Bankamatik.Business.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;
        private readonly LogService _logService;

        public UserService(UserRepository userRepository, LogService logService)
        {
            _userRepository = userRepository;
            _logService = logService;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }

        public User? GetUserByUsername(User user)
        {
            return _userRepository.GetUser(user);
        }

        public string CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                return "Username cannot be empty.";

            if (user.PasswordHash.Length < 8)
                return "Password must be at least 8 characters long.";

            var existingUser = _userRepository.GetUser(new User { Username = user.Username });
            if (existingUser != null)
                return "This username is already taken.";

            _userRepository.InsertUser(user);

            _logService.InsertLog(new Log
            {
                UserID = user.ID,
                ActionType = "Insert",
                Description = $"User created with username: {user.Username}",
                CreatedAt = DateTime.Now
            });

            return "success";
        }

        public string UpdateUser(User user)
        {
            string sonuc = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new Exception("Validation Error: Username cannot be empty.");

                if (string.IsNullOrWhiteSpace(user.PasswordHash) || user.PasswordHash.Length < 8)
                    throw new Exception("Validation Error: Password must be at least 8 characters long.");

                _userRepository.UpdateUser(user);

                _logService.InsertLog(new Log
                {
                    UserID = user.ID,
                    ActionType = "Update",
                    Description = $"User updated: {user.Username}",
                    CreatedAt = DateTime.Now
                });

                sonuc = "Başarılı";
            }
            catch (Exception ex)
            {
                sonuc = ex.Message;
            }
            return sonuc;
        }

        public User? GetUserById(User user)
        {
            return _userRepository.GetUserById(user);
        }

        public void DeleteUser(User user)
        {
            try
            {
                _userRepository.DeleteUser(user);

                _logService.InsertLog(new Log
                {
                    UserID = user.ID,
                    ActionType = "Delete",
                    Description = $"User deleted: {user.Username}",
                    CreatedAt = DateTime.Now
                });
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while deleting user: {ex.Message}", ex);
            }
        }
    }
}
