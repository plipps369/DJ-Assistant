using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class StoreItem : BaseItem
    {
        public int UserId { get; set; } = BaseItem.InvalidId;

        public StoreItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
