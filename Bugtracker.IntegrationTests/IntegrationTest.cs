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

        protected async Task AuthenticateWithRoleAsync()
        {
            TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtWithManagerRoleAsync());
        }

        protected async Task<ProjectResponse> CreateProjectAsync(CreateProjectRequest request)
        {
            var response = await TestClient.PostAsJsonAsync("api/projects/create", request);
            return (await response.Content.ReadAsAsync<ProjectResponse>());
        }

        protected async Task<TicketResponse> CreateTicketAsync(CreateTicketRequest request)
        {
            var response = await TestClient.PostAsJsonAsync("api/tickets/create", request);
            return (await response.Content.ReadAsAsync<TicketResponse>());
        }

        private async Task<string> GetJwtAsync()
        {
            var response = await TestClient.PostAsJsonAsync("api/identity/register", new UserRegistrationRequest
            {
                Email = "test@integration.com",
                Password = "Test1234!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        private async Task<string> GetJwtWithManagerRoleAsync()
        {

            await TestClient.PostAsJsonAsync("api/identity/register", new UserRegistrationRequest
            {
                Email = "test@integration.com",
                Password = "Test1234!"
            });

            using var serviceScope = _serviceProvider.CreateScope();
            var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
            var userManager = serviceScope.ServiceProvider.GetService<UserManager<IdentityUser>>();
            var testAccount = await userManager.FindByNameAsync("test@integration.com");
            await roleManager.CreateAsync(new IdentityRole("Manager"));
            await userManager.AddToRoleAsync(testAccount, "Manager");

            var response = await TestClient.PostAsJsonAsync("api/identity/login", new UserLoginRequest
            {
                Email = "test@integration.com",
                Password = "Test1234!"
            });

            var registrationResponse = await response.Content.ReadAsAsync<AuthSuccessResponse>();
            return registrationResponse.Token;
        }

        //Dispose In-Memory Database
        public void Dispose()
        {
            using var serviceScope = _serviceProvider.CreateScope();
            var context = serviceScope.ServiceProvider.GetService<ApplicationDbContext>();
            context.Database.EnsureDeleted();
        }
    }
}
