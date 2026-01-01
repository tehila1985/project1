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
   public async Task<(List<Product> Items, int TotalCount)> getProducts(int position, int skip, string? desc, double? minPrice, double? maxPrice, int?[] categoryIds)
 {
            var query = dbContext.Products.Where(product =>
                (desc == null ? (true) : (product.Description.Contains(desc)))
                && ((minPrice == null) ? (true) : (product.Price >= minPrice))
                && ((maxPrice == null) ? (true) : (product.Price <= maxPrice))
                && ((categoryIds.Length == 0) ? (true) : (categoryIds.Contains(product.CategoryId))))
                .OrderBy(product => product.Price);

            Console.WriteLine(query.ToQueryString());
            List<Product> products = await query.Skip((position - 1) * skip)
                .Take(skip).Include(product => product.Category).ToListAsync();

            var total = await query.CountAsync();

            return (products, total);
    }
    }
}
