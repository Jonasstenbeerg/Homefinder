using System.Net.Http.Headers;
using System.Net.Http.Json;
using HomefinderAPI.Data;
using HomefinderAPI.ViewModels.Authorization;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace HomefinderAPI.IntegrationTests
{
    public class IntegrationTest
	{
		protected readonly HttpClient testClient;

		protected IntegrationTest()
		{
			var appFactory = new WebApplicationFactory<Program>()
				.WithWebHostBuilder(builder => {
					builder.ConfigureServices(services => {
						services.RemoveAll(typeof(HomefinderContext));
						services.AddDbContext<HomefinderContext>(options => {
							options.UseInMemoryDatabase("IntegrationTestDb");
						});
					});
				});
			testClient = appFactory.CreateClient();
		}

		protected async Task AuthenticateAsync()
		{
			testClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
		}

		private async Task<string> GetJwtAsync()
		{
			var response = await testClient.PostAsJsonAsync("api/v1/auth/login", new LoginUserViewModel{
				UserName = "test@test.com",
				Password = "Jonas123456",
			});

			var registrationResponse = await response.Content.ReadFromJsonAsync<UserViewModel>();
			return registrationResponse!.Token!;
		}
	}
}