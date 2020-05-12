using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bugtracker.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bugtracker.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _applicationDbContext;
        private readonly UserManager<IdentityUser> _userManager;

        public UserService(ApplicationDbContext applicationDbContext, UserManager<IdentityUser> userManager)
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
        }

        public async Task<List<IdentityUser>> GetUsersAsync()
        {
            var queryable = _applicationDbContext.Users
                .AsQueryable();

            return await queryable.ToListAsync();
        }

        public async Task<IdentityUser> GetUserByUserIdAsync(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);

            return user;
        }

        public async Task<bool> AssignUserRoleAsync(string userId, string rolename)
        {
            var user = await _userManager.FindByIdAsync(userId);

            var result = await _userManager.AddToRoleAsync(user, rolename);
            if (!result.Succeeded)
            {
                return false;
            }

            return true;
        }

    }
}
