using System.ComponentModel.DataAnnotations;

namespace HomefinderAPI.ViewModels.PropertyType
{
	public class PostPropertyTypeViewModel
		{
			[Required(ErrorMessage = "Namn för arrendetyp är obligatoriskt")]
			public string? Name { get; set; }
			[Required(ErrorMessage = "Beskrivning för arrendetyp är obligatoriskt")]
			public string? Description { get; set; }
		}
}