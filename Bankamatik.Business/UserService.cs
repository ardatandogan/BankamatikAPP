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
        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public UserService()
        {
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }
        //get by username
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
            return "success";
        }

        public string UpdateUser(User user)
        {
            string sonuc = string.Empty;
            try
            {
                if (string.IsNullOrWhiteSpace(user.Username))
                {
                    throw new Exception("Validation Error: Username cannot be empty.");
                }

                if (string.IsNullOrWhiteSpace(user.PasswordHash) || user.PasswordHash.Length < 8)
                {
                    throw new Exception("Validation Error: Password must be at least 8 characters long.");
                }

                _userRepository.UpdateUser(user);
                sonuc = "Başarılı";
            }
            catch (Exception ex)
            {
                sonuc = ex.Message;
            }
            return sonuc;
        }

        public void DeleteUser(User user)
        {
            try
            {
                _userRepository.DeleteUser(user);
            }
            catch (Exception ex)
            {
                throw new Exception($"Error occurred while deleting user: {ex.Message}", ex);
            }
        }



    }
}
