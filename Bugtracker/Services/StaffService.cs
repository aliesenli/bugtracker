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

        public StaffService(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public async Task<List<IdentityUser>> getStaffsAysnc()
        {
            var queryable = _applicationDbContext.Users.AsQueryable();

            return await queryable.ToListAsync();
        }
    }
}
