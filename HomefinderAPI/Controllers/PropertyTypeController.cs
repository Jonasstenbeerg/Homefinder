using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.PropertyType;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers
{
	[ApiController]
		[Route("api/propertytype")]
		public class PropertyTypeController : ControllerBase
		{
    	private readonly IPropertyTypeRepository _propertObjectRepository;
			
			public PropertyTypeController(IPropertyTypeRepository propertObjectRepository)
			{
      	_propertObjectRepository = propertObjectRepository;
			}

			[HttpPost]
			public async Task<ActionResult> AddPropertyTypeAsync(PostPropertyTypeViewModel model)
			{
				try
				{
					await _propertObjectRepository.AddPropertyTypeAsync(model);

					if (await _propertObjectRepository.SaveAllAsync())
					{
						return StatusCode(201);
					}

					return StatusCode(500, "Ett fel inträffade vid skapande av objektstyp");
				}
				catch (System.Exception ex)
				{
					return StatusCode(500, ex.Message);					
				}
			}
		}
}