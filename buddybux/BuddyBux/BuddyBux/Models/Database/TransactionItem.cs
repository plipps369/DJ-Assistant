using BuddyBux.Models.Database;
using BuddyBux.Utility;
using System;

namespace BuddyBux.DAO
{
    public class TransactionItem : BaseItem
    {
        public int StoreProductId { get; set; } = BaseItem.InvalidId;
        public DateTime PurchaseDate { get; set; } = DateTime.UtcNow;
        public int Qty { get; set; } = BaseItem.InvalidId;
        public int PurchaserId { get; set; } = BaseItem.InvalidId;
        public int Cost { get; set; }
        public int CurrencyId { get; set; } = BaseItem.InvalidId;

        public TransactionItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}