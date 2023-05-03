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
	}
}