using Dto;
using Model;

namespace Services
{
  public interface ICategoryService
  {
    Task<IEnumerable<DtoCategory_Name_Id>> GetCategories();
  }
}
