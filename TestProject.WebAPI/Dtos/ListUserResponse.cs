using System.Collections.Generic;
using TestProject.WebAPI.Repository.Models;

namespace TestProject.WebAPI.Dtos
{
    public class ListUserResponse
    {
        public List<User> User { get; set; }
    }
}