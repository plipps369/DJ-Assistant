using System;
using System.Collections.Generic;
using System.Text;

namespace BuddyBux.Models
{
    public class TradeModel
    {
        public string Originator { get; set; } 
        public string Designee { get; set; }
        public int ProvidedCurrencyQty { get; set; }
        public int RequestedCurrencyQty { get; set; }
    }
}
