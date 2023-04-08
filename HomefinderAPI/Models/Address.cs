using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
    public int? StreetNumber { get; set; }
    public bool Deleted { get; set; } = false;
    
    public ICollection<PropertyObject>? PropertyObjects { get; set; }
  }
}