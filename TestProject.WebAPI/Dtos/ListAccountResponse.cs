using System.Collections.Generic;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Dtos
{
    public class ListAccountResponse
    {
        public List<Account> User { get; set; }
    }
}