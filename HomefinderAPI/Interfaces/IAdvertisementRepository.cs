using HomefinderAPI.Models;

namespace API.Interfaces
{
    public interface IAdvertisementRepository
    {
        public Task<List<Advertisement>> ListAllAdvertisementsAsync();
    }
}