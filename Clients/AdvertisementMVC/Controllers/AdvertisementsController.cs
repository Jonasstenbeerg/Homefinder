using AdvertisementMVC.Models;
using AdvertisementMVC.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace AdvertisementMVC.Controllers
{
  [Route("[controller]")]
	public class AdvertisementsController : Controller
	{
		private readonly ILogger<AdvertisementsController> _logger;
    private readonly IConfiguration _config;
		private readonly AdvertisementServiceModel _advertisementService;

		public AdvertisementsController(ILogger<AdvertisementsController> logger, IConfiguration config)
		{
      _config = config;
			_logger = logger;
			_advertisementService = new AdvertisementServiceModel(_config);
		}

		[HttpGet()]
		public async Task<IActionResult> Index()
		{
			try
			{
				// var advertisements = await _advertisementService.ListAllAdvertisementsAsync();

				// return View("Index", advertisements);
				var ads = new List<AdvertisementViewModel>
    		{
        	new AdvertisementViewModel 
					{ 
						Id = 0,
    				ListPrice = 2400000,
						LeaseType = "Villa",
						LeaseTypeIconUrl = "../images/lease-type-house.png", 
						PropertyType = "Mark",
   					Area =  180,
 						City =  "Gävle",
 						StreetName = "Majorsgatan", 
   					StreetNumber = 13,
						ImageUrl = "../images/ad-1.jpg"
						},
        	new AdvertisementViewModel 
					{ 
						Id = 1,
						ListPrice = 3700000,
						LeaseType = "Lägenhet",
						LeaseTypeIconUrl = "../images/lease-type-apartment.png", 
						PropertyType = "Mark",
   					Area =  75,
 						City =  "Göteborg",
 						StreetName = "Kontorsgatan", 
   					StreetNumber = 15, 
						ImageUrl = "../images/ad-2.jpg"
						},
        	new AdvertisementViewModel 
					{ 
						Id = 2,
						ListPrice = 1570000,
						LeaseType = "Lägenhet",
						LeaseTypeIconUrl = "../images/lease-type-apartment.png",  
						PropertyType = "Mark",
   					Area =  65,
 						City =  "Malmö",
 						StreetName = "Storgatan", 
   					StreetNumber = 5, 
						ImageUrl = "../images/ad-3.jpg"
						}
    		};
				return View(ads);

			}
			catch (System.Exception)
			{
				throw;
			}
		}

    [HttpPost("Find")]
    public async Task<IActionResult> Find(AdvertisementFilterViewModel advertisementFilterViewModel)
    {
      try
      {
        var advertisement = await _advertisementService.FindAdvertisement(advertisementFilterViewModel);
        return View("Index", advertisement);
      }
      catch (Exception ex)
      {
        Console.WriteLine(ex.Message);
        return View("Error");
      }
    }

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View("Error!");
		}
	}
}