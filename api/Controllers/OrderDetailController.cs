using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using api.DTOs.OrderDetail;
using api.Mappers;
using api.Models;
using api.Repositories.OrderDetails;
using api.Data;

namespace api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderDetailController : ControllerBase
    {
        private readonly IOrderDetailRepository _orderDetailRepository;
        private readonly ShopAppDbContext _context;

        public OrderDetailController(IOrderDetailRepository orderDetailRepository, ShopAppDbContext context)
        {
            _orderDetailRepository = orderDetailRepository;
            _context = context;
        }

        [HttpGet]
        [Route("all")]
        public async Task<ActionResult<IEnumerable<OrderDetailDTO>>> GetAllOrderDetails()
        {
            var orderDetails = await _orderDetailRepository.GetAllOrderDetailsAsync();
            var orderDetailsDTO = orderDetails.Select(od => od.ToOrderDetailDTO());
            return Ok(orderDetailsDTO);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<OrderDetailDTO>> GetOrderDetailById(int id)
        {
            var orderDetail = await _orderDetailRepository.GetOrderDetailByIdAsync(id);
            if (orderDetail == null)
                return NotFound("Order Detail Id doesn't exist");

            var orderDetailDTO = orderDetail.ToOrderDetailDTO();
            return Ok(orderDetailDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<OrderDetailDTO>> CreateOrderDetail(CreateOrderrDetailResponseDTO dto)
        {
            var newOrderDetail = dto.ToCreateOrderDetailResponseDTO(_context);
            var createdOrderDetail = await _orderDetailRepository.CreateOrderDetailAsync(newOrderDetail);
            var orderDetailDTO = createdOrderDetail.ToOrderDetailDTO();
            return CreatedAtAction(nameof(GetOrderDetailById), new { id = orderDetailDTO.Id }, orderDetailDTO);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<ActionResult<OrderDetailDTO>> UpdateOrderDetail(int id, UpdateOrderrDetailResponseDTO dto)
        {
            var orderDetailToUpdate = dto.ToUpdateOrderDetailResponseDTO(_context);
            var updatedOrderDetail = await _orderDetailRepository.UpdateOrderDetailAsync(orderDetailToUpdate, id);
            if (updatedOrderDetail == null)
                return NotFound("Order Detail Id doesn't exist");

            var orderDetailDTO = updatedOrderDetail.ToOrderDetailDTO();
            return Ok(orderDetailDTO);
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<ActionResult<OrderDetailDTO>> DeleteOrderDetail(int id)
        {
            var deletedOrderDetail = await _orderDetailRepository.DeleteOrderDetailAsync(id);
            if (deletedOrderDetail == null)
                return NotFound("Order Detail Id doesn't exist");

            return Ok("Successully deleted Order Detail");
        }
    }
}
