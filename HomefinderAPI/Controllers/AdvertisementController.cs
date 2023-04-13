using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Advertisement;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers
{  
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

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateAdvertisementAsync(int id, PostAdvertisementViewModel model)
    {
      try
      {
        await _advertisementRepository.UpdateAdvertisementAsync(id, model);

        if(await _advertisementRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Ett fel inträffade vid uppdatering av annons");
      }
      catch (System.Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteAdvertisementAsync(int id)
    {
      try
      {
        await _advertisementRepository.DeleteAdvertisementAsync(id);

        if (await _advertisementRepository.SaveAllAsync())
        {
          return NoContent();
        }

        return StatusCode(500, "Ett fel inträffade vid borttagning av annons");        
      }
      catch (System.Exception ex)
      {
        return StatusCode(500,ex.Message);
      }
    }
  }
}