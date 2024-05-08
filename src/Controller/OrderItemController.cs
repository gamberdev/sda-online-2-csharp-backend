using ecommerce.EntityFramework;
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

    public OrderItemController(AppDbContext appDbContext)
    {
        _orderItemService = new OrderItemService(appDbContext);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetAllOrderItems()
    {
        try
        {
            var orderItems = await _orderItemService.GetAllOrderItems();

            if (orderItems.Count() <= 0)
            {
                return ApiResponse.NotFound("There is no orderItems");
            }
            return ApiResponse.Success(orderItems, "All orderItems inside E-commerce system");
        }
        catch (Exception)
        {
            Console.Write($"An error occurred while retrieving all orderItems");
            return ApiResponse.ServerError("There is an error on getting the orderItems");
        }
    }

    [HttpGet("cart")]
    [Authorize]
    public async Task<IActionResult> GetCartItem(Guid id)
    {
        try
        {
            var cartItems = await _orderItemService.GetCartItem(id);

            if (cartItems.Count() <= 0)
            {
                return ApiResponse.NotFound("The Cart is Empty");
            }
            return ApiResponse.Success(cartItems, "All cartItems for the current user");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on getting the cartItems");
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetOrderItemById(Guid id)
    {
        try
        {
            var orderItem = await _orderItemService.GetOrderItemById(id);
            if (orderItem == null)
            {
                return ApiResponse.BadRequest("The orderItem not found");
            }
            return ApiResponse.Success(orderItem, "orderItem Detail");
        }
        catch (Exception)
        {
            Console.Write($"An error occurred while retrieving the orderItem");
            return ApiResponse.ServerError("There is an error on getting the orderItem");
        }
    }

    [HttpPost]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> AddOrderItem(OrderItemModel newOrderItem)
    {
        try
        {
            await _orderItemService.AddOrderItem(newOrderItem);
            return ApiResponse.Created(newOrderItem, "The OrderItem is Added");
        }
        catch (InvalidOperationException ex)
        {
            return ApiResponse.BadRequest(ex.Message);
        }
        catch (Exception)
        {
            Console.Write($"An error occurred while creating the orderItem");
            return ApiResponse.ServerError("Cannot add the OrderItem");
        }
    }

    [HttpPut("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> UpdateOrderItem(Guid id, OrderItemModel updateData)
    {
        try
        {
            var found = await _orderItemService.UpdateOrderItem(id, updateData);
            if (found == null)
            {
                return ApiResponse.NotFound("The OrderItem not found");
            }
            return ApiResponse.Success(found, "OrderItem updated");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on updating OrderItem");
        }
    }

    [HttpDelete("{id:guid}")]
    [Authorize]
    [Authorize(Policy = "RequiredNotBanned")]
    public async Task<IActionResult> DeleteOrderItem(Guid id)
    {
        try
        {
            var deleted = await _orderItemService.DeleteOrderItem(id);
            if (!deleted)
            {
                return ApiResponse.NotFound("The OrderItem not found");
            }
            return ApiResponse.Success(id, "OrderItem Deleted");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on deleting OrderItem");
        }
    }
}
