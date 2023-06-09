using HomefinderAPI.Helpers;
using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Advertisement;
using HomefinderAPI.ViewModels.Queries;
using HomefinderAPI.ViewModels.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OutputCaching;

namespace HomefinderAPI.Controllers.V1
{
  [ApiController]
  [Route("api/v1/advertisements")]
  public class AdvertisementsController : ControllerBase
  {
    private readonly IAdvertisementRepository _advertisementRepository;
    private readonly IUriRepository _uriRepository;
    private readonly IOutputCacheStore _cacheStore;
    
    public AdvertisementsController(IAdvertisementRepository advertisementRepository, IUriRepository uriRepository, IOutputCacheStore cacheStore)
    {
      _uriRepository = uriRepository;
      _cacheStore = cacheStore;
      _advertisementRepository = advertisementRepository;
    }
    
    /// <summary>
    /// Returns all available advertisements in the system pointed by the filter
    /// </summary>
    [OutputCache(PolicyName = "list-cache")]
    [HttpGet("list")]
    public async Task<ActionResult<List<AdvertisementViewModel>>> GetAllAvailable([FromQuery]PaginitationQuery pageQuery, [FromQuery]AdvertisementQuery addQuery)
    {
      try
      {
        var respons = await _advertisementRepository.ListAllAvailableAdvertisementsAsync(pageQuery, addQuery);
       
        var paginationResponse = PaginationHelper.CreatePaginatedResponse(_uriRepository, respons, pageQuery, addQuery);
        
        return Ok(paginationResponse);
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

    /// <summary>
    /// Returns all advertisements including deleted ones
    /// </summary>
    [Authorize]
    [HttpGet("list-all")]
    public async Task<ActionResult<List<AdvertisementViewModel>>> GetAll()
    {
      try
      {
        var respons = await _advertisementRepository.ListAllAdvertisementsAsync();

        return Ok(respons);
      }
      catch (System.Exception ex)
      {
        return StatusCode(500, ex.Message);
      }
    }
    
    [Authorize]
    [HttpPost]
    public async Task<ActionResult> Create(PostAdvertisementViewModel model,CancellationToken ct)
    {
      try
      {
        await _advertisementRepository.AddAdvertisementAsync(model);

        if(await _advertisementRepository.SaveAllAsync())
        {
          await _cacheStore.EvictByTagAsync("list",ct);
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
    public async Task<ActionResult> Update(int id, PostAdvertisementViewModel model,CancellationToken ct)
    {
      try
      {
        await _advertisementRepository.UpdateAdvertisementAsync(id, model);

        if(await _advertisementRepository.SaveAllAsync())
        {
          await _cacheStore.EvictByTagAsync("list",ct);
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