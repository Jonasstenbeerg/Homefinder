using System.ComponentModel.DataAnnotations;

namespace HomefinderAPI.Models
{
	public class PropertyType
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? Name { get; set; }
		[Required]
		public string? Description { get; set; }

		public ICollection<PropertyObject>? PropertyObjects { get; set; }
	}
}