using Microsoft.EntityFrameworkCore;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
  public class ProductRepository : IProductRepository
  {
    myDBContext dbContext;
    public ProductRepository(myDBContext dbContext)
    {
      this.dbContext = dbContext;
    }
    public async Task<List<Product>> GetProducts(int[]? categoryId, int? minPrice, int? maxPrice, int? limit, int? page, Boolean desc)
        {
            var products = await dbContext.Products
                                 .Include(p => p.Category) 
                                 .ToListAsync();

            return products;
        }
  }
}
