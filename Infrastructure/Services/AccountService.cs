using System;
using ApplicationCore.Contracts.Repositories;
using ApplicationCore.Contracts.Services;
using ApplicationCore.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using ApplicationCore.Exceptions;
using ApplicationCore.Entities;

namespace Infrastructure.Services
{
    public class AccountService : IAccountService
    {

        // Call our UserRepository: use our User dependency Injection
        private readonly IUserRepository _userRepository;

        // Generate constructor:
        public AccountService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }



        public async Task<bool> RegisterUser(UserRegisterModel model)
        {
            // check if the user already exits in the database, check by email -> we need to talk to the "UserRepository class"

            var user = await _userRepository.GetUserByEmail(model.Email);
            if (user != null)   // user already exits 
            {
                // we can create a customer exception
                throw new ConflictException("Email already exists, please try to login.");
            }

            // if email does not exist, continue with registration:

            // 1. create a random salt (see at below)
            var salt = GetRandomSalt();

            // 2. hash the password with salt created above (see at below)
            var hashedPassword = GetHashedPassword(model.Password, salt);

            // 3. create User object and save using Entity Framework (EF)
            var newUser = new User   // User Entity
            {
                Email = model.Email,
                HashedPassword = hashedPassword,
                Salt = salt,
                FirstName = model.FirstName,
                LastName = model.LastName,
                DateOfBirth = model.DateOfBirth
            };

            // 4. save the user to User table using UserRepository, so that we need to send these information above to UserRepository
            var savedUser = await _userRepository.Add(newUser);  // newly created user
            if (savedUser.Id > 0)
            {
                return true;
            }
            return false;
        }


        public async Task<UserModel> ValidateUser(string email, string password)  // return type was bool
        {
            // 1. go to database and get the row by email
            var user = await _userRepository.GetUserByEmail(email);

            // 1.1 if email does not exist
            if (user == null)  
            {
                throw new Exception("Email does not exists.");
            }

            // 1.2 if email exists
            var hashedPassword = GetHashedPassword(password, user.Salt);

            // comparing
            if (hashedPassword == user.HashedPassword)
            {
                // good password
                var userModel = new UserModel
                {
                    Id = user.Id,
                    DateOfBirth = user.DateOfBirth.GetValueOrDefault(),  // it's nullablt
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName
                };
                return userModel;
            }
            return null;
        }



        public async Task<bool> CheckEmail(string email)
        {
            var user = await _userRepository.GetUserByEmail(email);

            if (user == null)
            {
                return false;
            }
            return true;
        }




        // create private methods for a random salt: (for both registration and login)
        // using code from official documentation (never never create by ourself)

        private string GetRandomSalt()
        {
            // code for Random Generation salt:

            // generate a 128-bit salt
            var randomBytes = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);

        }

        private string GetHashedPassword(string password, string salt)
        {
            // code for Hashed Password:

            // derive a 256-bit subkey (use HMACSHA512 with 10,000 iterations)
            var hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password, //password
                Convert.FromBase64String(salt), //salt
                KeyDerivationPrf.HMACSHA512, //prf
                10000, //iterationCount
                256 / 8)); //numBytesRequested
            return hashed;
        }
    }
}

