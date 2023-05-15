namespace AdvertisementMVC.ViewModels
{
  public class PaginationViewModel
  {
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
    public string? NextPage { get; set; }
    public string? PreviousPage { get; set; }
    public List<AdvertisementViewModel> Data { get; set; } = new List<AdvertisementViewModel>();
  }
}