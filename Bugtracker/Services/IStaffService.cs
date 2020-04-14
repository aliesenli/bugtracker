using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bugtracker.Services
{
    public interface IStaffService
    {
        Task<List<IdentityUser>> getStaffsAsync();

        Task<IdentityUser> assignStaffRoleAsync(IdentityUser user);
    }
}
