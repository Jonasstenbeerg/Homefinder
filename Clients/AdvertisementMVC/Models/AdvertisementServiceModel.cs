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

        public async Task<AdvertisementViewModel> GetAdvertisementAsync(int id)
        {
            var url = $"{_baseApiUrl}/{id}";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Det gick inte att hämta annonsen");
            }

            var advertisement = await response.Content.ReadFromJsonAsync<AdvertisementViewModel>();

            return advertisement ?? new AdvertisementViewModel();
        }

        public async Task<PaginationViewModel> ListAllAdvertisementsAsync(int pageNumber = 1, int pageSize = 5)
        {
            var url = $"{_baseApiUrl}/list?pageNumber={pageNumber}&pageSize={pageSize}";

            using var http = new HttpClient();
            var response = await http.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Det gick inte att hämta annonserna");
            }

            var data = await response.Content.ReadFromJsonAsync<PaginationViewModel>();

            return data ?? new PaginationViewModel();
        }

        public async Task<PaginationViewModel> ListAllFilteredAdvertisementsAsync(string address, int minPrice, int maxPrice)
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

            var advertisements = await response.Content.ReadFromJsonAsync<PaginationViewModel>();

            return advertisements ?? new PaginationViewModel();
        }
    }
}