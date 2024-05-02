using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Tables;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.EF;

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

    // Create a new Order
    public async Task<OrderModel> CreateOrder(OrderModel newOrder)
    {
        Order order = new Order
        {
            OrderId = Guid.NewGuid(),
            TotalPrice = newOrder.TotalPrice,
            OrderDate = DateTime.UtcNow,
            PaymentMethod = newOrder.PaymentMethod,
            OrderStatus = newOrder.OrderStatus,
            DeliveryDate = DateTime.UtcNow.AddDays(5),
            DeliveryAddress = newOrder.DeliveryAddress ?? ""
        };
        await _appDbContext.Orders.AddAsync(order);
        await _appDbContext.SaveChangesAsync();
        return newOrder;
    }

    // Update an existing order
    public async Task<Order?> UpdateOrder(Guid id, OrderModel updatedOrder)
    {
        var foundOrder = await _appDbContext.Orders.FirstOrDefaultAsync(order => order.OrderId == id);
        if (foundOrder != null)
        {
            foundOrder.TotalPrice = updatedOrder.TotalPrice;
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
        var foundOrder = await _appDbContext.Orders.FirstOrDefaultAsync(order => order.OrderId == id);
        if (foundOrder != null)
        {
            _appDbContext.Orders.Remove(foundOrder);
           await  _appDbContext.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
