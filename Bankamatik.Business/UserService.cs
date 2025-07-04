using Bankamatik.Core.Entities;
using Bankamatik.DataAccess.Repositories;
using System;
using System.Collections.Generic;

namespace Bankamatik.Business.Services
{
    public class UserService
    {
        private readonly UserRepository _userRepository;

        public UserService(UserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public List<User> GetAllUsers()
        {
            return _userRepository.GetUsers();
        }

        public User? GetUserByUsername(string username)
        {
            return _userRepository.GetUser(new User { Username = username }); // 👈 eski method string değil User bekliyor
        }

        public void CreateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(user.PasswordHash) || user.PasswordHash.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            var existingUser = _userRepository.GetUser(new User { Username = user.Username }); // 👈 eski methoda uygun

            if (existingUser != null)
                throw new InvalidOperationException("A user with the same username already exists.");

            _userRepository.InsertUser(user); // 👈 eski method 2 parametre alıyor
        }

        public void UpdateUser(User user)
        {
            if (string.IsNullOrWhiteSpace(user.Username))
                throw new ArgumentException("Username cannot be empty.");

            if (string.IsNullOrWhiteSpace(user.PasswordHash) || user.PasswordHash.Length < 8)
                throw new ArgumentException("Password must be at least 8 characters long.");

            _userRepository.UpdateUser(user); // 👈 eski method 3 parametre alıyor
        }

        public void DeleteUser(int id)
        {
            _userRepository.DeleteUser(id);
        }
    }
}
