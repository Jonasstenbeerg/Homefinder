using HomefinderAPI.ViewModels.Advertisement;
using HomefinderAPI.ViewModels.Queries;

namespace HomefinderAPI.Interfaces
{
  public interface IAdvertisementRepository
  {
    public Task<List<AdvertisementViewModel>> ListAllAvailableAdvertisementsAsync(PaginitationQuery? pageQuery = null, AdvertisementQuery? addQuery = null);
    public Task<List<AdvertisementViewModel>> ListAllAdvertisementsAsync();
    public Task<AdvertisementViewModel?> GetAdvertisementByIdAsync(int id);
    public Task AddAdvertisementAsync(PostAdvertisementViewModel model);
    public Task UpdateAdvertisementAsync(int id, PostAdvertisementViewModel model);
    public Task DeleteAdvertisementAsync(int id);
    public Task<bool> SaveAllAsync();
  }
}