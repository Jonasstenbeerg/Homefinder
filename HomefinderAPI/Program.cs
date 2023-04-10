using HomefinderAPI.Data;
using HomefinderAPI.Helpers;
using HomefinderAPI.Interfaces;
using HomefinderAPI.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Context that uses SQLite
builder.Services.AddDbContext<HomefinderContext>(options => {
	options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});

//Configure Microsoft Identity for users
builder.Services.AddIdentity<IdentityUser, IdentityRole>(
	options =>
		{
			options.Password.RequireDigit = true;
			options.Password.RequireLowercase = true;
			options.Password.RequireUppercase = true;
			options.Password.RequireNonAlphanumeric = false;
			options.Password.RequiredLength = 8;

			options.User.RequireUniqueEmail = true;

			options.Lockout.MaxFailedAccessAttempts = 5;
			options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
		}
).AddEntityFrameworkStores<HomefinderContext>();

//Repo service for DI
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
		app.UseSwagger();
		app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
