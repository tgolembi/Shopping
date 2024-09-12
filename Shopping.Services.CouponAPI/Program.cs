using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Shopping.Services.CouponAPI;
using Shopping.Services.CouponAPI.Data;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<AppDbContext>(options =>
{
    string connectionStringName = string.Empty;

	switch (Environment.MachineName)
	{
		case "ASUS702": connectionStringName = "Localhost702"; break;
		case "BR-NOT-DEV-17": connectionStringName = "LocalHostIPSTong"; break;
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
builder.Services.AddSwaggerGen(option =>
{
	option.AddSecurityDefinition(name: "Bearer", securityScheme: new OpenApiSecurityScheme
	{
		Name = "Authorization",
		Description = "Enter the Bearer Authorization string as the following: `Bearer Generated-Jwt-Token`",
		In = ParameterLocation.Header,
		Type = SecuritySchemeType.ApiKey,
		Scheme = "Bearer"
	});
	option.AddSecurityRequirement(new OpenApiSecurityRequirement
	{
		{
			new OpenApiSecurityScheme
			{
				Reference= new OpenApiReference
				{
					Type=ReferenceType.SecurityScheme,
					Id=JwtBearerDefaults.AuthenticationScheme
				}
			}, new string[]{}
		}
	});
});

var settings = builder.Configuration.GetSection("ApiSettings");
var secret = settings.GetValue<string>("Secret");
var issuer = settings.GetValue<string>("Issuer");
var audience = settings.GetValue<string>("Audience");

var key = Encoding.ASCII.GetBytes(secret);

builder.Services.AddAuthentication(a => 
{
	a.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
	a.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(j => 
{
	j.TokenValidationParameters = new TokenValidationParameters
	{
		ValidateIssuerSigningKey = true,
		IssuerSigningKey = new SymmetricSecurityKey(key),
		ValidateIssuer = true,
		ValidIssuer = issuer,
		ValidAudience = audience,
		ValidateAudience = true
	};
});

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
