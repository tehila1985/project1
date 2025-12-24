using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.ComponentModel.DataAnnotations;

namespace Dto
{
    public class DtoOrderItem_Id_OrderId_ProductId_Quantity
    {
        public int OrderItemId { get; set; }

        [Required(ErrorMessage = "Order ID is required.")]
        public int OrderId { get; set; }

        [Required(ErrorMessage = "Item name is required.")]
        [StringLength(100, ErrorMessage = "Item name cannot be longer than 100 characters.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Product ID is required.")]
        public int? ProductId { get; set; } // Assuming ProductId can be null but might need validation based on business logic

        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int? Quantity { get; set; } // Assuming Quantity can be nullable but should be validated if present
    }
}
