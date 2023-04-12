using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.LeaseType;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers
{
	[ApiController]
		[Route("api/leasetype")]
		public class LeaseTypeController : ControllerBase
		{
    	private readonly ILeaseTypeRepository _repository;
			public LeaseTypeController(ILeaseTypeRepository repository)
			{
				_repository = repository;
				
			}

			[HttpPost]
			public async Task<ActionResult> AddLeaseTypeAsync(PostLeaseTypeViewModel model)
			{
				try
				{
					await _repository.AddLeaseTypeAsync(model);

					if (await _repository.SaveAllAsync())
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
		}
}