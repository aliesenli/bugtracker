using Bugtracker.Contracts.Responses;
using Bugtracker.Converters;
using Bugtracker.Data;
using Bugtracker.Domain;
using Bugtracker.Repositories;
using Bugtracker.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace Bugtracker.Installers
{
    public class DbInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("DefaultConnection")));
            services.AddDefaultIdentity<IdentityUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddScoped<ITicketService, TicketService>();
            services.AddScoped<IProjectService, ProjectService>();
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<IProjectRepository, ProjectRepository>();
            services.AddScoped<ITicketRepository, TicketRepository>();

            services.AddTransient<IConverter<Project, ProjectResponse>, ProjectToDtoConverter>();
            services.AddTransient<IConverter<IList<Project>, IList<ProjectResponse>>, ProjectToDtoConverter>();

            services.AddTransient<IConverter<Ticket, TicketResponse>, TicketToDtoConverter>();
            services.AddTransient<IConverter<IList<Ticket>, IList<TicketResponse>>, TicketToDtoConverter>();
        }
    }
}
