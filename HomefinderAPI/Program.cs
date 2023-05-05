using System.Reflection;
using System.Text;
using HomefinderAPI.Data;
using HomefinderAPI.Data.DbSeed;
using HomefinderAPI.Helpers;
using HomefinderAPI.Interfaces;
using HomefinderAPI.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// Context that uses SQLite
builder.Services.AddDbContext<HomefinderContext>(options => 
{
	options.UseSqlite(builder.Configuration.GetConnectionString("SQLite"));
});

//Configure Microsoft Identity for users
builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
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

//Configure Jason Web Token authentication
builder.Services.AddAuthentication(options => 
{
	options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
	options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options => 
{
	options.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(
				Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("apiSecret")!)
			),
		ValidateLifetime = true,
		ValidateAudience = false,
		ValidateIssuer = false,
		ClockSkew = TimeSpan.Zero
	};
});

//Repo service for DI
builder.Services.AddScoped<IAdvertisementRepository, AdvertisementRepository>();
builder.Services.AddScoped<ILeaseTypeRepository, LeaseTypeRepository>();
builder.Services.AddScoped<IPropertyTypeRepository, PropertyTypeRepository>();

// AutoMapper
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles).Assembly);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// Configurer swagger to enable jwt bearer auth
builder.Services.AddSwaggerGen(options =>
{
	options.SwaggerDoc("v1", new OpenApiInfo{ Title = "Homefinder API", Version = "v1"});
	options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer",
		BearerFormat = "JWT",
		In = ParameterLocation.Header,
		Description = "Enter JWT token with bearer format like: Bearer token"
	});

	options.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference = new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id="Bearer"
				}
			},
			new string[]{}
		}
	});

	// Configure swagger to include summary on controller methods in UI
	var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
	var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
	options.IncludeXmlComments(xmlPath);
});

//Configure allowed connecting origins
builder.Services.AddCors(options =>
{
  options.AddPolicy("Homefinder",
    policy =>
    {
      policy.AllowAnyHeader();
      policy.AllowAnyMethod();
      policy.WithOrigins(
        "http://localhost:3000");
    }
  );
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
		app.UseSwagger();
		app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("Homefinder");

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

using var scope = app.Services.CreateScope();
var services = scope.ServiceProvider;
try
{
	var context = services.GetRequiredService<HomefinderContext>();
	await context.Database.MigrateAsync();
	await LoadData.LoadAddressesAsync(context);
	await LoadData.LoadPropertyTypesAsync(context);
	await LoadData.LoadLeaseTypesAsync(context);
	await LoadData.LoadPropertyObjectsAsync(context);
	await LoadData.LoadImagesAsync(context);
	await LoadData.LoadAspNetUsersAsync(context);
	await LoadData.LoadAspNetUserClaimsAsync(context);
  await LoadData.LoadAdvertisementsAsync(context);
	
}
catch(Exception ex)
{
	var logger = services.GetRequiredService<ILogger<Program>>();
  logger.LogError(ex, "Ett fel inträffade när migrering utfördes");
}

app.Run();
