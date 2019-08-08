using BuddyBux.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models
{
    [Serializable]
    public class UserAccountModel
    {
        public UserItem User { get; set; }
        public CurrencyItem Currency { get; set; }
        public StoreItem Store { get; set; }
    }
}
