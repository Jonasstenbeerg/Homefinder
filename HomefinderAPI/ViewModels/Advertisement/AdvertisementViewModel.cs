namespace HomefinderAPI.ViewModels.Advertisement
{
  public class AdvertisementViewModel
  {   
    public int Id { get; set; }
    public int ListPrice { get; set; }
    public string? LeaseType { get; set; }
    public string? PropertyType { get; set; }
    public int Area { get; set; }
    public string? City { get; set; }
    public string? PostalCode { get; set; }
    public string? StreetName { get; set; }
    public int StreetNumber { get; set; }
    public string? ImageBin { get; set; }
    public bool Deleted { get; set; }
  }
}