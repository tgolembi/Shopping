using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.Services.CouponAPI;
using Shopping.Services.CouponAPI.Data;

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

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseAuthorization();

app.MapControllers();

app.Run();
