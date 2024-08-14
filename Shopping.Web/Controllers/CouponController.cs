using Microsoft.AspNetCore.Mvc;
using Shopping.Web.Service.IService;
using Shopping.Web.Models;
using Shopping.Web.Tools;

namespace Shopping.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController (ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO>? list = new();

            ResponseDTO? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.Result != null && response.Success)
            {
                list = JsonHelper.Deserialize<List<CouponDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }
    }
}
