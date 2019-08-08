using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class StoreProductItem : BaseItem
    {
        public int ProductId { get; set; } = BaseItem.InvalidId;
        public int StoreId { get; set; } = BaseItem.InvalidId;
        public int Qty { get; set; }
        public int Cost { get; set; }
        public int CurrencyId { get; set; } = BaseItem.InvalidId;

        public StoreProductItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
