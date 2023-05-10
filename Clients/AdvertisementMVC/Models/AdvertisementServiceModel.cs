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
            _baseApiUrl = $"{_config.GetValue<string>("baseApiUrl")}/v1/advertisements";
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

        public async Task<List<AdvertisementViewModel>> ListAllFilteredAdvertisementsAsync(string address, int minPrice, int maxPrice)
        {
            var url = $"{_baseApiUrl}/list";
            
            if (!string.IsNullOrEmpty(address))
            {
                url += $"?Address={address}";
            }
            if (minPrice >= 0 && maxPrice > 0 && minPrice <= maxPrice)
            {
                if (url.Contains("?"))
                {
                    url += $"&MinPrice={minPrice}&MaxPrice={maxPrice}";
                }
                else
                {
                    url += $"?MinPrice={minPrice}&MaxPrice={maxPrice}";
                }
            }

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