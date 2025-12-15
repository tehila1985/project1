using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dto
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    namespace Dto
    {
        public class DtoOrderItem_Id_OrderId_ProductId_Quantity
        {

            public int OrderItemId { get; set; }

            public int OrderId { get; set; }

            public string ItemName { get; set; }
            public int? ProductId { get; set; }

            public int? Quantity { get; set; }
        }
    }
}
