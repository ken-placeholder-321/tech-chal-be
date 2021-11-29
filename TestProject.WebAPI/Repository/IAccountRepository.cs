using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Repository
{
    public interface IAccountRepository
    {
        Task<List<Account>> FetchAllAsync();
        Task<bool> CheckIfAccountExists(string email);
        Task<bool> CreateAccount(Account a);
    }
}
