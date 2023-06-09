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
			// Arrange
			var testAdd1 = new PostAdvertisementViewModel()
			{
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
			var testAdd2 = new PostAdvertisementViewModel()
			{
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

			await AuthenticateAsync();

			await CreatAdvertisementAsync(testAdd1);
			await CreatAdvertisementAsync(testAdd2);

			// Act
			var response = await TestClient.GetAsync("api/v1/advertisements/list");
			var pagedResponse = await response.Content.ReadFromJsonAsync<PagedResponse<AdvertisementViewModel>>();

			// Assert
			foreach (AdvertisementViewModel advertisement in pagedResponse!.Data)
			{
				Assert.IsFalse(advertisement.Deleted, "Response included deleted advertisement");
			}
		}

		[TestMethod]
		public async Task Get_WithValidId_ReturnsMatchingAdvertisement()
		{
			// Arrange
			var testAdd1 = new PostAdvertisementViewModel()
			{
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

			// Act
			var response = await TestClient.GetAsync("api/v1/advertisements/list");
			var responseAsPage = await response.Content.ReadFromJsonAsync<PagedResponse<AdvertisementViewModel>>();
			var firstAdvertisement = responseAsPage!.Data.First();

			var getByIdResponse = await TestClient.GetAsync($"api/v1/advertisements/{firstAdvertisement.Id}");
			var getByIdAdvertisement = await getByIdResponse.Content.ReadFromJsonAsync<AdvertisementViewModel>();

			// Assert
			Assert.IsNotNull(getByIdAdvertisement);
			Assert.AreEqual(firstAdvertisement.ToString(), getByIdAdvertisement.ToString());
		}

		[TestMethod]
		public async Task GetAll_WhenAdvertisementsExist_ReturnsAdvertisementsIncludingDeletedOnes()
		{
			// Arrange
			var testAdd1 = new PostAdvertisementViewModel()
			{
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

			// Act
			var listAllResponse = await TestClient.GetAsync("api/v1/advertisements/list-all");
			var advertisementList = await listAllResponse.Content.ReadFromJsonAsync<List<AdvertisementViewModel>>();

			// Assert
			Assert.IsNotNull(advertisementList);
			Assert.IsTrue(advertisementList.First().Deleted);
		}

		[TestMethod]
		public async Task Create_WhenValidDataProvided_CreatesAdvertisement()
		{
			// Arrange
			var testAdd1 = new PostAdvertisementViewModel()
			{
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

			// Act
			await CreatAdvertisementAsync(testAdd1);

			var getByIdResponse = await TestClient.GetAsync("api/v1/advertisements/1");
			var getByIdAdvertisement = await getByIdResponse.Content.ReadFromJsonAsync<AdvertisementViewModel>();

			// Assert
			Assert.IsNotNull(getByIdAdvertisement);
		}

		[TestMethod]
		public async Task Update_WhenValidDataProvided_UpdatesAdvertisement()
		{
			// Arrange
			var testAdd1 = new PostAdvertisementViewModel()
			{
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
			var expectedAdd = new PostAdvertisementViewModel()
			{
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
			var responseAsPage = await response.Content.ReadFromJsonAsync<PagedResponse<AdvertisementViewModel>>();
			var firstAdvertisement = responseAsPage!.Data.First();

			// Act
			await UpdateAdvertisementAsync(firstAdvertisement.Id, expectedAdd);

			var updatedAdvertisementResponse = await TestClient.GetAsync($"api/v1/advertisements/{firstAdvertisement.Id}");
			var updatedAdvertisement = await updatedAdvertisementResponse.Content.ReadFromJsonAsync<AdvertisementViewModel>();

			// Assert
			Assert.AreEqual(expectedAdd.City, updatedAdvertisement!.City);
			Assert.AreEqual(expectedAdd.ImageBin, updatedAdvertisement.ImageBin);
			Assert.AreEqual(expectedAdd.LeaseType, updatedAdvertisement.LeaseType);
			Assert.AreEqual(expectedAdd.ListPrice, updatedAdvertisement.ListPrice);
			Assert.AreEqual(expectedAdd.PostalCode, int.Parse(updatedAdvertisement.PostalCode!));
			Assert.AreEqual(expectedAdd.Area, updatedAdvertisement.Area);
			Assert.AreEqual(expectedAdd.PropertyType, updatedAdvertisement.PropertyType);
			Assert.AreEqual(expectedAdd.StreetName, updatedAdvertisement.StreetName);
			Assert.AreEqual(expectedAdd.StreetNumber, updatedAdvertisement.StreetNumber);
		}

		[TestMethod]
		public async Task Delete_WhenAdvertisementExists_SetsDeletedPropertyToTrue()
		{
			// Arrange
			var testAdd1 = new PostAdvertisementViewModel()
			{
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

			// Act
			await TestClient.DeleteAsync("api/v1/advertisements/1");

			var listAllResponse = await TestClient.GetAsync("api/v1/advertisements/list-all");
			var advertisementList = await listAllResponse.Content.ReadFromJsonAsync<List<AdvertisementViewModel>>();

			// Assert
			Assert.IsNotNull(advertisementList);
			Assert.IsTrue(advertisementList.First().Deleted);
		}
	}
}