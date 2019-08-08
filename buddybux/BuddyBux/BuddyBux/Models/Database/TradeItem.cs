using BuddyBux.Models.Database;
using BuddyBux.Utility;

namespace BuddyBux.DAO
{
    public class TradeItem : BaseItem
    {
        public int OriginatorId { get; set; } = BaseItem.InvalidId;
        public int DesigneeId { get; set; } = BaseItem.InvalidId;
        public int ProvidedCurrencyId { get; set; } = BaseItem.InvalidId;
        public int RequestedCurrencyId { get; set; } = BaseItem.InvalidId;
        public int ProvidedCurrencyQty { get; set; }
        public int RequestedCurrencyQty { get; set; } 
        public int TradeStatusId { get; set; } = BaseItem.InvalidId;

        public enum StatusId
        {
            Pending = 1,
            Accepted = 2,
            Rejected = 3
        };

        public TradeItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}