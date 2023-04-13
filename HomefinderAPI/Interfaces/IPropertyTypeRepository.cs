using HomefinderAPI.ViewModels.PropertyType;

namespace HomefinderAPI.Interfaces
{
	public interface IPropertyTypeRepository
		{
			public Task AddPropertyTypeAsync(PostPropertyTypeViewModel model);
			public Task<List<PropertyTypeViewModel>> ListAllPropertyTypesAsync();

			public Task<bool> SaveAllAsync();
		}
}