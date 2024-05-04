using ecommerce.Models;
using Microsoft.EntityFrameworkCore;
using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;

namespace ecommerce.service;

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
        var product = await _appDbContext.Products.FindAsync(newOrderItem.ProductId);
        if (product != null)
        {
            OrderItem orderItem = new OrderItem
            {
                OrderItemId = Guid.NewGuid(),
                Quantity = newOrderItem.Quantity,
                Price = product.Price,
                ProductId = newOrderItem.ProductId,
                UserId = newOrderItem.UserId,
                OrderId = newOrderItem.OrderId,
            };
            await _appDbContext.OrderItems.AddAsync(orderItem);
            await _appDbContext.SaveChangesAsync();
            return newOrderItem;
        }
        throw new InvalidOperationException("Product not found to Add");
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
