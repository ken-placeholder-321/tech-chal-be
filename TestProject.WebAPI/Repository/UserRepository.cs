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
    public class UserRepository :IUserRepository
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ISimpleLogger _logger;

        public UserRepository(IServiceScopeFactory serviceScopeFactory, ISimpleLogger logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        public async Task<User> FetchAsync(string email)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ZipProjectDbContext>();
                return context.Users.FirstOrDefault(x => x.Email == email);
            }
        }

        public async Task<List<User>> FetchAllAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ZipProjectDbContext>();
                return context.Users.ToList();
            }
        }

        public async Task<bool> CheckIfUserExists(string email)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ZipProjectDbContext>();
                return context.Users.Any(x => x.Email == email);
            }
        }

        public async Task<bool> CreateUser(User u)
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<ZipProjectDbContext>();
                try
                {
                    context.Users.Add(u);
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
