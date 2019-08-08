using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class ProductItem : BaseItem
    {
        public string Name { get; set; }
        public int ImageFileId { get; set; } = BaseItem.InvalidId;
        public bool IsCharitable { get; set; }
        public int CreatorId { get; set; }

        public ProductItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
