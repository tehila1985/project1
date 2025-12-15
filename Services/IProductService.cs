using Dto;
using Model;

namespace Services
{
  public interface IProductService
  {
    Task<IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image>> GetProducts(int[]? categoryId, int? minPrice, int? maxPrice, int? limit, int? page, bool desc);
  }
}