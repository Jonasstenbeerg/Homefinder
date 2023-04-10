using HomefinderAPI.Controllers;
using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Advertisement;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace HomefinderAPI.Tests.Controllers
{
  [TestClass]
	public class AdvertisementControllerTest
	{
		private AdvertisementController? _controller;
		private AdvertisementViewModel? _mockedAdvertisement;

		[TestInitialize]
		public void TestInitialize()
		{
			var mockedRepository = new Mock<IAdvertisementRepository>();
			var id = 1;
			_mockedAdvertisement = new AdvertisementViewModel { Id = id };
			mockedRepository.Setup(repo => repo.GetAdvertisementByIdAsync(id)).ReturnsAsync( _mockedAdvertisement);
			_controller = new AdvertisementController(mockedRepository.Object);
		}
		[TestMethod]
		public async Task GetAdvertisementByIdAsync_Should_Return_Statuscode_200_ON_Success()
		{
			var id = 1;

			var respons = await _controller!.GetAdvertisementByIdAsync(id);

			Assert.IsInstanceOfType(respons.Result, typeof(OkObjectResult));
		}

		[TestMethod]
		public async Task GetAdvertisementByIdAsync_Should_Return_Statuscode_404_ON_NotFound()
		{
			var id = 2;

			var respons = await _controller!.GetAdvertisementByIdAsync(id);

			Assert.IsInstanceOfType(respons.Result, typeof(NotFoundObjectResult));
		}
		//TODO: Create test for all controller methods to check if right status code is returned
	}
}