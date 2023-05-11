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
        var advertisements = await _advertisementService.ListAllAdvertisementsAsync();

        return View(advertisements);
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    [HttpPost("Find")]
    public async Task<IActionResult> Find([FromForm] AdvertisementFilterViewModel filterModel)
    {
      try
      {
        var advertisements = await _advertisementService.ListAllFilteredAdvertisementsAsync(filterModel.Address, filterModel.MinPrice, filterModel.MaxPrice);
        return View("~/Views/Advertisements/Index.cshtml", advertisements);
      }
      catch (System.Exception)
      {
        throw;
      }
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
      return View("Error!");
    }
  }
}