using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DtoProduct_Id_Name_Category_Price_Desc_Image
    {
        public int ProductId { get; set; }

        [Required(ErrorMessage = "Product name is required.")]
        [StringLength(100, ErrorMessage = "Product name cannot be longer than 100 characters.")]
        public string Name { get; set; }

        public int? CategoryId { get; set; } // Assuming CategoryId can be null

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters.")]
        public string CategoryName { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Price must be a positive value.")]
        public int? Price { get; set; } // Assuming Price can be null

        [StringLength(500, ErrorMessage = "Description cannot be longer than 500 characters.")]
        public string Description { get; set; }

        [Url(ErrorMessage = "Invalid URL format.")]
        public string ImageUrl { get; set; }
    }
}
