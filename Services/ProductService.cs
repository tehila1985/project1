using AutoMapper;
using Dto;
using Model;
using Repository;
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
    IMapper _mapper;
        public ProductService(IProductRepository i, IMapper mapper)
            {
                  _r = i;
                 _mapper= mapper;
             }

        public async Task<IEnumerable<DtoProduct_Id_Name_Category_Price_Desc_Image>> GetProducts(int[]? categoryId, int? minPrice, int? maxPrice, int? limit, int? page, Boolean desc)
    {
            var u = await _r.GetProducts(categoryId, minPrice, maxPrice, limit, page, desc);
            var r = _mapper.Map<List<Product>, List<DtoProduct_Id_Name_Category_Price_Desc_Image>>(u);
            return r;
    }
  }
}
