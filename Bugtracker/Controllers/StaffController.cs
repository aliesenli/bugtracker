using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Responses;
using Bugtracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;
        private readonly UserManager<IdentityUser> _userManager;

        public StaffController(IStaffService staffService, UserManager<IdentityUser> userManager)
        {
            _staffService = staffService;
            _userManager = userManager;
        }

        [HttpGet("api/staffs")]
        public async Task<IActionResult> GetAll()
        {
            var staffs = await _staffService.getStaffsAsync();

            var staffResponse = staffs.Select(staff => new StaffResponse
            {
                StaffId = staff.Id,
                Name = staff.UserName,
                Role = getRole(staff)
            });

            IList<string> getRole(IdentityUser staff)
            {
                var role = _userManager.GetRolesAsync(staff);

                return role.Result;
            }

            return Ok(staffResponse);
        }

        public async Task<IActionResult> AssignRole()
        {
            //var x = await _staffService.assignStaffRoleAsync();

            return Ok();
        }
    }
}