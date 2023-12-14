using NiceToes.ShoppingCartAPI.Models.Dto;

namespace NiceToes.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
