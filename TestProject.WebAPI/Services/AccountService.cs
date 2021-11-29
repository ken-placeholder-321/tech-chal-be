using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Repository;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Services
{
    public class AccountService: IAccountService
    {
        private readonly IUserRepository _userRepository;
        private readonly IAccountRepository _accountRepository;
        public AccountService(IUserRepository userRepository, IAccountRepository accountRepository)
        {
            _userRepository = userRepository;
            _accountRepository = accountRepository;
        }
        public async Task<List<Account>> GetAllAccounts()
        {
            return await _accountRepository.FetchAllAsync();
        }
        public async Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request)
        {
            // check if user exists
            var userExists = await _userRepository.CheckIfUserExists(request.Email);
            if (!userExists)
            {
                return new CreateAccountResponse { Success = false, Status = HttpStatusCode.NotFound, ErrorMessage = $"User with email: {request.Email} not found" };
            }

            var accountExists = await _accountRepository.CheckIfAccountExists(request.Email);
            if (accountExists)
            {
                return new CreateAccountResponse { Success = false, Status = HttpStatusCode.BadRequest, ErrorMessage = $"Account with email: {request.Email} exists already" };
            }

            await _accountRepository.CreateAccount(new Account
            {
                Email = request.Email,
                Username = request.Username,
                Password = request.Password
            });

            return new CreateAccountResponse
            {
                Success = true,
                Status = HttpStatusCode.OK
            };
        }
    }
}
