using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Repository.Models;
using TestProject.WebAPI.Dtos;

namespace TestProject.WebAPI.Services
{
    public interface IUserService
    {
        Task<User> GetUser(string userId);
        Task<List<User>> GetAllUsers();
        Task<CreateUserResponse> CreateUser(CreateUserRequest request);
    }
}