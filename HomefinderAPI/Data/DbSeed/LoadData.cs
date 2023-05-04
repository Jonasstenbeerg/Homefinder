using System.Text.Json;
using HomefinderAPI.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace HomefinderAPI.Data.DbSeed
{
	public class LoadData
	{
		public static async Task LoadAddressesAsync(HomefinderContext context)
    {
      if (await context.Addresses.AnyAsync()) return;

      var addressData = await File.ReadAllTextAsync("Data/DbSeed/Tables/Addresses.json");
      var addresses = JsonSerializer.Deserialize<List<Address>>(addressData);

      await context.AddRangeAsync(addresses!);
      await context.SaveChangesAsync();

    }

		public static async Task LoadAdvertisementsAsync(HomefinderContext context)
    {
      if (await context.Advertisements.AnyAsync()) return;

      var addData = await File.ReadAllTextAsync("Data/DbSeed/Tables/Advertisements.json");
      var adds = JsonSerializer.Deserialize<List<Advertisement>>(addData);

      await context.AddRangeAsync(adds!);
      await context.SaveChangesAsync();

    }

		public static async Task LoadAspNetUserClaimsAsync(HomefinderContext context)
    {
      if (await context.UserClaims.AnyAsync()) return;

      var claimData = await File.ReadAllTextAsync("Data/DbSeed/Tables/AspNetUserClaims.json");
      var claims = JsonSerializer.Deserialize<List<IdentityUserClaim<string>>>(claimData);

      await context.AddRangeAsync(claims!);
      await context.SaveChangesAsync();

    }

		public static async Task LoadAspNetUsersAsync(HomefinderContext context)
    {
      if (await context.Users.AnyAsync()) return;

      var usersData = await File.ReadAllTextAsync("Data/DbSeed/Tables/AspNetUsers.json");
      var users = JsonSerializer.Deserialize<List<IdentityUser>>(usersData);

      await context.AddRangeAsync(users!);
      await context.SaveChangesAsync();

    }

		public static async Task LoadImagesAsync(HomefinderContext context)
    {
      if (await context.Images.AnyAsync()) return;

      var imagesData = await File.ReadAllTextAsync("Data/DbSeed/Tables/Images.json");
      var images = JsonSerializer.Deserialize<List<Image>>(imagesData);

      await context.AddRangeAsync(images!);
      await context.SaveChangesAsync();

    }

		public static async Task LoadLeaseTypesAsync(HomefinderContext context)
    {
      if (await context.LeaseTypes.AnyAsync()) return;

      var leaseTypesData = await File.ReadAllTextAsync("Data/DbSeed/Tables/LeaseTypes.json");
      var leaseTypes = JsonSerializer.Deserialize<List<LeaseType>>(leaseTypesData);

      await context.AddRangeAsync(leaseTypes!);
      await context.SaveChangesAsync();

    }

		public static async Task LoadPropertyObjectsAsync(HomefinderContext context)
    {
      if (await context.PropertyObjects.AnyAsync()) return;

      var propertyObjectsData = await File.ReadAllTextAsync("Data/DbSeed/Tables/PropertyObjects.json");
      var propertyObjects = JsonSerializer.Deserialize<List<PropertyObject>>(propertyObjectsData);

      await context.AddRangeAsync(propertyObjects!);
      await context.SaveChangesAsync();

    }

		public static async Task LoadPropertyTypesAsync(HomefinderContext context)
    {
      if (await context.PropertyTypes.AnyAsync()) return;

      var propertyTypesData = await File.ReadAllTextAsync("Data/DbSeed/Tables/PropertyTypes.json");
      var propertyTypes = JsonSerializer.Deserialize<List<PropertyType>>(propertyTypesData);

      await context.AddRangeAsync(propertyTypes!);
      await context.SaveChangesAsync();

    }
	}
}