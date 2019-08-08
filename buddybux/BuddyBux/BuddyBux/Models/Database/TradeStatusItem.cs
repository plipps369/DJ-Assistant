using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class TradeStatusItem : BaseItem
    {
        public string Status { get; set; }

        public TradeStatusItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
