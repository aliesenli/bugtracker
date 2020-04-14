using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bugtracker.Services
{
    public class StaffService : IStaffService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public StaffService(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<List<IdentityUser>> getStaffsAsync()
        {
            var queryable = _applicationDbContext.Users
                .AsQueryable();

            return await queryable.ToListAsync();
        }

        public Task<IdentityUser> assignStaffRoleAsync(IdentityUser user)
        {
            throw new NotImplementedException();
        }
    }
}
