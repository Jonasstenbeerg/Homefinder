using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.LeaseType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers.V1
{
	[ApiController]
	[Route("api/v1/leasetypes")]
	public class LeaseTypesController : ControllerBase
	{
		private readonly ILeaseTypeRepository _leaseTypeRepository;

		public LeaseTypesController(ILeaseTypeRepository leaseTypeRepository)
		{
			_leaseTypeRepository = leaseTypeRepository;
			
		}

		[HttpGet("list")]
		public async Task<ActionResult<List<LeaseTypeViewModel>>> GetAll()
		{
			var respons = await _leaseTypeRepository.ListAllLeaseTypesAsync();

			return Ok(respons);
		}

		[HttpGet("{id}")]
	public async Task<ActionResult<LeaseTypeViewModel>> Get(int id)
	{
	  var respons = await _leaseTypeRepository.GetLeaseTypeByIdAsync(id);

	  if (respons is null)
	  {
		return NotFound($"Vi kunde inte hitta någon arrendetyp med id {id}");
	  }

	  return Ok(respons);
	}

		[Authorize]
		[HttpPost]
		public async Task<ActionResult> Create(PostLeaseTypeViewModel model)
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

		[Authorize]
		[HttpPut("{id}")]
		public async Task<ActionResult> Update(int id, PostLeaseTypeViewModel model)
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