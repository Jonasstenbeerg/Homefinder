using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomefinderAPI.Data;
using HomefinderAPI.Interfaces;

using HomefinderAPI.ViewModels;
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

        public Task<List<AdvertisementViewModel>> ListAllAdvertisementsAsync()
        {
            return _context.Advertisements
            .Include(advertisement => advertisement.Property)
            .ThenInclude(property => property.Address)
            .Include(advertisement => advertisement.Property.LeaseType)
            .Include(advertisement => advertisement.Property.PropertyType)
            .ProjectTo<AdvertisementViewModel>(_mapper.ConfigurationProvider)
            .ToListAsync();
            
            
        }
    }
}