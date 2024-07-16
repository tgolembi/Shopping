using Shopping.Web.Models;
using Shopping.Web.Service.IService;

namespace Shopping.Web.Service
{
    public class CouponService : ICouponService
    {
        Task<ResponseDTO?> ICouponService.CreateCouponAsync (CouponDTO coupon)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDTO?> ICouponService.DeleteCouponAsync (int couponId)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDTO?> ICouponService.GetAllCouponsAsync ()
        {
            throw new NotImplementedException();
        }

        Task<ResponseDTO?> ICouponService.GetCouponAsync (string couponCode)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDTO?> ICouponService.GetCouponByIdAsync (int couponId)
        {
            throw new NotImplementedException();
        }

        Task<ResponseDTO?> ICouponService.UpdateCouponAsync (CouponDTO coupon)
        {
            throw new NotImplementedException();
        }
    }
}
