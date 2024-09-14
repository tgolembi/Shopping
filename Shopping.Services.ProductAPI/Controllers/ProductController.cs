using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Shopping.Services.ProductAPI.Data;
using Shopping.Services.ProductAPI.Models.DTO;

namespace Shopping.Services.ProductAPI.Controllers
{
	[Route("api/product")]
	[ApiController]
	public class ProductController : ControllerBase
	{
		private readonly ResponseDTO _response = new();
		private readonly AppDbContext _dbContext;
		private readonly IMapper _mapper;

		public ProductController (AppDbContext dbContext, IMapper mapper)
		{
			_dbContext = dbContext;
			_mapper = mapper;
		}
	}
}
