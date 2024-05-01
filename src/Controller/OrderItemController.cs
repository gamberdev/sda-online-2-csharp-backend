using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("/OrderItems")]

    public class OrderItemController : ControllerBase
    {
        private readonly OrderItemService _orderItemService;
        public OrderItemController(AppDbContext appDbContext)
        {
            _orderItemService = new OrderItemService(appDbContext);
        }

        [HttpGet]
    public async Task<IActionResult> GetAllOrderItems(){
         try
        {
            var orderItems = await _orderItemService.GetAllOrderItems();
            return Ok(orderItems);
        }

   catch (Exception ex)
        {
            Console.Write($"An error occurred while retrieving all order items");
            return StatusCode(500, ex.Message);
        }
    }



        [HttpGet("{id}")]
    public async Task<IActionResult> GetOrderItemById(Guid id)
        {
            try
        {
            var orderItem = await _orderItemService.GetOrderItemById(id);
            if (orderItem == null)
            {
                return NotFound();
            }
             return Ok(orderItem);
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while retrieving the order item");
            return StatusCode(500, ex.Message);
        }
    }



         [HttpPost]
    public async Task<IActionResult> AddOrderItem(OrderItemModel newOrderItem)
    {
        try
        {
           await _orderItemService.AddOrderItem(newOrderItem);
                  return Ok(new SuccessResponse<ReviewModel> { Message = "The OrderItem is Added" });

        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while creating the order item");
            return StatusCode(500, ex.Message);
        }
    }



        [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrderItem(Guid id, OrderItemModel updateData)
        {
     try
        {
            var found = await _orderItemService.UpdateOrderItem(id, updateData);

            if (found == null)
            {
                return NotFound(new ErrorResponse { Message = "The OrderItem not found" });
            }
            return Ok(new SuccessResponse<OrderItem> { Message = "Review OrderItem", Data = found });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on updating Review" }
            );
        }
    }

      [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        try
        {
             var deleted = await _orderItemService.DeleteOrderItem(id);
            if (!deleted)
            {
                return NotFound(new ErrorResponse { Message = "The OrderItem not found" });
            }
            return Ok(new SuccessResponse<bool> { Message = "OrderItem Deleted" });
        }
        catch (Exception)
        {
            return StatusCode(
                500,
                new ErrorResponse { Message = "There is an error on deleting OrderItem" }
            );
        }
    }
}