using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.PropertyType;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers
{
	[ApiController]
		[Route("api/propertytypes")]
		public class PropertyTypeController : ControllerBase
		{
    	private readonly IPropertyTypeRepository _propertObjectRepository;

			public PropertyTypeController(IPropertyTypeRepository propertObjectRepository)
			{
      	_propertObjectRepository = propertObjectRepository;
			}

			[HttpGet("list")]
			public async Task<ActionResult<List<PropertyTypeViewModel>>> ListAllPropertyTypesAsync()
			{
				var respons = await _propertObjectRepository.ListAllPropertyTypesAsync();

				return Ok(respons);
			}

			[HttpGet("{id}")]
			public async Task<ActionResult<PropertyTypeViewModel>> GetPropertyTypeByIdAsync(int id)
			{
				var respons = await _propertObjectRepository.GetPropertyTypeByIdAsync(id);

				if (respons is null)
				{
					return NotFound($"Vi kunde inte hitta någon objektstyp med id {id}");
				}

				return Ok(respons);
			}

			[HttpPut("id")]
			public async Task<ActionResult> UpdatePropertTypeAsync(int id, PostPropertyTypeViewModel model)
			{
				try
				{
					await _propertObjectRepository.UpdatePropertyTypeAsync(id, model);

					if (await _propertObjectRepository.SaveAllAsync())
					{
						return StatusCode(201);
					}

					return StatusCode(500,"Något gick fel vid uppdatering av objektstyp");
				}
				catch (System.Exception ex)
				{
					return StatusCode(500, ex.Message);
				}
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