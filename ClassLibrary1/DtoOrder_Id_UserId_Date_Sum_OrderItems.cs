using Dto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Dto
{
    public class DtoOrder_Id_UserId_Date_Sum_OrderItems
    {
        public int OrderId { get; set; }

        [Required(ErrorMessage = "User ID is required.")]
        public int UserId { get; set; }

        [Required(ErrorMessage = "Order date is required.")]
        public DateOnly? Date { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "Sum must be a non-negative value.")]
        public int? Sum { get; set; }

        public virtual ICollection<DtoOrderItem_Id_OrderId_ProductId_Quantity> OrderItems { get; set; } = new List<DtoOrderItem_Id_OrderId_ProductId_Quantity>();
    }
}
