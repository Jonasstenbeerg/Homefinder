using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.PropertyType;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomefinderAPI.Controllers.V1
{
	[ApiController]
	[Route("api/v1/propertytypes")]
	public class PropertyTypesController : ControllerBase
	{
		private readonly IPropertyTypeRepository _propertObjectRepository;

		public PropertyTypesController(IPropertyTypeRepository propertObjectRepository)
		{
		_propertObjectRepository = propertObjectRepository;
		}

		[HttpGet("list")]
		public async Task<ActionResult<List<PropertyTypeViewModel>>> GetAll()
		{
			var respons = await _propertObjectRepository.ListAllPropertyTypesAsync();

			return Ok(respons);
		}

		[HttpGet("{id}")]
		public async Task<ActionResult<PropertyTypeViewModel>> Get(int id)
		{
			var respons = await _propertObjectRepository.GetPropertyTypeByIdAsync(id);

			if (respons is null)
			{
				return NotFound($"Vi kunde inte hitta någon objektstyp med id {id}");
			}

			return Ok(respons);
		}

		[Authorize]
		[HttpPut("id")]
		public async Task<ActionResult> Update(int id, PostPropertyTypeViewModel model)
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

		[Authorize]
		[HttpPost]
		public async Task<ActionResult> Create(PostPropertyTypeViewModel model)
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