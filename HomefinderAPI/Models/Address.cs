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
        public string? PostalCode { get; set; }
        [Required]
        public string? StreetName { get; set; }
        [Required]
        public string? StreetNumber { get; set; }
    }
}