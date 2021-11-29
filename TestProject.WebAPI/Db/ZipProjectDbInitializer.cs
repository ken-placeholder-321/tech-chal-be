using System;
using System.Linq;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Db
{
    public static class ZipProjectDbInitializer
    {
        public static void Initialize(ZipProjectDbContext context)
        {
            context.Database.EnsureCreated();
        }
    }
}