using Microsoft.AspNetCore.Mvc;
using Shopping.Web.Service.IService;
using Shopping.Web.Models;
using Shopping.Web.Tools;
using System.Collections.Generic;

namespace Shopping.Web.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController (ICouponService couponService)
        {
            _couponService = couponService;
        }

        public async Task<IActionResult> CouponIndex ()
        {
            List<CouponDTO>? list = new();

            ResponseDTO? response = await _couponService.GetAllCouponsAsync();

            if (response != null && response.Result != null && response.Success)
            {
                list = JsonHelper.Deserialize<List<CouponDTO>>(Convert.ToString(response.Result));
            }

            return View(list);
        }

        public async Task<IActionResult> CouponCreate ()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CouponCreate (CouponDTO coupon)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO? response = await _couponService.CreateCouponAsync(coupon);

                if (response != null && response.Result != null && response.Success)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
            }
            return View(coupon);
        }

        public async Task<IActionResult> CouponDelete (int couponId)
        {
            ResponseDTO? response = await _couponService.GetCouponByIdAsync(couponId);

            if (response != null && response.Result != null && response.Success)
            {
                CouponDTO? coupon = JsonHelper.Deserialize<CouponDTO>(Convert.ToString(response.Result));
                return View(coupon);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> CouponDelete (CouponDTO coupon)
        {
            ResponseDTO? response = await _couponService.DeleteCouponAsync(coupon.CouponId);

            if (response != null && response.Result != null && response.Success)
            {
                return RedirectToAction(nameof(CouponIndex));
            }
            return View(coupon);
        }
    }
}
