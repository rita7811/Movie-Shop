using System;
using ApplicationCore.Models;

namespace ApplicationCore.Contracts.Services
{
	public interface IAccountService
	{
        // Login method
        Task<bool> RegisterUser(UserRegisterModel model);

        // Validating method (return type was bool)
        Task<UserModel> ValidateUser(string email, string password);

        Task<bool> CheckEmail(string email);

    }
}

