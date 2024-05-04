using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;
using ecommerce.service;


namespace ecommerce.Controller;

[ApiController]
[Route("/orders")]
public class OrderController : ControllerBase
{
    private readonly OrderService _orderService;

    public OrderController(AppDbContext appDbContext)
    {
        _orderService = new OrderService(appDbContext);
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrders()
    {
        try
        {
            var orders = await _orderService.GetAllOrders();

            if (orders.ToList().Count < 1)
            {
                return NotFound(new ErrorResponse { Success = false, Message = "No orders found" });
            }

            return Ok(
                new SuccessResponse<IEnumerable<Order>>
                {
                    Success = true,
                    Message = "all orders are returned successfully",
                    Data = orders
                }
            );
        }
        catch (Exception ex)
        {
            Console.Write($"an error occured while retrieving all orders");

            return StatusCode(500, new ErrorResponse { Message = ex.Message });
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        try
        {
            // return BadRequest("Invalid order ID Format");

            var foundOrder = await _orderService.GetOrderById(id);
            if (foundOrder == null)
            {
                return NotFound(new ErrorResponse { Message = "The order not found" });
            }

            return Ok(
                new SuccessResponse<Order>
                {
                    Success = true,
                    Message = "all order are returned successfully",
                    Data = foundOrder
                }
            );
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while retrieving the orderId");
            return StatusCode(500, new ErrorResponse { Message = ex.Message });
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderModel newOrder)
    {
        try
        {
            await _orderService.CreateOrder(newOrder);
            return Ok(new SuccessResponse<OrderModel> { Message = "The order is Added" });
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while creating the product");
            return StatusCode(500, new ErrorResponse { Message = ex.Message });
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateOrder(Guid id, OrderModel updatedOrder)
    {
        try
        {
            var found = await _orderService.UpdateOrder(id, updatedOrder);
            if (found == null)
            {
                return NotFound(new ErrorResponse { Message = "The order not found" });
            }
            return Ok(new SuccessResponse<Order> { Message = "Order updated", Data = found });
        }
        catch (Exception ex)
        {
            Console.Write($"An error occurred while updating the Order");

            return StatusCode(500, new ErrorResponse { Message = ex.Message });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        try
        {
            var deleted = await _orderService.DeleteOrder(id);
            if (!deleted)
            {
                return NotFound(new ErrorResponse { Message = "The Order not found" });
            }
            return Ok(new SuccessResponse<bool> { Message = "Order Deleted" });
        }
        catch (Exception ex)
        {
            return StatusCode(500, new ErrorResponse { Message = ex.Message });
        }
    }
}
