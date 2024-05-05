using System.Net.NetworkInformation;
using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.service;

public class OrderItemService
{
    private readonly AppDbContext _appDbContext;

    public OrderItemService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Get all items ordered or not
    public async Task<IEnumerable<OrderItem>> GetAllOrderItems()
    {
        var orderItems = await _appDbContext
            .OrderItems.Include(p => p.Product)
            .Include(u => u.User)
            .Include(o => o.Order)
            .ToListAsync();
        return orderItems;
    }

    // Get all items that not ordered yet
    public async Task<IEnumerable<OrderItem>> GetCartItem(Guid id)
    {
        var cartItems = await _appDbContext
            .OrderItems.Where(cartItem => cartItem.OrderId == null && cartItem.UserId == id)
            .ToListAsync();
        return cartItems;
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
        var productInCart = await _appDbContext.OrderItems.FirstOrDefaultAsync(p =>
            p.ProductId == newOrderItem.ProductId
            && p.OrderId == null
            && p.UserId == newOrderItem.UserId
        );
        // IF item already on the cart just update the quantity
        if (productInCart != null)
        {
            productInCart.Quantity += newOrderItem.Quantity;
            await _appDbContext.SaveChangesAsync();
            return newOrderItem;
        }
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
            foundOrderItem.Price = foundOrderItem.Price; //should not be change
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
