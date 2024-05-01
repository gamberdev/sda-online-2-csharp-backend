using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using ecommerce.Models;
using ecommerce.Tables;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.EF;
public class OrderItemService {
   private readonly AppDbContext _appDbContext;

    public OrderItemService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
        }
    
    // Get all order items
    public async Task<IEnumerable<OrderItem>> GetAllOrderItems()
    {
        await Task.CompletedTask;
        var orderItems = _appDbContext.OrderItems.ToList();
        return orderItems;
    }


   // Get order item by ID
    public async Task<OrderItem?> GetOrderItemById(Guid id)
{
      await Task.CompletedTask;
        var orderItemsDb = _appDbContext.OrderItems.ToList();
        var foundOrderItem = orderItemsDb.FirstOrDefault(orderItem => orderItem.OrderItemId == id);
        return foundOrderItem;
}

    // // Create a new order item
  
  public async Task<OrderItemModel> AddOrderItem(OrderItemModel newOrderItem)
    {
        await Task.CompletedTask;
        OrderItem orderItem = new OrderItem
        {
            OrderItemId = Guid.NewGuid(),
            Quantity = newOrderItem.Quantity,
            Price = newOrderItem.Price,
         
        };
        _appDbContext.OrderItems.Add(orderItem);
        _appDbContext.SaveChanges();
        return newOrderItem;
    }


   // Update an existing order item
      public async Task<OrderItem?> UpdateOrderItem(Guid id, OrderItemModel updateOrderItem)
    {
        await Task.CompletedTask;
        var OrderItemsDb = _appDbContext.OrderItems.ToList();
        var foundOrderItem = OrderItemsDb.FirstOrDefault(orderItem => orderItem.OrderItemId == id);
        if (foundOrderItem!= null)
        {
            foundOrderItem.Quantity = updateOrderItem.Quantity;
            foundOrderItem.Price = updateOrderItem.Price;

        }
        _appDbContext.SaveChanges();
        return foundOrderItem;
    }

       public async Task<bool> DeleteOrderItem(Guid id)
{

     await Task.CompletedTask;
        var orderItemsDb = _appDbContext.OrderItems.ToList();
        var foundOrderItem = orderItemsDb.FirstOrDefault(orderItem => orderItem.OrderItemId == id);
        if (foundOrderItem != null)
        {
            _appDbContext.OrderItems.Remove(foundOrderItem);
            _appDbContext.SaveChanges();
            return true;
        }
        return false;
    }
}





    






