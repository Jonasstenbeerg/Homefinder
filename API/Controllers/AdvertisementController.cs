using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interfaces;
using API.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
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
    }
}