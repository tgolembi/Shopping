﻿
namespace Shopping.Web.Models
{
    public class CouponDTO
    {
        public int CouponId { get; set; }
        public required string CouponCode { get; set; }
        public double DiscountAmount { get; set; }
        public int MinAmount { get; set; }
    }
}
