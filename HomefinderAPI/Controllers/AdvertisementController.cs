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
            try
            {
                var respons = await _advertisementRepository.ListAllAdvertisementsAsync();

                return Ok(respons);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(500,ex.Message);
            }
        }

        [HttpGet("byid/{id}")]
        public async Task<ActionResult<AdvertisementViewModel>> GetAdvertisementByIdAsync(int id)
        {
            try
            {
               var respons = await _advertisementRepository.GetAdvertisementByIdAsync(id);

               return Ok(respons);
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(500,ex.Message);
            }
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

                return StatusCode(500,"Ett fel inträffade skapande av annons");
            }
            catch (System.Exception ex)
            {
                
                return StatusCode(500, ex.Message);
            }
        }
    }
}