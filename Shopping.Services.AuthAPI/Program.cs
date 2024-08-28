using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Services.AuthAPI.Data;
using Shopping.Services.AuthAPI.Models;
using Shopping.Services.AuthAPI.Service;
using Shopping.Services.AuthAPI.Service.IService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
	string connectionStringName = string.Empty;

	switch (Environment.MachineName)
	{
		case "ASUS702": connectionStringName = "Localhost702"; break;
		case "br-not-dev-17": connectionStringName = "LocalHostIPSTong"; break;
		default: throw new Exception("This machine has no database connection string associated");
	}

	string? connectionString = builder.Configuration.GetConnectionString(connectionStringName);

	options.UseSqlServer(connectionString);
});

builder.Services.Configure<JwtOptions>(builder.Configuration.GetSection("ApiSettings:JwtOptions"));

builder.Services
	   .AddIdentity<ApplicationUser,IdentityRole>()
	   .AddEntityFrameworkStores<AppDbContext>()
	   .AddDefaultTokenProviders();

builder.Services.AddControllers();

builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
