using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.LeaseType;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers
{
	[ApiController]
	[Route("api/leasetype")]
	public class LeaseTypeController : ControllerBase
	{
		private readonly ILeaseTypeRepository _leaseTypeRepository;
		public LeaseTypeController(ILeaseTypeRepository leaseTypeRepository)
		{
			_leaseTypeRepository = leaseTypeRepository;
			
		}

		[HttpGet("list")]
		public async Task<ActionResult<List<LeaseTypeViewModel>>> ListAllAdvertisementsAsync()
		{
			var respons = await _leaseTypeRepository.ListAllLeaseTypesAsync();

			return Ok(respons);
		}

		[HttpGet("{id}")]
    public async Task<ActionResult<LeaseTypeViewModel>> GetLeaseTypeByIdAsync(int id)
    {
      var respons = await _leaseTypeRepository.GetLeaseTypeByIdAsync(id);

      if (respons is null)
      {
        return NotFound($"Vi kunde inte hitta någon arrendetyp med id {id}");
      }

      return Ok(respons);
    }

		[HttpPost]
		public async Task<ActionResult> AddLeaseTypeAsync(PostLeaseTypeViewModel model)
		{
			try
			{
				await _leaseTypeRepository.AddLeaseTypeAsync(model);

				if (await _leaseTypeRepository.SaveAllAsync())
				{
					return StatusCode(201);
				}

				return StatusCode(500,"Något gick fel skapande av arrendetyp");
			}
			catch (System.Exception ex)
			{
				return StatusCode(500, ex.Message);					
			}
		}

		[HttpPut("{id}")]
		public async Task<IActionResult> UpdateLeaseTypeAsync(int id, PostLeaseTypeViewModel model)
		{
			try
			{
				await _leaseTypeRepository.UpdateLeaseTypeAsync(id, model);

				if(await _leaseTypeRepository.SaveAllAsync())
				{
					return NoContent();
				}

				return StatusCode(500, "Ett fel inträffade vid uppdatering av arrendetyp");
			}
			catch (System.Exception ex)
			{
				return StatusCode(500, ex.Message);
			}
		}
	}
}