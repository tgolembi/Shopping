using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Shopping.Services.CouponAPI;
using Shopping.Services.CouponAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    //string? connectionString = builder.Configuration.GetConnectionString("Localhost702");
    string? connectionString = builder.Configuration.GetConnectionString("LocalHostIPSTong");
    //string? connectionString = builder.Configuration.GetConnectionString("SmarterDB");
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
