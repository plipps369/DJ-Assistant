using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    [Serializable]
    public class PartyItem:BaseItem
    {
        public int DJId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
