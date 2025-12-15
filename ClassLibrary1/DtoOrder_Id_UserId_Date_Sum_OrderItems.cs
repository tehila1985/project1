using Dto.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    public class DtoOrder_Id_UserId_Date_Sum_OrderItems
    {
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public DateOnly? Date { get; set; }

        public int? Sum { get; set; }

        public virtual ICollection<DtoOrderItem_Id_OrderId_ProductId_Quantity> OrderItems { get; set; } = new List<DtoOrderItem_Id_OrderId_ProductId_Quantity>();
    }
}