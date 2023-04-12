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
    private readonly Mock<IAdvertisementRepository>? _mockedRepository;
    private readonly AdvertisementController? _sut; //System Under Test

		public AdvertisementControllerTest()
		{
			_mockedRepository = new Mock<IAdvertisementRepository>();
			_sut = new AdvertisementController(_mockedRepository.Object); 
		}
		
		[TestMethod]
		public async Task GetAdvertisementByIdAsync_Should_Return_OK_Respons_When_Data_Found()
		{
			//Arrange
			var id = 1;
			var advertisement = new AdvertisementViewModel();
			_mockedRepository!.Setup(repo => repo.GetAdvertisementByIdAsync(id)).ReturnsAsync(advertisement);

			//Act
			var respons = await _sut!.GetAdvertisementByIdAsync(id);

			//Assert
			_mockedRepository.Verify(repo => repo.GetAdvertisementByIdAsync(id), Times.Once);
			Assert.IsInstanceOfType(respons.Result, typeof(OkObjectResult));
		}

		[TestMethod]
		public async Task GetAdvertisementByIdAsync_Should_Return_NotFound_Respons_When_Data_Not_Found()
		{
			var id = 2;

			var respons = await _sut!.GetAdvertisementByIdAsync(id);

			_mockedRepository!.Verify(repo => repo.GetAdvertisementByIdAsync(id), Times.Once);
			Assert.IsInstanceOfType(respons.Result, typeof(NotFoundObjectResult));
		}

		[TestMethod]
		public async Task ListAllAdvertisementsAsync_Should_Return_OK_Respons_And_Not_Be_Null()
		{
			var advertisements = new List<AdvertisementViewModel>();
			_mockedRepository!.Setup(repo => repo.ListAllAdvertisementsAsync()).ReturnsAsync(advertisements);

			var respons = await _sut!.ListAllAdvertisementsAsync();

			_mockedRepository!.Verify(repo => repo.ListAllAdvertisementsAsync(), Times.Once);
			Assert.IsNotNull(respons);
			Assert.IsInstanceOfType(respons.Result, typeof(OkObjectResult));
		}
		//TODO: Create test for all controller methods to check if right status code is returned
	}
}