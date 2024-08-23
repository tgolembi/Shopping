using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shopping.Services.AuthAPI.Data;
using Shopping.Services.AuthAPI.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
	//string? connectionString = builder.Configuration.GetConnectionString("Localhost702");
	string? connectionString = builder.Configuration.GetConnectionString("LocalHostIPSTong");
	//string? connectionString = builder.Configuration.GetConnectionString("SmarterDB");
	options.UseSqlServer(connectionString);
});

builder.Services
	   .AddIdentity<ApplicationUser,IdentityRole>()
	   .AddEntityFrameworkStores<AppDbContext>()
	   .AddDefaultTokenProviders();

builder.Services.AddControllers();
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
