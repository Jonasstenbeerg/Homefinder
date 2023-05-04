using AdvertisementMVC.ViewModels;

namespace AdvertisementMVC.Models
{
  public class AdvertisementServiceModel
  {
    private readonly string _baseApiUrl;
    private readonly IConfiguration _config;
    public AdvertisementServiceModel(IConfiguration config)
    {
      _config = config;
      _baseApiUrl = $"{_config.GetValue<string>("baseApiUrl")}/advertisements";
    }

    public async Task<List<AdvertisementViewModel>> ListAllAdvertisementsAsync()
    {
      var url = $"{_baseApiUrl}/list";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        throw new Exception("Det gick inte att hämta annonserna");
      }

      var advertisements = await response.Content.ReadFromJsonAsync<List<AdvertisementViewModel>>();

      return advertisements ?? new List<AdvertisementViewModel>();
    }

    public async Task<AdvertisementViewModel> FindAdvertisement(AdvertisementFilterViewModel advertisementFilterViewModel)
    {
      var baseApiUrl = _config.GetValue<string>("baseApiUrl");
      var url = $"{baseApiUrl}/advertisements/list";

      using var http = new HttpClient();
      var response = await http.GetAsync(url);

      if (!response.IsSuccessStatusCode)
      {
        Console.WriteLine("No advertisement found with that filter.");
      }

      var advertisement = await response.Content.ReadFromJsonAsync<AdvertisementViewModel>();

      return advertisement ?? new AdvertisementViewModel();
    }

  }
}