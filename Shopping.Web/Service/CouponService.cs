using Shopping.Web.Models;
using Shopping.Web.Service.IService;
using Shopping.Web.Enumerators;

namespace Shopping.Web.Service
{
    public class CouponService : ICouponService
    {
        private readonly IBaseService _baseService;

        public CouponService (IBaseService baseService)
        {
            _baseService = baseService;
        }
        

        public async Task<ResponseDTO?> CreateCouponAsync (CouponDTO coupon)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiMethod = StaticDetails.ApiMethod.POST,
                Url = $"{StaticDetails.CouponAPIBase}/coupon/",
                Data = coupon
            });
        }
        public async Task<ResponseDTO?> UpdateCouponAsync (CouponDTO coupon)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiMethod = StaticDetails.ApiMethod.PUT,
                Url = $"{StaticDetails.CouponAPIBase}/coupon/",
                Data = coupon
            });
        }

        public async Task<ResponseDTO?> DeleteCouponAsync (int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiMethod = StaticDetails.ApiMethod.DELETE,
                Url = $"{StaticDetails.CouponAPIBase}/coupon/{couponId}"
            });
        }

        public async Task<ResponseDTO?> GetAllCouponsAsync ()
        {
			var coupons = await _baseService.SendAsync(new RequestDTO()
            {
                ApiMethod = StaticDetails.ApiMethod.GET,
                Url = $"{StaticDetails.CouponAPIBase}/coupon"
            });
            return coupons;
        }

        public async Task<ResponseDTO?> GetCouponAsync (string couponCode)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiMethod = StaticDetails.ApiMethod.GET,
                Url = $"{StaticDetails.CouponAPIBase}/coupon/GetByCode/{couponCode}"
            });
        }

        public async Task<ResponseDTO?> GetCouponByIdAsync (int couponId)
        {
            return await _baseService.SendAsync(new RequestDTO()
            {
                ApiMethod = StaticDetails.ApiMethod.GET,
                Url = $"{StaticDetails.CouponAPIBase}/coupon/{couponId}"
            });
        }

    }
}
