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

        //dependency injection nedir : nesneyi dışarıdan alarak bağımlılığı azaltmak 

        //tüm kullanıcılar
        public List<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }
        //get by username
        public User? GetUserByUsername(User user)

        {
            return _userRepository.GetUser(user);
        }

        //create user kontrolleri en az 8 karakter - boş olamaz - aynı isimde kullanıcı olamaz
        public void CreateUser(User user)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user.Username))
                    throw new ArgumentException("Username cannot be empty.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Username error: " + ex.Message);
                return;
            }

            try
            {
                if (user.PasswordHash.Length < 8)
                    throw new ArgumentException("Password too short.");
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Password error: " + ex.Message);
                return;
            }

            try
            {
                var existingUser = _userRepository.GetUser(new User { Username = user.Username });
                if (existingUser != null)
                    throw new InvalidOperationException("User already exists.");
            }
            catch (InvalidOperationException ex)
            {
                Console.WriteLine("Duplicate user: " + ex.Message);
                return;
            }

            try
            {
                _userRepository.InsertUser(user);
                Console.WriteLine("User created successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database error: " + ex.Message);
            }
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
                Console.WriteLine("User successfully deleted.");
            }
            catch (ArgumentNullException ex)
            {
                Console.WriteLine($"User is null: {ex.Message}");
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Database error while deleting user: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error occurred while deleting user: {ex.Message}");

            }
        }


    }
}
