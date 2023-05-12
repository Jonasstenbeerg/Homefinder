using HomefinderAPI.ViewModels.Advertisement;
using HomefinderAPI.ViewModels.Responses;
using System.Net.Http.Json;

namespace HomefinderAPI.IntegrationTests.Controllers
{
  [TestClass]
	public class AdvertisementsControllerTests : IntegrationTest
	{
		[TestMethod]
		public async Task GetAllAvailable_WhenAdvertisementsExist_ReturnsAdvertisementsWithDeletedEqualsToFalse()
		{
			var testAdd1 = new PostAdvertisementViewModel(){
				ListPrice = 200,
				Area = 200,
				City = "Gävle",
				ImageBin = "test",
				LeaseType = "Bostadsrätt",
				PostalCode = 1337,
				PropertyType = "Lägenhet",
				StreetName = "Testgatan",
				StreetNumber = 22
			};
			var testAdd2 = new PostAdvertisementViewModel(){
				ListPrice = 400,
				Area = 120,
				City = "Stockholm",
				ImageBin = "test2",
				LeaseType = "Egenrätt",
				PostalCode = 13344,
				PropertyType = "Villa",
				StreetName = "Testvägen",
				StreetNumber = 24,
				Deleted = true
			};

			//TODO: skapa leasetypes och property types för annonserna innan dom annonserna skapas
			await AuthenticateAsync();
			await CreatAdvertisementAsync(testAdd1);
			await CreatAdvertisementAsync(testAdd2);
			
			var response = await TestClient.GetAsync("api/v1/advertisements/list");
			var pagedResponse = await response.Content.ReadFromJsonAsync<PagedResponse<AdvertisementViewModel>>();
			
			
			foreach (AdvertisementViewModel Add in pagedResponse!.Data)
			{
				if (Add.Deleted)
				{
					Assert.Fail("Response included deleted advertisement");
				}
			}
		}

		[TestMethod]
		public async Task Get_WithValidId_ReturnsMatchingAdvertisement()
		{
			var testAdd1 = new PostAdvertisementViewModel(){
				ListPrice = 200,
				Area = 200,
				City = "Gävle",
				ImageBin = "test",
				LeaseType = "Bostadsrätt",
				PostalCode = 1337,
				PropertyType = "Lägenhet",
				StreetName = "Testgatan",
				StreetNumber = 22
			};
			await AuthenticateAsync();
			await CreatAdvertisementAsync(testAdd1);
			var response = await TestClient.GetAsync("api/v1/advertisements/list");
			var pagedResponse = await response.Content.ReadFromJsonAsync<PagedResponse<AdvertisementViewModel>>();

			var firstAdd = pagedResponse!.Data.First();

			var response2 = await TestClient.GetAsync($"api/v1/advertisements/{firstAdd.Id}");
			var firstAddResponse = await response2.Content.ReadFromJsonAsync<AdvertisementViewModel>();
			
			Assert.IsNotNull(firstAddResponse);
			Assert.AreEqual(firstAdd.ToString(), firstAddResponse.ToString());
		}

		[TestMethod]
		public async Task GetAll_WhenAdvertisementsExist_ReturnsAdvertisementsIncludingDeletedOnes()
		{
			await AuthenticateAsync();

			var response = await TestClient.GetAsync("api/v1/advertisements/list-all");
			
			Assert.IsNotNull(response);
			
		}

		[TestMethod]
		public async Task Create_WhenValidDataProvided_CreatesAdvertisement()
		{
			//Testa att skapa en och kolla därefter om den finns i db
			
		}

		[TestMethod]
		public async Task Update_WhenValidDataProvided_UpdatesAdvertisement()
		{
			//Uppdatera ett object
			
		}

		[TestMethod]
		public async Task Delete_WhenAdvertisementExists_SetsDeletedPropertyToTrue()
		{
			//skapa en annons och deletea den sen hämta den och kolla att deleted är true
			
		}
	}
}