using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Bugtracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly UserManager<IdentityUser> _userManager;

        public UserController(IUserService userService, UserManager<IdentityUser> userManager)
        {
            _userService = userService;
            _userManager = userManager;
        }

        [HttpGet("api/users")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetUsersAsync();

            var userResponse = users.Select(user => new UserResponse
            {
                UserId = user.Id,
                Name = user.UserName,
                Role = getRole(user)
            });

            IList<string> getRole(IdentityUser user)
            {
                var role = _userManager.GetRolesAsync(user);

                return role.Result;
            }

            return Ok(userResponse);
        }

        [HttpPost("api/users/role")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> AssignRole([FromBody] AssignRoleRequest request)
        {
            await _userService.AssignUserRoleAsync(request.User, request.Role);

            var user = await _userService.GetUserByUserIdAsync(request.User);

            var userResponse = new UserResponse
            {
                UserId = user.Id,
                Name = user.UserName,
                Role = getRole(user)
            };

            IList<string> getRole(IdentityUser user)
            {
                var role = _userManager.GetRolesAsync(user);

                return role.Result;
            }

            return Ok(userResponse);
        }
    }
}