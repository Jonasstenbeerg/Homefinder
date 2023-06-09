using AutoMapper;
using AutoMapper.QueryableExtensions;
using HomefinderAPI.Data;
using HomefinderAPI.Interfaces;
using HomefinderAPI.Models;
using HomefinderAPI.ViewModels.PropertyType;
using Microsoft.EntityFrameworkCore;

namespace HomefinderAPI.Repositories
{
	public class PropertyTypeRepository : IPropertyTypeRepository
	{
		private readonly IMapper _mapper;
		private readonly HomefinderContext _context;
		public PropertyTypeRepository(HomefinderContext context,IMapper mapper)
		{
			_context = context;
			_mapper = mapper;
		}

    public async Task AddPropertyTypeAsync(PostPropertyTypeViewModel model)
    {
      var propertyType = await _context.PropertyTypes
			.Where(p => p.Name!.ToLower() == model.Name!.ToLower())
			.FirstOrDefaultAsync();

			if (propertyType is not null)
			{
				throw new Exception($"En objekttyp med namnet {model.Name} finns redan");
			}

			var propertyTypeToAdd = _mapper.Map<PropertyType>(model);

			await _context.AddAsync(propertyTypeToAdd); 
    }

    public async Task<List<PropertyTypeViewModel>> ListAllPropertyTypesAsync()
    {
      return await _context.PropertyTypes
        .ProjectTo<PropertyTypeViewModel>(_mapper.ConfigurationProvider)
        .ToListAsync();
    }

    public async Task UpdatePropertyTypeAsync(int id, PostPropertyTypeViewModel model)
    {
      var propertyType = await _context.PropertyTypes.FindAsync(id);

      if (propertyType is null)
      {
        throw new Exception($"Vi kunde inte hitta någon objekttyp med id {id}");
      }

      _mapper.Map<PostPropertyTypeViewModel, PropertyType>(model, propertyType);

      _context.PropertyTypes.Update(propertyType);
		}
    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }

    public async Task<PropertyTypeViewModel?> GetPropertyTypeByIdAsync(int id)
    {
      return await _context.PropertyTypes
        .Where(p => p.Id == id)
        .ProjectTo<PropertyTypeViewModel>(_mapper.ConfigurationProvider)
        .SingleOrDefaultAsync();
    }
  }
}