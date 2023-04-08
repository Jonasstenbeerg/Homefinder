using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Advertisement;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers
{  
  //Endpointens namn ska vara ett substantiv i plural
  //i detta fall advertisements
  [ApiController]
  [Route("api/advertisements")]
  public class AdvertisementController : ControllerBase
  {
    private readonly IAdvertisementRepository _advertisementRepository;
    
    public AdvertisementController(IAdvertisementRepository advertisementRepository)
    {
      _advertisementRepository = advertisementRepository;
    }

    [HttpGet("list")]
    public async Task<ActionResult<List<AdvertisementViewModel>>> ListAllAdvertisementsAsync()
    {
      var respons = await _advertisementRepository.ListAllAdvertisementsAsync();

      return Ok(respons);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdvertisementViewModel>> GetAdvertisementByIdAsync(int id)
    {
      var respons = await _advertisementRepository.GetAdvertisementByIdAsync(id);

      if (respons is null)
      {
        return NotFound($"Vi kunde inte hitta någon annons med id {id}");
      }

      return Ok(respons);
    }

    [HttpPost]
    public async Task<ActionResult> AddAdvertisementAsync(PostAdvertisementViewModel model)
    {
      try
      {
        await _advertisementRepository.AddAdvertisementAsync(model);

        if(await _advertisementRepository.SaveAllAsync())
        {
          return StatusCode(201);
        }

        return StatusCode(500,"Ett fel inträffade vid skapande av annons");
      }
      catch (System.Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
  }
}