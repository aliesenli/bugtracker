using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface IStaffService
    {
        Task<List<IdentityUser>> GetStaffsAsync();

        Task<IdentityUser> GetStaffByUserIdAsync(string userId);

        Task<bool> AssignUserRoleAsync(string userId, string rolename);
    }
}
