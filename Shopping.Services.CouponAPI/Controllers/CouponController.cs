using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Shopping.Services.CouponAPI.Data;
using Shopping.Services.CouponAPI.Models;
using Shopping.Services.CouponAPI.Models.DTO;
using Shopping.Services.CouponAPI.Enumerators;

namespace Shopping.Services.CouponAPI.Controllers
{
    [Route("api/coupon")]
    [ApiController]
    [Authorize]
    public class CouponController : ControllerBase
    {
        private ResponseDTO _response = new();
        private readonly AppDbContext _dbContext;
        private readonly IMapper _mapper;

        public CouponController (AppDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public ResponseDTO Get ()
        {
            try
            {
                IEnumerable<Coupon> objList = _dbContext.Coupons.ToList();
                _response.Result = _mapper.Map<IEnumerable<CouponDTO>>(objList);
                _response.Success = true;
            }
            catch (Exception ex)
            {
                _response.Message = ex.GetType().ToString() + " ::: " + ex.Message; //TODO: Change message
            }

            return _response;
        }

        [HttpGet("{id:int}")]
        public ResponseDTO Get (int id)
        {
            try
            {
                Coupon? obj = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id);
                
                if (obj == null)
                {
                    _response.Message = $"Coupon [{id}] not found";
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDTO>(obj);
                    _response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.GetType().ToString() + " ::: " + ex.Message; //TODO: Change message
            }

            return _response;
        }

        [HttpGet("GetByCode/{code}")]
        public ResponseDTO GetByCode (string code)
        {
            try
            {
                Coupon? obj = _dbContext.Coupons.FirstOrDefault(c => c.CouponCode.ToLower() == code.ToLower());
                
                if (obj == null)
                {
                    _response.Message = $"Coupon '{code}' not found";
                }
                else
                {
                    _response.Result = _mapper.Map<CouponDTO>(obj);
                    _response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.GetType().ToString() + " ::: " + ex.Message;
            }

            return _response;
        }

        [HttpPost]
        [Authorize(Roles = nameof(Role.ADMIN))]
        public ResponseDTO Post ([FromBody]CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);

                _dbContext.Coupons.Add(obj);
                _dbContext.SaveChanges();

                _response.Result = _mapper.Map<CouponDTO>(obj);
                _response.Success = true;
            }
            catch(Exception ex)
            {
                _response.Message = ex.GetType().ToString() + " ::: " + ex.Message;
            }
            
            return _response;
        }

        [HttpPut]
		[Authorize(Roles = nameof(Role.ADMIN))]
		public ResponseDTO Put ([FromBody]CouponDTO couponDTO)
        {
            try
            {
                Coupon obj = _mapper.Map<Coupon>(couponDTO);

                //TODO: verificar se existe no DB?

                if (obj == null)
                {
                    _response.Result = couponDTO;
                    _response.Message = $"Coupon [{couponDTO.CouponId}] not found";
                }
                else
                {
                    _dbContext.Coupons.Update(obj);
                    _dbContext.SaveChanges();

                    _response.Result = _mapper.Map<CouponDTO>(obj);
                    _response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.GetType().ToString() + " ::: " + ex.Message;
            }

            return _response;
        }

        [HttpDelete("{id:int}")]
		[Authorize(Roles = nameof(Role.ADMIN))]
		public ResponseDTO Delete (int id)
        {
            try
            {
                Coupon? obj = _dbContext.Coupons.FirstOrDefault(c => c.CouponId == id);
                //Ou Any()?

                if (obj == null)
                {
                    _response.Message = $"Coupon [{id}] not found";
                }
                else
                {
                    _dbContext.Coupons.Remove(obj);
                    _dbContext.SaveChanges();

                    _response.Result = _mapper.Map<CouponDTO>(obj);
                    _response.Success = true;
                }
            }
            catch (Exception ex)
            {
                _response.Message = ex.GetType().ToString() + " ::: " + ex.Message;
            }

            return _response;
        }
    }
}
