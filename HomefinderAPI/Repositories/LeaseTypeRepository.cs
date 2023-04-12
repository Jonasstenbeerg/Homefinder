using AutoMapper;
using HomefinderAPI.Data;
using HomefinderAPI.Interfaces;
using HomefinderAPI.Models;
using HomefinderAPI.ViewModels.LeaseType;
using Microsoft.EntityFrameworkCore;

namespace HomefinderAPI.Repositories
{
  public class LeaseTypeRepository : ILeaseTypeRepository
  {
    private readonly IMapper _mapper;
    private readonly HomefinderContext _context;
		public LeaseTypeRepository(HomefinderContext context, IMapper mapper)
		{
      _context = context;
      _mapper = mapper;
			
		}
    public async Task AddLeaseTypeAsync(PostLeaseTypeViewModel model)
    {
			var leaseType = await _context.LeaseTypes
        .Where(l => l.Name!.ToLower() == model.Name!.ToLower())
        .FirstOrDefaultAsync();

			if (leaseType is not null)
			{
				throw new Exception($"En arrendetyp med namnet {model.Name} finns redan");
			}

      var leaseTypeToAdd = _mapper.Map<LeaseType>(model);

			await _context.AddAsync(leaseTypeToAdd);
    }

    public async Task<bool> SaveAllAsync()
    {
      return await _context.SaveChangesAsync() > 0;
    }
  }
}