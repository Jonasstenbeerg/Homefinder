using System.ComponentModel.DataAnnotations;

namespace HomefinderAPI.ViewModels.Advertisement
{
	public class PostAdvertisementViewModel
	{
		[Required(ErrorMessage = "Pris är obligatoriskt")]
		public int ListPrice { get; set; }
		[Required(ErrorMessage = "Arrendetyp är obligatoriskt")]
		public string? LeaseType { get; set; }
		[Required(ErrorMessage = "Objektstyp är obligatoriskt")]
		public string? PropertyType { get; set; }
		[Required(ErrorMessage = "Area är obligatoriskt")]
		public int Area { get; set; }
		[Required(ErrorMessage = "Stad är obligatoriskt")]
		public string? City { get; set; }
		[Required(ErrorMessage = "Postnummer är obligatoriskt")]
		public int PostalCode { get; set; }
		[Required(ErrorMessage = "Gatunamn är obligatoriskt")]
		public string? StreetName { get; set; }
		[Required(ErrorMessage = "Gatunummer är obligatoriskt")]
		public int StreetNumber { get; set; }
		[Required(ErrorMessage = "Binär kod för bild är obligatoriskt")]
		public string? ImageBin { get; set; }
	}
}