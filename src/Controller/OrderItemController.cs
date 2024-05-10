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
[Route("/orderItems")]
public class OrderItemController : ControllerBase
{
    private readonly OrderItemService _orderItemService;

    public OrderItemController(OrderItemService orderItemService)
    {
        _orderItemService = orderItemService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrderItems()
    {
        var orderItems = await _orderItemService.GetAllOrderItems();

        if (!orderItems.Any())
        {
            throw new NotFoundException("There is no orderItems");
        }
        return ApiResponse.Success(orderItems, "All orderItems inside E-commerce system");
    }

    [HttpGet("cart")]
    [Authorize]
    public async Task<IActionResult> GetCartItem()
    {
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var idUser = Guid.Parse(userIdString!);
        var cartItems = await _orderItemService.GetCartItem(idUser);

        if (!cartItems.Any())
        {
            throw new NotFoundException("The Cart is Empty!");
        }
        return ApiResponse.Success(cartItems, "All cartItems for the current user");
    }

    [HttpGet("{id:guid}")]
    [Authorize]
    public async Task<IActionResult> GetOrderItemById(Guid id)
    {
        var orderItem =
            await _orderItemService.GetOrderItemById(id)
            ?? throw new BadRequestException("The entered orderItem is not in the system");

        return ApiResponse.Success(orderItem, "orderItem Detail");
    }

    [HttpPost]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> AddOrderItem(OrderItemModel newOrderItem)
    {
        //identify id depend on the login user
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        newOrderItem.UserId = Guid.Parse(userIdString!);
        await _orderItemService.AddOrderItem(newOrderItem);
        return ApiResponse.Created(newOrderItem, "The OrderItem is Added");
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> UpdateOrderItem(Guid id, OrderItemModel updateData)
    {
        var found =
            await _orderItemService.UpdateOrderItem(id, updateData)
            ?? throw new NotFoundException("The OrderItem not found");

        return ApiResponse.Success(found, "OrderItem updated");
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        var deleted = await _orderItemService.DeleteOrderItem(id);
        if (!deleted)
        {
            throw new NotFoundException("The OrderItem not found");
        }
        return ApiResponse.Success(id, "OrderItem Deleted");
    }
}
