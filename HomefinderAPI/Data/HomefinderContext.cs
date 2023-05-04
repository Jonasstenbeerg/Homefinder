using HomefinderAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace HomefinderAPI.Data
{
  public class HomefinderContext: IdentityDbContext
  {
    public HomefinderContext(DbContextOptions options) : base(options)
    {
        
    }
    public DbSet<Address> Addresses => Set<Address>();
    public DbSet<Advertisement> Advertisements => Set<Advertisement>();
    public DbSet<LeaseType> LeaseTypes => Set<LeaseType>();
    public DbSet<PropertyObject> PropertyObjects => Set<PropertyObject>();
    public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();
    public DbSet<Image> Images => Set<Image>();
  }
}