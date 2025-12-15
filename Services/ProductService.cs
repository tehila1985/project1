using Repository;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
  public class ProductService : IProductService
  {
    IProductRepository _r;
    public ProductService(IProductRepository i)
    {
      _r = i;
    }

    public async Task<IEnumerable<Product>> GetProducts(int[]? categoryId, int? minPrice, int? maxPrice, int? limit, int? page, Boolean desc)
    {
            return await _r.GetProducts(categoryId, minPrice, maxPrice, limit, page, desc);
    }
  }
}
