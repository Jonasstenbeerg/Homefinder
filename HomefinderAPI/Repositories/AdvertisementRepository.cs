using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomefinderAPI.Data;
using HomefinderAPI.Interfaces;
using HomefinderAPI.Models;
using HomefinderAPI.ViewModels.Advertisement;
using Microsoft.EntityFrameworkCore;
using System.Collections;
using System.Reflection;

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

        public async Task AddAdvertisementAsync(PostAdvertisementViewModel model)
        {
            //TODO: Refaktorera kod koddupliceringar ner till rad 42
            var leaseType = await _context.LeaseTypes
            .Where(l => l.Name!.ToLower() == model.LeaseType!.ToLower())
            .SingleOrDefaultAsync();

            var propertyType = await _context.PropertyTypes
            .Where(p => p.Name!.ToLower() == model.PropertyType!.ToLower())
            .SingleOrDefaultAsync();

            if (leaseType is null)
            {
                throw new Exception($"Tyvärr vi har inte arrendetypen {model.LeaseType}");
            }
            else if (propertyType is null)
            {
                throw new Exception($"Tyvärr vi har inte objektstypen {model.PropertyType}");
            }
           

            var advertisementToAdd = _mapper.Map<Advertisement>(model);
            advertisementToAdd.Property.LeaseType = leaseType;
            advertisementToAdd.Property.PropertyType = propertyType;

            await _context.Advertisements.AddAsync(advertisementToAdd);
        }

       public async Task<AdvertisementViewModel?> GetAdvertisementByIdAsync(int id)
        {
            return await _context.Advertisements
                .Include(a => a.Property.Address)
                .Include(a => a.Property.LeaseType)
                .Include(a => a.Property.PropertyType)
                .Where(a => a.Id == id)
                .ProjectTo<AdvertisementViewModel>(_mapper.ConfigurationProvider)
                .SingleOrDefaultAsync();
        }


       public async Task<List<AdvertisementViewModel>> ListAllAdvertisementsAsync()
        {
            return await _context.Advertisements
                .Include(a => a.Property.Address)
                .Include(a => a.Property.LeaseType)
                .Include(a => a.Property.PropertyType)
                .ProjectTo<AdvertisementViewModel>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }


        public async Task<bool> SaveAllAsync()
        {
            return await _context.SaveChangesAsync() > 0;
        }


    };
}