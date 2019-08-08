using BuddyBux.Utility;
using System;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class RoleItem : BaseItem
    {
        public string Name { get; set; }

        public RoleItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
