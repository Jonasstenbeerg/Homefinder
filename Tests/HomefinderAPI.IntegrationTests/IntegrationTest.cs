using System.Net.Http.Headers;
using System.Net.Http.Json;
using HomefinderAPI.Data;
using HomefinderAPI.ViewModels.Advertisement;
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
		protected readonly HttpClient TestClient;

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
			TestClient = appFactory.CreateClient();
		}

		protected async Task AuthenticateAsync()
		{
			TestClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", await GetJwtAsync());
		}

		protected async Task<bool> CreatAdvertisementAsync(PostAdvertisementViewModel model)
		{
			var response = await TestClient.PostAsJsonAsync("api/v1/advertisements",model);

			return response.IsSuccessStatusCode;
		}

		protected async Task<bool> UpdateAdvertisementAsync(int id,PostAdvertisementViewModel model)
		{
			var response = await TestClient.PutAsJsonAsync($"api/v1/advertisements/{id}",model);

			return response.IsSuccessStatusCode;
		}

		private async Task<string> GetJwtAsync()
		{
			var response = await TestClient.PostAsJsonAsync("api/v1/auth/login", new LoginUserViewModel{
				UserName = "test@test.com",
				Password = "Jonas123456",
			});

			var registrationResponse = await response.Content.ReadFromJsonAsync<UserViewModel>();
			return registrationResponse!.Token!;
		}
	}
}