namespace HomefinderAPI.IntegrationTests.Controllers
{
    [TestClass]
	public class AdvertisementsControllerTests : IntegrationTest
	{
		[TestMethod]
		public async Task TestMethod1()
		{
			await AuthenticateAsync();

			var response = await testClient.GetAsync("api/v1/advertisements/list");

			Assert.IsNotNull(response);
			
		}
	}
}