using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;
using ecommerce.service;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;

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
                return ApiResponse.NotFound("No orders found");
            }

            return ApiResponse.Success(orders, "All Orders are returned successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        try
        {
            var foundOrder = await _orderService.GetOrderById(id);
            if (foundOrder == null)
            {
                return ApiResponse.NotFound("The order not found");
            }

            return ApiResponse.Success(foundOrder, "Order is returned successfully");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(OrderModel newOrder)
    {
        try
        {
            await _orderService.CreateOrder(newOrder);
            return ApiResponse.Created(newOrder, "The order is Added");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
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
                return ApiResponse.NotFound("The order not found");
            }
            return ApiResponse.Success(found, "Order is updated");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
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
                return ApiResponse.NotFound("The Order not found");
            }
            return ApiResponse.Success(id, "Order Deleted");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }

    [HttpPut("{id}/cancel")]
    //change order status to cancel by pass order id
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        try
        {
            var canceled = await _orderService.CancelOrder(id);
            if (!canceled)
            {
                return ApiResponse.NotFound("The order not found");
            }
            return ApiResponse.Success(id, "Order Canceled");
        }
        catch (Exception ex)
        {
            return ApiResponse.ServerError(ex.Message);
        }
    }
}
