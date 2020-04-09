using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Contracts.Responses;
using Bugtracker.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Bugtracker.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StaffController : Controller
    {
        private readonly IStaffService _staffService;

        public StaffController(IStaffService staffService)
        {
            _staffService = staffService;
        }

        [HttpGet("api/staffs")]
        public async Task<IActionResult> GetAll()
        {
            var staffs = await _staffService.getStaffsAysnc();
            var staffResponse = staffs.Select(staff => new StaffResponse
            {
                StaffId = staff.Id,
                Name = staff.UserName
            });

            return Ok(staffResponse);
        }
    }
}