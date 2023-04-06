using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomefinderAPI.Models
{
    public class Advertisement
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public int? PropertyId { get; set; }
        [ForeignKey("PropertyId")]
        public PropertyObject Property { get; set; } = new PropertyObject();
    }
}