using Dto;
using Microsoft.AspNetCore.Mvc;
using Model;
using Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class CategoryController : ControllerBase
  {

    ICategoryService _s;
    public CategoryController(ICategoryService i)
    {
      _s = i;
    }
    // GET: api/<CategoryController>
    [HttpGet]
    public async Task<IEnumerable<DtoCategory_Name_Id>> Get()
    {
      return await _s.GetCategories();
    }
  }
}
