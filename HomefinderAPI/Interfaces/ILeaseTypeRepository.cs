using HomefinderAPI.ViewModels.LeaseType;

namespace HomefinderAPI.Interfaces
{
	public interface ILeaseTypeRepository
		{
			public Task AddLeaseTypeAsync(PostLeaseTypeViewModel model);
			public Task UpdateLeaseTypeAsync(int id, PostLeaseTypeViewModel model);
			public Task<List<LeaseTypeViewModel>> ListAllLeaseTypesAsync();
			public Task<bool> SaveAllAsync();
    
		}
}