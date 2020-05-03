using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Bugtracker.Data;
using Bugtracker.Contracts.Requests;
using Bugtracker.Contracts.Responses;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.AspNetCore.Identity;
using System.Collections;

namespace Bugtracker.IntegrationTests
{
    public class IntegrationTest : IDisposable
    {
        protected readonly HttpClient TestClient;
        private readonly IServiceProvider _serviceProvider;

        protected IntegrationTest()
        {
            var appFactory = new WebApplicationFactory<Startup>()
                .WithWebHostBuilder(builder =>
                {
                    builder.ConfigureServices(services =>
                    {
                        services.RemoveAll(typeof(ApplicationDbContext));
                        services.AddDbContext<ApplicationDbContext>(options => { options.UseInMemoryDatabase("TestDb"); });
                    });
                });

            _serviceProvider = appFactory.Services;
            TestClient = appFactory.CreateClient();
        }

        protected async Task AuthenticateAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
        }

        protected async Task<TicketResponse> CreateTicketAsync(CreateTicketRequest request)
        {
            var response = await TestClient.PostAsJsonAsync("api/tickets/create", request);
            return (await response.Content.ReadAsAsync<TicketResponse>());
        }

        protected async Task<ProjectResponse> CreateProjectAsync(CreateProjectRequest request)
        {
            var response = await TestClient.PostAsJsonAsync("api/projects/create", request);
            return (await response.Content.ReadAsAsync<ProjectResponse>());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("api/identity/register", new UserRegistrationRequest
            {
                Email = "test@integration.com",
                Password = "Test1234!"
            });

            using (var provider = _serviceProvider.CreateScope())
            {
                var userManager = provider.ServiceProvider.GetService<UserManager<IdentityUser>>();
                var roleManager = provider.ServiceProvider.GetService<RoleManager<IdentityRole>>();

                var roleExists = await roleManager.RoleExistsAsync("Manager");
                if (!roleExists)
                {
                    var adminRole = new IdentityRole("Manager");
                    await roleManager.CreateAsync(adminRole);
                    var testUser = await userManager.FindByNameAsync("test@integration.com");
                    await userManager.AddToRoleAsync(testUser, "Manager");
                }

            }


            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        //Dispose Database
        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }
    }
}
