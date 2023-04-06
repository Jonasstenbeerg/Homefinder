using System.ComponentModel.DataAnnotations;

namespace HomefinderAPI.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? City { get; set; }
        [Required]
        [Display(Name = "Postal Code")]
        public string? PostalCode { get; set; }
        [Required]
        [Display(Name = "Street Address")]
        public string? StreetAddress { get; set; }
        
        public ICollection<PropertyObject>? PropertyObjects { get; set; }
    }
}