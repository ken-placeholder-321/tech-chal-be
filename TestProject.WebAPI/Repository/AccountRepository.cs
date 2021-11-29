using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using TestProject.WebAPI.Db;
using TestProject.WebAPI.Logger;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Repository
{
    public class AccountRepository :IAccountRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ISimpleLogger _logger;

        public AccountRepository(IServiceScopeFactory serviceScopeFactory, ISimpleLogger logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;

        }

        public async Task<List<Account>> FetchAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ZipProjectDbContext>();
                return context.Accounts.ToList();
            }
        }

        public async Task<bool> CheckIfAccountExists(string email)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ZipProjectDbContext>();
                return context.Accounts.Any(x => x.Email == email);
            }
        }

        public async Task<bool> CreateAccount(Account u)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ZipProjectDbContext>();
                try
                {
                    context.Accounts.Add(u);
                    context.SaveChanges();
                    return true;
                } catch(Exception ex)
                {
                    _logger.Log(ex.ToString());
                    return false;
                }
                
            }
        }
    }
}
