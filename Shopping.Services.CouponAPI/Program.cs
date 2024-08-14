using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.Services.CouponAPI;
using Shopping.Services.CouponAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    //var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    //string connectionStringName;

    //if (environment == "Production")
    //{
    //    connectionStringName = "SmarterDB";
    //}
    //else
    //{
    //    connectionStringName = "Localhost702";
    //}
    string connectionString = builder.Configuration.GetConnectionString("Localhost702");
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
