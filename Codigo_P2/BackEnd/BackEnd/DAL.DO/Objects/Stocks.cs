using System;
using System.Collections.Generic;
using System.Text;

namespace DAL.DO.Objects
{
    public class Stocks
    {
        public int StoreId { get; set; }
        public int ProductId { get; set; }
        public int? Quantity { get; set; }

       // public virtual Products Product { get; set; }
        public virtual Stores Store { get; set; }
    }
}
