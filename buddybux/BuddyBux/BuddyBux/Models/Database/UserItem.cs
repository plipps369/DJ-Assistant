using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class UserItem : BaseItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
        public int RoleId { get; set; } = BaseItem.InvalidId;
        public int ImageFileId { get; set; } = BaseItem.InvalidId;
        public DateTime CreationDate { get; set; } = DateTime.UtcNow;
        public int Rating { get; set; } = 1;

        public UserItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
