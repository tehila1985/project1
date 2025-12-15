using Microsoft.AspNetCore.Mvc;
using Model;
using Services;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class ProductController : ControllerBase
  {

    IProductService _s;
    public ProductController(IProductService i)
    {
      _s = i;
    }
    // GET: api/<CategoryController>
    [HttpGet]
    public async Task<IEnumerable<Product>> Gets([FromBody] int[]? categoryId, int? minPrice, int? maxPrice, int? limit, int? page, Boolean desc)
    {
      return await _s.GetProducts(categoryId, minPrice, maxPrice, limit, page, desc);
    }
  }
}
