using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomefinderAPI.Data;
using HomefinderAPI.Interfaces;
using HomefinderAPI.ViewModels.Advertisement;
using Microsoft.EntityFrameworkCore;

namespace HomefinderAPI.Repositories
{
    public class AdvertisementRepository : IAdvertisementRepository
    {
        private readonly HomefinderContext _context;
        private readonly IMapper _mapper;
        public AdvertisementRepository(HomefinderContext context, IMapper mapper)
        {
            _mapper = mapper;
            _context = context;
            
        }

        public Task AddAdvertisementAsync(PostAdvertisementViewModel model)
        {
            throw new NotImplementedException();
        }

        public async Task<AdvertisementViewModel> GetAdvertisementByIdAsync(int id)
        {
            var advertisement = await _context.Advertisements
            .Include(advertisement => advertisement.Property)
            .Include(advertisement => advertisement.Property.Address)
            .Include(advertisement => advertisement.Property.LeaseType)
            .Include(advertisement => advertisement.Property.PropertyType)
            .FirstOrDefaultAsync(advertisement => advertisement.Id == id);

            if(advertisement is null)
            {
                throw new Exception($"Vi kunde inte hitta någon annons med id {id}");
            }

            var advertisementToReturn = _mapper.Map<AdvertisementViewModel>(advertisement);

            return advertisementToReturn;
        }

        public async Task<List<AdvertisementViewModel>> ListAllAdvertisementsAsync()
        {
            return await _context.Advertisements
            .Include(advertisement => advertisement.Property)
            .ThenInclude(property => property.Address)
            .Include(advertisement => advertisement.Property.LeaseType)
            .Include(advertisement => advertisement.Property.PropertyType)
            .ProjectTo<AdvertisementViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
            
            
        }

        public Task<bool> SaveAllAsync()
        {
            throw new NotImplementedException();
        }
    }
}