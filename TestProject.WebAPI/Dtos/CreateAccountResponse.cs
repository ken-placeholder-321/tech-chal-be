using System.Net;

namespace TestProject.WebAPI.Dtos
{
    public class CreateAccountResponse
    {
        public bool Success { get; set; }
        public HttpStatusCode Status {get;set;}
        public string? ErrorMessage { get; set; }
    }
}
