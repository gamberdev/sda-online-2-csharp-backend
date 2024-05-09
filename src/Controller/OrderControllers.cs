using System.Security.Claims;
using ecommerce.EntityFramework;
using ecommerce.Middleware;
using ecommerce.Models;
using ecommerce.service;
using ecommerce.utils;
using Microsoft.AspNetCore.Authorization;
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
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrders()
    {
        var orders = await _orderService.GetAllOrders();

        if (orders.ToList().Count < 1)
        {
            throw new NotFoundException("No orders found");
        }

        return ApiResponse.Success(orders, "All Orders are returned successfully");
    }

    [HttpGet("{id:guid}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetOrder(Guid id)
    {
        var foundOrder =
            await _orderService.GetOrderById(id)
            ?? throw new NotFoundException("The order not found");

        return ApiResponse.Success(foundOrder, "Order is returned successfully");
    }

    [HttpGet("userOrder")]
    [Authorize]
    public async Task<IActionResult> GetUserOrder()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var idUser = Guid.Parse(userIdString!);
        var foundUserOrders = await _orderService.GetUserOrder(idUser);
        if (!foundUserOrders.Any())
        {
            throw new NotFoundException("There is no orders yet!");
        }
        return ApiResponse.Success(foundUserOrders, "User's Orders is returned successfully");
    }

    [HttpPost]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> AddOrder(OrderModel newOrder)
    {
        //identify id depend on the login user
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        newOrder.UserId = Guid.Parse(userIdString!);
        await _orderService.AddOrder(newOrder);
        return ApiResponse.Created(newOrder, "The order is Added");
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> UpdateOrder(Guid id, OrderModel updatedOrder)
    {
        var found =
            await _orderService.UpdateOrder(id, updatedOrder)
            ?? throw new NotFoundException("The order not found");

        return ApiResponse.Success(found, "Order is updated");
    }

    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "Admin")]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> DeleteOrder(Guid id)
    {
        var deleted = await _orderService.DeleteOrder(id);
        if (!deleted)
        {
            throw new NotFoundException("The Order not found");
        }
        return ApiResponse.Success(id, "Order Deleted");
    }

    [HttpPut("{id:guid}/cancel")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    //change order status to cancel by pass order id
    public async Task<IActionResult> CancelOrder(Guid id)
    {
        var canceled = await _orderService.CancelOrder(id);
        if (!canceled)
        {
            throw new BadRequestException("There is no order with this ID to cancel");
        }
        return ApiResponse.Success(id, "Order Canceled");
    }
}
