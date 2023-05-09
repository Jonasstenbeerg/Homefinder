using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Advertisement;
using HomefinderAPI.ViewModels.Queries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers.V1
{
  [ApiController]
  [Route("api/v1/advertisements")]
  public class AdvertisementsController : ControllerBase
  {
    private readonly IAdvertisementRepository _advertisementRepository;
    
    public AdvertisementsController(IAdvertisementRepository advertisementRepository)
    {
      _advertisementRepository = advertisementRepository;
    }
    
    /// <summary>
    /// Returns all advertisements in the system pointed by the filter
    /// </summary>
    [HttpGet("list")]
    public async Task<ActionResult<List<AdvertisementViewModel>>> GetAll([FromQuery]AdvertisementQuery? query)
    {
      try
      {
        var respons = await _advertisementRepository.ListAllAdvertisementsAsync(query);

        return Ok(respons);
      }
      catch (System.Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<AdvertisementViewModel>> Get(int id)
    {
      var respons = await _advertisementRepository.GetAdvertisementByIdAsync(id);

      if (respons is null)
      {
        return NotFound($"Vi kunde inte hitta någon annons med id {id}");
      }

      return Ok(respons);
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create(PostAdvertisementViewModel model)
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

    [Authorize]
    [HttpPut("{id}")]
    public async Task<ActionResult> Update(int id, PostAdvertisementViewModel model)
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

    [Authorize]
    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(int id)
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