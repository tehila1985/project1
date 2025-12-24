using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DtoCategory_Name_Id
    {
        public int CategoryId { get; set; }

        [Required(ErrorMessage = "Category name is required.")]
        [StringLength(50, ErrorMessage = "Category name cannot be longer than 50 characters.")]
        public string Name { get; set; }
    }
}
