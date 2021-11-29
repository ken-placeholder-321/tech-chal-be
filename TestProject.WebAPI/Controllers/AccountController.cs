using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TestProject.WebAPI.Dtos;
using TestProject.WebAPI.Logger;
using TestProject.WebAPI.Services;

namespace TestProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class AccountController: ControllerBase
    {
        private readonly IAccountService _accountService;
        private readonly ISimpleLogger _logger;

        public AccountController( IAccountService accountService, ISimpleLogger logger)
        {
            _accountService = accountService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("list")]
        public async Task<ActionResult<ListAccountResponse>> GetAllAccounts()
        {
            var users = await _accountService.GetAllAccounts();
            return Ok(users);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateAccountResponse>> CreateAccount(CreateAccountRequest request)
        {
            var response = await _accountService.CreateAccount(request);
            
            if (!response.Success && response.Status == System.Net.HttpStatusCode.NotFound)
            {
                _logger.Log(response.ErrorMessage);
                return NotFound(response.ErrorMessage);
            }

            if (!response.Success && response.Status == System.Net.HttpStatusCode.BadRequest)
            {
                _logger.Log(response.ErrorMessage);
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response);
        }
        
    }
}