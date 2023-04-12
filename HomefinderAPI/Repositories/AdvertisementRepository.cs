using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomefinderAPI.Data;
using HomefinderAPI.Interfaces;
using HomefinderAPI.Models;
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

    public async Task AddAdvertisementAsync(PostAdvertisementViewModel model)
    {
        //TODO: Refaktorisera koddupliceringar ner till rad 42
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
        var address = await SearchAddressAsync(advertisementToAdd.Property.Address!);
        
        if(address != null)
        {
            advertisementToAdd.Property.AddressId = address.Id;
        }

        advertisementToAdd.Property.LeaseType = leaseType;
        advertisementToAdd.Property.PropertyType = propertyType;

        await _context.Advertisements.AddAsync(advertisementToAdd);
    }

    public async Task<AdvertisementViewModel?> GetAdvertisementByIdAsync(int id)
    {
      //TODO: uppdatering av adress ska skapa ett nytt objekt i databasen 
      //om fler bostäder finns på samma adress
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

    public async Task UpdateAdvertisementAsync(int id, PostAdvertisementViewModel model)
    {
      var advertisement = await _context.Advertisements
      .Include(a => a.Property.Address)
      .Include(a => a.Property.LeaseType)
      .Include(a => a.Property.PropertyType)
      .SingleOrDefaultAsync(a => a.Id == id);

      if (advertisement is null)
      {
        throw new Exception($"Vi kunde inte hitta någon annons med id {id}");
      }

      _mapper.Map<PostAdvertisementViewModel, Advertisement>(model, advertisement);

      _context.Advertisements.Update(advertisement);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    private async Task<Address?> SearchAddressAsync(Address address)
    {
      return await _context.Addresses
        .Where(a => 
        a.StreetName!.ToLower()==address.StreetName!.ToLower() && 
        a.StreetNumber==address.StreetNumber && 
        a.PostalCode==address.PostalCode)
        .FirstOrDefaultAsync();
    }

    public async Task DeleteAdvertisementAsync(int id)
    {
      var advertisement = await _context.Advertisements.FindAsync(id);

      if (advertisement is null)
      {
        throw new Exception($"Vi kunde inte hitta någon annons med id {id}");
      }

      advertisement.Deleted = true;

      _context.Advertisements.Update(advertisement);
    }
  }
}