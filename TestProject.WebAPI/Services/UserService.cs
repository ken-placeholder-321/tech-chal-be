using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Repository.Models;
using System;
using TestProject.WebAPI.Repository;
using TestProject.WebAPI.Dtos;
using System.Net;

namespace TestProject.WebAPI.Services
{
    public class UserService : IUserService
    {
        public const int UserSalaryExpenseDifferenceThreshold = 1000;

        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository){
            _userRepository = userRepository;
        }
        public async Task<User> GetUser(string email)
        {
           return await _userRepository.FetchAsync(email);
        }

        public async Task<List<User>> GetAllUsers()
        {
            return await _userRepository.FetchAllAsync();
        }

        public async Task<CreateUserResponse> CreateUser(CreateUserRequest request)
        {
            // Validate if email already exists in db
            var userExists = await _userRepository.CheckIfUserExists(request.Email);
            if (userExists)
            {
                return new CreateUserResponse { Success = false, ErrorMessage = $"{request.Email} already exists in our system" };
            }

            // Validate if monthly expensive is negative
            if (request.MonthlyExpense < 0)
            {
                return new CreateUserResponse { Success = false, ErrorMessage = $"Monthly Expense cannot be negative" };
            }

            // Validate if monthly salary is negative
            if (request.MonthlySalary < 0)
            {
                return new CreateUserResponse { Success = false, ErrorMessage = $"Monthly Salary cannot be negative" };
            }

            // Validate if monthly expense - salary does not fit business requirements
            if (!ValidateSalaryExpense(request))
            {
                return new CreateUserResponse { Success = false, ErrorMessage = $"Monthly Salary and Expense do not satisfy our requirements" };
            }


            await _userRepository.CreateUser(
                new User {
                    Email = request.Email,
                    Name=request.Name,
                    MonthlyExpense = request.MonthlyExpense,
                    MonthlySalary = request.MonthlySalary
                });

            return new CreateUserResponse { Success = true };
        }

        private bool ValidateSalaryExpense(CreateUserRequest request)
        {
            if (request.MonthlySalary - request.MonthlyExpense < UserSalaryExpenseDifferenceThreshold)
            {
                return false;
            }

            return true;
        }
    }
}