using Dto;
using Model;

namespace Services
{
    public interface IProductService
    {
        Task<(IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image>, int TotalCount)> GetProducts(int position, int skip, string? desc, double? minPrice, double? maxPrice, int?[] categoryIds);
    }
}