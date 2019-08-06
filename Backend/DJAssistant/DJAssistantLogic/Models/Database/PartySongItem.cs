using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    [Serializable]
    public class PartySongItem: BaseItem
    {
        public int PartyId { get; set; }
        public int SongId { get; set; }
        public int PlayOrder { get; set; }
        public bool Played { get; set; }
    }
}
