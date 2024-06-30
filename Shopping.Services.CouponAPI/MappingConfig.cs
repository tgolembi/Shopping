using AutoMapper;
using Shopping.Services.CouponAPI.Models;
using Shopping.Services.CouponAPI.Models.DTO;

namespace Shopping.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps ()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDTO, Coupon>();
                config.CreateMap<Coupon, CouponDTO>();
            });
            return mappingConfig;
        }
    }
}
