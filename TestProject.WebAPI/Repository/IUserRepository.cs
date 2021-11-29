using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Repository
{
    public interface IUserRepository
    {
        Task<User> FetchAsync(string email);
        Task<List<User>> FetchAllAsync();
        Task<bool> CheckIfUserExists(string email);
        Task<bool> CreateUser(User u);
    }
}
