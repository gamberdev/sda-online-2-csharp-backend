using ecommerce.EntityFramework;
using ecommerce.EntityFramework.Table;
using ecommerce.Models;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.service;

public class OrderService
{
    private readonly AppDbContext _appDbContext;

    public OrderService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    // Get all orders
    public async Task<IEnumerable<Order>> GetAllOrders()
    {
        var orders = await _appDbContext
            .Orders.Include(u => u.User)
            .Include(item => item.OrderItems)
            .ToListAsync();
        return orders;
    }

    // Get order by ID
    public async Task<Order?> GetOrderById(Guid id)
    {
        var foundOrder = await _appDbContext
            .Orders.Include(u => u.User)
            .Include(item => item.OrderItems)
            .FirstOrDefaultAsync(order => order.OrderId == id);
        return foundOrder;
    }

     // Get User Orders by UserId
    public async Task<IEnumerable<Order>> GetUserOrder(Guid id)
    {
        var userOrder = await _appDbContext
            .Orders
            .Where(order => order.UserId == id).ToListAsync();
        return userOrder;
    }

    // Create a new Order
    public async Task<OrderModel> CreateOrder(OrderModel newOrder)
    {
        double total = 0;
        var foundOrderItems = await _appDbContext
            .OrderItems.Where(orderItem =>
                orderItem.OrderId == null && orderItem.UserId == newOrder.UserId
            )
            .ToListAsync();
        Order order = new Order
        {
            OrderId = Guid.NewGuid(),
            OrderDate = DateTime.UtcNow,
            TotalPrice = 1, //just as default value
            PaymentMethod = newOrder.PaymentMethod,
            OrderStatus = OrderStatus.Pending,
            DeliveryDate = DateTime.UtcNow.AddDays(5),
            DeliveryAddress = newOrder.DeliveryAddress ?? "",
            UserId = newOrder.UserId
        };

        foreach (var orderItem in foundOrderItems)
        {
            orderItem.OrderId = order.OrderId;
            total += orderItem.Price * orderItem.Quantity;
        }
        order.TotalPrice = total;

        if (foundOrderItems.Any())
        {
            await _appDbContext.Orders.AddAsync(order);
            await _appDbContext.SaveChangesAsync();
            return newOrder;
        }
        throw new Exception("There is a problem on Add Order");
    }

    // Update an existing order
    public async Task<Order?> UpdateOrder(Guid id, OrderModel updatedOrder)
    {
        var foundOrder = await _appDbContext.Orders.FirstOrDefaultAsync(order =>
            order.OrderId == id
        );
        if (foundOrder != null)
        {
            foundOrder.TotalPrice = foundOrder.TotalPrice; // Should not be change
            foundOrder.PaymentMethod = updatedOrder.PaymentMethod ?? foundOrder.PaymentMethod;
            foundOrder.OrderStatus = updatedOrder.OrderStatus;
            foundOrder.DeliveryAddress = updatedOrder.DeliveryAddress ?? foundOrder.DeliveryAddress;
        }
        await _appDbContext.SaveChangesAsync();
        return foundOrder;
    }

    // Delete a order
    public async Task<bool> DeleteOrder(Guid id)
    {
        await Task.CompletedTask;
        var foundOrder = await _appDbContext.Orders.FirstOrDefaultAsync(order =>
            order.OrderId == id
        );
        if (foundOrder != null)
        {
            _appDbContext.Orders.Remove(foundOrder);
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }

    // Update the existing order cancellation method to change the order status to "canceled"
    public async Task<bool> CancelOrder(Guid id)
    {
        var foundOrder = await _appDbContext.Orders.FirstOrDefaultAsync(order =>
            order.OrderId == id
        );
        if (foundOrder != null)
        {
            foundOrder.OrderStatus = OrderStatus.Canceled;
            await _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
