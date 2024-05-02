using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Tables;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.EF;

public class OrderItemService
{
    private readonly AppDbContext _appDbContext;

    public OrderItemService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Get all order items
    public async Task<IEnumerable<OrderItem>> GetAllOrderItems()
    {
        var orderItems = await _appDbContext
            .OrderItems.Include(p => p.Product)
            .Include(u => u.User)
            .Include(o => o.Order)
            .ToListAsync();
        return orderItems;
    }

    // Get order item by ID
    public async Task<OrderItem?> GetOrderItemById(Guid id)
    {
        var orderItems = await _appDbContext
            .OrderItems.Include(p => p.Product)
            .Include(u => u.User)
            .Include(o => o.Order)
            .FirstOrDefaultAsync(orderItem => orderItem.OrderItemId == id);
        return orderItems;
    }

    // Create a new order item
    public async Task<OrderItemModel> AddOrderItem(OrderItemModel newOrderItem)
    {
        OrderItem orderItem = new OrderItem
        {
            OrderItemId = Guid.NewGuid(),
            Quantity = newOrderItem.Quantity,
            Price = newOrderItem.Price,
        };
        await _appDbContext.OrderItems.AddAsync(orderItem);
        await _appDbContext.SaveChangesAsync();
        return newOrderItem;
    }

    // Update an existing order item
    public async Task<OrderItem?> UpdateOrderItem(Guid id, OrderItemModel updateOrderItem)
    {
        var foundOrderItem = await _appDbContext.OrderItems.FirstOrDefaultAsync(orderItem =>
            orderItem.OrderItemId == id
        );
        if (foundOrderItem != null)
        {
            foundOrderItem.Quantity = updateOrderItem.Quantity;
            foundOrderItem.Price = updateOrderItem.Price;
        }
        await _appDbContext.SaveChangesAsync();
        return foundOrderItem;
    }

    public async Task<bool> DeleteOrderItem(Guid id)
    {
        await Task.CompletedTask;
        var foundOrderItem = await _appDbContext.OrderItems.FirstOrDefaultAsync(orderItem =>
            orderItem.OrderItemId == id
        );
        if (foundOrderItem != null)
        {
            _appDbContext.OrderItems.Remove(foundOrderItem);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
