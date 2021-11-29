using System.Collections.Generic;
using System.Threading.Tasks;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Services
{
    public interface IAccountService
    {
        Task<List<Account>> GetAllAccounts();
        Task<CreateAccountResponse> CreateAccount(CreateAccountRequest request);
    }
}
