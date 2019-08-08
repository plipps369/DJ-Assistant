using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class UserCurrencyItem : BaseItem
    {
        public int CurrencyId { get; set; } = BaseItem.InvalidId;
        public int UserId { get; set; }
        public int Amount { get; set; }

        public UserCurrencyItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
