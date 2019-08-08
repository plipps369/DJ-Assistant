using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class CurrencyItem : BaseItem
    {
        public string Name { get; set; }
        public int ImageFileId { get; set; } = BaseItem.InvalidId;
        public int OwnerId { get; set; }

        public CurrencyItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
