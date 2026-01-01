using Model;

namespace Repository
{
  public interface IProductRepository
  {
       Task<(List<Product> Items, int TotalCount)> getProducts(int position, int skip, string? desc, double? minPrice, double? maxPrice, int?[] categoryIds);
  }
}