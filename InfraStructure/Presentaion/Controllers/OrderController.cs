using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ServiceAbstraction;
using Shared.Dtos.OrderModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Presentaion.Controllers
{
    public class OrderController(IServiceManger _serviceManger):ApiBaseController
    {
        // Create Order
        [Authorize]
        [HttpPost]
        public ActionResult<OrderToReturnDto> CreateOrder(OrderDto orderDto)
        {
            var order = _serviceManger.OrderService.CreateOrderAsync(orderDto, GetEmailFromToken());
            return Ok(order);
        }

        // Get Delivery Methods
        [HttpGet("DeliveryMethods")]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<DeliveryMethodDto>>> GetDeliveryMethods()
        {
            var deliveryMethods = await _serviceManger.OrderService.GetDeliveryMethodsAsync();
            return Ok(deliveryMethods);
        }

        // Get Order By Email
        [Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderToReturnDto>>> GetAllOrdersAsync()
        {
            var orders = await _serviceManger.OrderService.GetAllOrdersAsync(GetEmailFromToken());
            return Ok(orders);
        }
        // Get Order By Id
        [Authorize]
        [HttpGet("{id:guid}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByIdAsync(Guid id)
        {
            var order = await _serviceManger.OrderService.GetOrderByIdAsync(id);
            return Ok(order);
        }
    }
}
