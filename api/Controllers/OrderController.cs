using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Data;
using api.DTOs.Order;
using api.Mappers;
using api.Repositories.Orders;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepository _orderRepository;

        public OrderController(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        [HttpGet]
        [Route("all")]
        public async Task<IActionResult> GetAllOrders()
        {
            var orders = await _orderRepository.GetAllOrdersAsync();
            var orderDTOs = orders.Select(b => b.ToOrderDTO()).ToList();
            return Ok(orderDTOs);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetOrderById(int id)
        {
            var order = await _orderRepository.GetOrderByIdAsync(id);
            if (order == null)
            {
                return NotFound("Order ID not found");
            }
            var orderDTO = order.ToOrderDTO();
            return Ok(orderDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderResponseDTO dTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newOrder = dTO.ToCreateOrderResponseDTO();
            var createdOrder = await _orderRepository.CreateOrderAsync(newOrder);
            var orderDTO = createdOrder.ToOrderDTO();
            return CreatedAtAction(nameof(GetOrderById), new { id = orderDTO.Id }, orderDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderResponseDTO dTO, int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var orderToUpdate = dTO.ToUpdateOrderResponseDTO();
            var updatedOrder = await _orderRepository.UpdateOrderAsync(orderToUpdate, id);
            if (updatedOrder == null)
            {
                return NotFound("Order not found");
            }
            var orderDTO = updatedOrder.ToOrderDTO();
            return Ok(orderDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteBanner(int id)
        {
            var deletedOrder = await _orderRepository.DeleteOrderAsync(id);
            if (deletedOrder == null)
            {
                return NotFound("Order ID not found");
            }
            return Ok("Order deleted successfully!");
        }
    }
}