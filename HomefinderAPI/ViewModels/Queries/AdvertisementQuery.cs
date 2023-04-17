namespace HomefinderAPI.ViewModels.Queries
{
  public class AdvertisementQuery
	{
		public string? Address { get; set; }
		public int MinPrice { get; set; }
		public int MaxPrice { get; set; }
	}
}