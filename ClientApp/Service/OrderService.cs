using NiceToes.Web.Models;
using NiceToes.Web.Service.IService;
using NiceToes.Web.Utility;

namespace NiceToes.Web.Service
{
    public class OrderService : IOrderService
    {
        private readonly IBaseService _baseService;
        public OrderService(IBaseService baseService)
        {
            _baseService = baseService;
        }

       

        public async Task<ResponseDto?> CreateOrder(CartDto cartDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = cartDto,
                Url = Sd.OrderAPIBase + "/api/order/CreateOrder"
            });
        }

        public async Task<ResponseDto?> CreateStripeSession(StripeRequestDto stripeRequestDto)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = stripeRequestDto,
                Url = Sd.OrderAPIBase + "/api/order/CreateStripeSession"
            });
        }

        public async Task<ResponseDto?> GetAllOrder(string? userId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.GET,
                Url = Sd.OrderAPIBase + "/api/order/GetOrders/" + userId
            });
        }

        public async Task<ResponseDto?> GetOrder(int orderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.GET,
                Url = Sd.OrderAPIBase + "/api/order/GetOrder/" + orderId
            });
        }

        public async Task<ResponseDto?> UpdateOrderStatus(int orderId, string newStatus)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = newStatus,
                Url = Sd.OrderAPIBase + "/api/order/UpdateOrderStatus/"+orderId
            });
        }

        public async Task<ResponseDto?> ValidateStripeSession(int orderHeaderId)
        {
            return await _baseService.SendAsync(new RequestDto()
            {
                ApiType = Sd.ApiType.POST,
                Data = orderHeaderId,
                Url = Sd.OrderAPIBase + "/api/order/ValidateStripeSession"
            });
        }
    }
}
