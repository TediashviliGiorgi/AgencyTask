using Microsoft.AspNetCore.Mvc;
using UserManagementSystem.Models.Requests;
using UserManagementSystem.Services;

namespace UserManagementSystem.Controllers
{
    [Route("api/v1/users")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IUserRoleService _userRoleService;

        public UsersController(IUserService userService, IUserRoleService userRoleService)
        {
            _userService = userService;
            _userRoleService = userRoleService;
        }

        [HttpPost("create-user")]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserRequest request)
        {
            var newUser = await _userService.CreateUser(request);
            return CreatedAtRoute(nameof(GetUserById), new { id = newUser.Id }, newUser);
        }

        [HttpPut("update-user/{userId}")]
        public async Task<IActionResult> UpdateUser(int userId, [FromBody] UpdateUserRequest request)
        {
            var updatedUser = await _userService.UpdateUser(userId, request);
            return Ok(updatedUser);
        }

        [HttpDelete("delete-user/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            await _userService.DeleteUser(userId);
            return NoContent();
        }

        [HttpGet("get-user-by-id/{id}", Name = nameof(GetUserById))]
        public async Task<IActionResult> GetUserById(int id)
        {
            var user = await _userService.GetUserById(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpGet("get-user-by-role/{roleId}")]
        public async Task<IActionResult> GetUsersByRole(int roleId)
        {
            var users = await _userRoleService.GetUsersByRole(roleId);
            return Ok(users);
        }
    }
}
