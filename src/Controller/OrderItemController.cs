using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using api.Controllers;
using ecommerce.EF;
using ecommerce.Models;
using ecommerce.Tables;
using ecommerce.utils;
using Microsoft.AspNetCore.Mvc;

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
            Console.Write($"An error occurred while retrieving all order items");
            return ApiResponse.ServerError("There is an error on getting the orderItems");
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
                return ApiResponse.BadRequest("The orderItem not found");
            }
            return ApiResponse.Success(orderItem, "orderItem Detail");
        }
        catch (Exception)
        {
            Console.Write($"An error occurred while retrieving the order item");
            return ApiResponse.ServerError("There is an error on getting the orderItem");
        }
    }

    [HttpPost]
    public async Task<IActionResult> AddOrderItem(OrderItemModel newOrderItem)
    {
        try
        {
            await _orderItemService.AddOrderItem(newOrderItem);
            return ApiResponse.Created(newOrderItem, "The OrderItem is Added");
        }
        catch (Exception)
        {
            Console.Write($"An error occurred while creating the order item");
            return ApiResponse.ServerError("Cannot add the OrderItem");
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
                return ApiResponse.NotFound("The OrderItem not found");
            }
            return ApiResponse.Success(found, "OrderItem updated");

        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on updating OrderItem"

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
                return ApiResponse.NotFound("The OrderItem not found");
            }
            return ApiResponse.Success("", "OrderItem Deleted");
        }
        catch (Exception)
        {
            return ApiResponse.ServerError("There is an error on deleting OrderItem"

                );
        }
    }
}
