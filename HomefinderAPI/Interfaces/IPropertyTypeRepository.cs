using HomefinderAPI.ViewModels.PropertyType;

namespace HomefinderAPI.Interfaces
{
	public interface IPropertyTypeRepository
		{
			public Task AddPropertyTypeAsync(PostPropertyTypeViewModel model);
			public Task<bool> SaveAllAsync();
		}
}