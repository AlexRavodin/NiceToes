using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using NiceToes.Web.Models;
using NiceToes.Web.Service.IService;
using NiceToes.Web.Utility;

namespace NiceToes.Web.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        public IActionResult OrderIndex()
        {
            return View();
        }


		public async Task<IActionResult> OrderDetail(int orderId)
		{
			OrderHeaderDto orderHeaderDto = new OrderHeaderDto();
            string userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;

            var response = await _orderService.GetOrder(orderId);
			if (response != null && response.IsSuccess)
			{
				orderHeaderDto = JsonConvert.DeserializeObject<OrderHeaderDto>(Convert.ToString(response.Result));
			}
			if(!User.IsInRole(Sd.RoleAdmin) && userId!= orderHeaderDto.UserId)
            {
                return NotFound();
            }
            return View(orderHeaderDto);
		}

        [HttpPost("OrderReadyForPickup")]
        public async Task<IActionResult> OrderReadyForPickup(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId,Sd.Status_ReadyForPickup);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        [HttpPost("CompleteOrder")]
        public async Task<IActionResult> CompleteOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, Sd.Status_Completed);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }

        [HttpPost("CancelOrder")]
        public async Task<IActionResult> CancelOrder(int orderId)
        {
            var response = await _orderService.UpdateOrderStatus(orderId, Sd.Status_Cancelled);
            if (response != null && response.IsSuccess)
            {
                TempData["success"] = "Status updated successfully";
                return RedirectToAction(nameof(OrderDetail), new { orderId = orderId });
            }
            return View();
        }


        [HttpGet]
        public IActionResult GetAll(string status) 
        {
            IEnumerable<OrderHeaderDto> list;
            string userId = "";
            if (!User.IsInRole(Sd.RoleAdmin))
            {
                userId = User.Claims.Where(u => u.Type == JwtRegisteredClaimNames.Sub)?.FirstOrDefault()?.Value;
            }
            ResponseDto response = _orderService.GetAllOrder(userId).GetAwaiter().GetResult();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderHeaderDto>>(Convert.ToString(response.Result));
                switch (status)
                {
                    case "approved":
                        list = list.Where(u => u.Status == Sd.Status_Approved);
                        break;
					case "readyforpickup":
						list = list.Where(u => u.Status == Sd.Status_ReadyForPickup);
						break;
					case "cancelled":
						list = list.Where(u => u.Status == Sd.Status_Cancelled || u.Status == Sd.Status_Refunded);
						break;
					default:
						break;
				}
            }
            else
            {
                list = new List<OrderHeaderDto>();
            }
            return Json(new { data = list.OrderByDescending(u=>u.OrderHeaderId) });
        }
    }
}
