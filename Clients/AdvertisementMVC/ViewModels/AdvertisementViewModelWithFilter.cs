namespace AdvertisementMVC.ViewModels
{
  public class AdvertisementViewModelWithFilter
  {
    public IEnumerable<AdvertisementViewModel> Advertisements { get; set; }
    public AdvertisementFilterViewModel Filter { get; set; }
  }
}