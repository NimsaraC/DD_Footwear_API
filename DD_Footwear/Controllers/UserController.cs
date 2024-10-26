using DD_Footwear.DTOs;
using DD_Footwear.Services;
using Microsoft.AspNetCore.Mvc;

namespace DD_Footwear.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserRegistration userCreateDto)
        {
            await _userService.AddNewUserAsync(userCreateDto);
            return Ok(userCreateDto);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            return Ok(user);
        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginDto loginDto)
        {
            if (loginDto == null)
            {
                return BadRequest("Login data required");
            }

            var user = await _userService.UserLoginAsync(loginDto);

            if (user == null)
            {
                return Unauthorized();
            }

            return Ok(user);
        }



    }
}
