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
    public class UserController: ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISimpleLogger _logger;

        public UserController(IUserService userService, ISimpleLogger logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<GetUserResponse>> GetUser([FromQuery] string email)
        {
            var user = await _userService.GetUser(email);
           
            if (user == null)
            {
                _logger.Log($"Email: {email} not found");
                return NotFound($"Email: {email} not found");
            }
            return Ok(user);
        }
        
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [Route("list")]
        public async Task<ActionResult<ListUserResponse>> GetAllUsers()
        {
            var users = await _userService.GetAllUsers();
            return Ok(users);
        }
        
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ActionResult<CreateUserResponse>> CreateUser(CreateUserRequest request)
        {
            var response = await _userService.CreateUser(request);
            
            if (!response.Success)
            {
                _logger.Log(response.ErrorMessage);
                return BadRequest(response.ErrorMessage);
            }

            return Ok(response);
        }
        
    }
}