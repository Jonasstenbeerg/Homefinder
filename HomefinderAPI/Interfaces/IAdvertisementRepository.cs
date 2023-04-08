using HomefinderAPI.ViewModels.Advertisement;

namespace HomefinderAPI.Interfaces
{
  public interface IAdvertisementRepository
  {
    public Task<List<AdvertisementViewModel>> ListAllAdvertisementsAsync();
    public Task<AdvertisementViewModel?> GetAdvertisementByIdAsync(int id);
    public Task AddAdvertisementAsync(PostAdvertisementViewModel model);
    public Task<bool> SaveAllAsync();
  }
}