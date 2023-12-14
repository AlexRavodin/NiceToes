using NiceToes.Web.Models;
using NiceToes.Web.Service.IService;
using NiceToes.Web.Utility;

namespace NiceToes.Web.Service
{
    public class CartService : ICartService
    {
        private readonly IBaseService _baseService;
        public CartService(IBaseService baseService)
        {
            _baseService = baseService;
        }

        public async Task<ResponseDto?> ApplyCouponAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = cartDto,
                Url = Sd.ShoppingCartAPIBase + "/api/cart/ApplyCoupon"
            });
        }

        public async Task<ResponseDto?> EmailCart(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = cartDto,
                Url = Sd.ShoppingCartAPIBase + "/api/cart/EmailCartRequest"
            });
        }

        public async Task<ResponseDto?> GetCartByUserIdAsnyc(string userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.GET,
                Url = Sd.ShoppingCartAPIBase + "/api/cart/GetCart/"+ userId
            });
        }

        
        public async Task<ResponseDto?> RemoveFromCartAsync(int cartDetailsId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = cartDetailsId,
                Url = Sd.ShoppingCartAPIBase + "/api/cart/RemoveCart"
            });
        }

      
        public async Task<ResponseDto?> UpsertCartAsync(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = cartDto,
                Url = Sd.ShoppingCartAPIBase + "/api/cart/CartUpsert"
            });
        }
    }
}
