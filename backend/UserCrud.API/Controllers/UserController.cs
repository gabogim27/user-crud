namespace UserCrud.API.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using UserCrud.Domain.Common;
    using UserCrud.Domain.DTOs;
    using UserCrud.Domain.Services;

    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(ILogger<UserController> logger, IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserDto>>> Get([FromQuery] Pagination pagination)
        {
            var result = await _userService.GetUsers(pagination);
            return Ok(result);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserDto>> Get(int userId)
        {
            var result = await _userService.GetUser(userId);
            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<UserDto>> Post([FromBody] UserDto userDto)
        {
            var result = await _userService.AddUser(userDto);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDto userDto)
        {
            await _userService.UpdateUser(userDto);
            return Ok();
        }

        [HttpDelete("{userId}")]
        public async Task<IActionResult> Delete(int userId)
        {
            await _userService.DeleteUser(userId);
            return Ok();
        }
    }
}