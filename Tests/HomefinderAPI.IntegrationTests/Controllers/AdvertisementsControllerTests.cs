using HomefinderAPI.ViewModels.Advertisement;
using HomefinderAPI.ViewModels.Responses;
using System.Net.Http.Json;

namespace HomefinderAPI.IntegrationTests.Controllers
{
  [TestClass]
	public class AdvertisementsControllerTests : IntegrationTest
	{
		//TODO: Leasetypes och propertytypes behövs skapas för testerna, just nu är testerna beroende av våran seedDb....
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
			await TestClient.DeleteAsync("api/v1/advertisements/1");

			var response = await TestClient.GetAsync("api/v1/advertisements/list-all");
			var pagedResponse = await response.Content.ReadFromJsonAsync<List<AdvertisementViewModel>>();
			
			Assert.IsNotNull(pagedResponse);
			Assert.IsTrue(pagedResponse.First().Deleted);
		}

		[TestMethod]
		public async Task Create_WhenValidDataProvided_CreatesAdvertisement()
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

			var response2 = await TestClient.GetAsync($"api/v1/advertisements/1");
			var firstAddResponse = await response2.Content.ReadFromJsonAsync<AdvertisementViewModel>();
			Assert.IsNotNull(firstAddResponse);
		}

		[TestMethod]
		public async Task Update_WhenValidDataProvided_UpdatesAdvertisement()
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
			var expectedAdd = new PostAdvertisementViewModel(){
				ListPrice = 999999,
				Area = 23424234,
				City = "TEst",
				ImageBin = "testtest",
				LeaseType = "Egenrätt",
				PostalCode = 543653,
				PropertyType = "Villa",
				StreetName = "Testvägen",
				StreetNumber = 1
			};
			await AuthenticateAsync();
			await CreatAdvertisementAsync(testAdd1);

			var response = await TestClient.GetAsync("api/v1/advertisements/list");
			var pagedResponse = await response.Content.ReadFromJsonAsync<PagedResponse<AdvertisementViewModel>>();

			var firstAddFromResponse = pagedResponse!.Data.First();

			await UpdateAdvertisementAsync(firstAddFromResponse.Id,expectedAdd);

			var updatedAdvertisementResponse = await TestClient.GetAsync($"api/v1/advertisements/{firstAddFromResponse.Id}");
			var updatedAdvertisement = await updatedAdvertisementResponse.Content.ReadFromJsonAsync<AdvertisementViewModel>();

			Assert.AreEqual(expectedAdd.City,updatedAdvertisement!.City);
			Assert.AreEqual(expectedAdd.ImageBin,updatedAdvertisement.ImageBin);
			Assert.AreEqual(expectedAdd.LeaseType,updatedAdvertisement.LeaseType);
			Assert.AreEqual(expectedAdd.ListPrice,updatedAdvertisement.ListPrice);
			Assert.AreEqual(expectedAdd.PostalCode,int.Parse(updatedAdvertisement.PostalCode!));
			Assert.AreEqual(expectedAdd.Area,updatedAdvertisement.Area);
			Assert.AreEqual(expectedAdd.PropertyType,updatedAdvertisement.PropertyType);
			Assert.AreEqual(expectedAdd.StreetName,updatedAdvertisement.StreetName);
			Assert.AreEqual(expectedAdd.StreetNumber,updatedAdvertisement.StreetNumber);
			
		}

		[TestMethod]
		public async Task Delete_WhenAdvertisementExists_SetsDeletedPropertyToTrue()
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
			await TestClient.DeleteAsync("api/v1/advertisements/1");

			var response = await TestClient.GetAsync("api/v1/advertisements/list-all");
			var pagedResponse = await response.Content.ReadFromJsonAsync<List<AdvertisementViewModel>>();
			
			Assert.IsNotNull(pagedResponse);
			Assert.IsTrue(pagedResponse.First().Deleted);
			
		}
	}
}