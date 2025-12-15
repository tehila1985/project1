using Model;
using Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class OrderService : IOrderService
  {
    IOrderRepository _r;
    public OrderService(IOrderRepository i)
    {
      _r = i;
    }
    public async Task<Order?> GetOrderById(int id)
    {
      return await _r.GetOrderById(id);
    }

    public async Task<Order> AddNewOrder(Order order)
    {
      return await _r.AddNewOrder(order);
    }
  }
}
