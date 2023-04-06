using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HomefinderAPI.Models
{
    public class PropertyObject
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Area { get; set; }
        [Required]
        public int PropertyTypeId { get; set; }
        [Required]
        public int AddressId { get; set; }
        [Required]
        public int LeaseTypeId { get; set; }
        [ForeignKey("PropertyTypeId")]
        public PropertyType? PropertyType { get; set; }
        [ForeignKey("AddressId")]
        public Address? Address { get; set; }
        [ForeignKey("LeaseTypeId")]
        public LeaseType? LeaseType { get; set; }
    }
}