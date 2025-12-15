using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class OrderRepository : IOrderRepository
  {
    myDBContext dbContext;
    public OrderRepository(myDBContext dbContext)
    {
      this.dbContext = dbContext;
    }

    public async Task<Order?> GetOrderById(int id)
    {
      return await dbContext.Orders.FindAsync(id);
    }
    public async Task<Order> AddNewOrder(Order order)
    {
      await dbContext.Orders.AddAsync(order);
      await dbContext.SaveChangesAsync();
      return order;
    }
  }

}
