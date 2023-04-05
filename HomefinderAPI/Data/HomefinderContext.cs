using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HomefinderAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace HomefinderAPI.Data
{
    public class HomefinderContext: DbContext
    {
        public HomefinderContext(DbContextOptions options) : base(options)
        {
            
        }
        public DbSet<Address> Addresses => Set<Address>();
        public DbSet<Advertisement> Advertisements => Set<Advertisement>();
        public DbSet<LeaseType> LeaseTypes => Set<LeaseType>();
        public DbSet<Property> Properties => Set<Property>();
        public DbSet<PropertyType> PropertyTypes => Set<PropertyType>();    
    }
}