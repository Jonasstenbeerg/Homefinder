using HomefinderAPI.ViewModels;

namespace HomefinderAPI.Interfaces
{
    public interface IAdvertisementRepository
    {
        public Task<List<AdvertisementViewModel>> ListAllAdvertisementsAsync();
    }
}