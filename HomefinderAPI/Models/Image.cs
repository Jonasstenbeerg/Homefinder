using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomefinderAPI.Models
{
	public class Image
	{
		[Key]
		public int Id { get; set; }
		[Required]
		public string? ImageBin { get; set; }
		[Required]
		public int PropertyObjectId { get; set; }
		[ForeignKey("PropertyObjectId")]
		public PropertyObject? PropertyObject { get; set; }
	}
}