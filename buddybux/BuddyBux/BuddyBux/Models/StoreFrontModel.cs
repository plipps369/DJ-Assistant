using BuddyBux.Models.Database;
using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models
{
    public class StoreFrontModel
    {
        public StoreItem Store { get; set; }

        /// <summary>
        /// This uses the product id as the key
        /// </summary>
        public Dictionary<int, ProductItem> Products { get; set; } = new Dictionary<int, ProductItem>();

        public List<StoreProductItem> StoreProducts { get; set; }
    }
}
