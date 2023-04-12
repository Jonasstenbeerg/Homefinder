using HomefinderAPI.ViewModels.LeaseType;

namespace HomefinderAPI.Interfaces
{
	public interface ILeaseTypeRepository
		{
			public Task AddLeaseTypeAsync(PostLeaseTypeViewModel model);
			public Task<bool> SaveAllAsync();
    
		}
}