using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ecommerce.Models;
using ecommerce.Tables;

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
        await Task.CompletedTask;
        var orders = _appDbContext.Orders.ToList();
        return orders;
    }

    // Get order by ID
    public async Task<Order?> GetOrderById(Guid id)
    {
        await Task.CompletedTask;
        var OrdersDb = _appDbContext.Orders.ToList();
        var foundOrder = OrdersDb.FirstOrDefault(order => order.OrderId == id);
        return foundOrder;
    }

    // Create a new Order
    public async Task<OrderModel> CreateOrder(OrderModel newOrder)
    {
        await Task.CompletedTask;
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
        _appDbContext.Orders.Add(order);
        _appDbContext.SaveChanges();
        return newOrder;
    }

    // Update an existing order
    public async Task<Order?> UpdateOrder(Guid id, OrderModel updatedOrder)
    {
        await Task.CompletedTask;
        var orderDb = _appDbContext.Orders.ToList();
        var foundOrder = orderDb.FirstOrDefault(order => order.OrderId == id);
        if (foundOrder != null)
        {
            foundOrder.TotalPrice = updatedOrder.TotalPrice;
            foundOrder.PaymentMethod = updatedOrder.PaymentMethod ?? foundOrder.PaymentMethod;
            foundOrder.OrderStatus = updatedOrder.OrderStatus;
            foundOrder.DeliveryAddress = updatedOrder.DeliveryAddress ?? foundOrder.DeliveryAddress;
        }
        _appDbContext.SaveChanges();
        return foundOrder;
    }

    // Delete a order
    public async Task<bool> DeleteOrder(Guid id)
    {
        await Task.CompletedTask;
        var orderDb = _appDbContext.Orders.ToList();
        var foundOrder = orderDb.FirstOrDefault(order => order.OrderId == id);
        if (foundOrder != null)
        {
            _appDbContext.Orders.Remove(foundOrder);
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
}