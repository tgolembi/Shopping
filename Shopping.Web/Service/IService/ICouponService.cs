using Shopping.Web.Models;

namespace Shopping.Web.Service.IService
{
    public interface ICouponService
    {
        Task<ResponseDTO?> GetCouponAsync (string couponCode);
        Task<ResponseDTO?> GetAllCouponsAsync ();
        Task<ResponseDTO?> GetCouponByIdAsync (int couponId);
        Task<ResponseDTO?> CreateCouponAsync (CouponDTO coupon);
        Task<ResponseDTO?> UpdateCouponAsync (CouponDTO coupon);
        Task<ResponseDTO?> DeleteCouponAsync (int couponId);
    }
}
