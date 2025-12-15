using Model;

namespace Services
{
  public interface IProductService
  {
    Task<IEnumerable<Product>> GetProducts(int[]? categoryId, int? minPrice, int? maxPrice, int? limit, int? page, bool desc);
  }
}