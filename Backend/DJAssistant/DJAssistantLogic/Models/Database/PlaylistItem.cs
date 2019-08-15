using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    public class PlaylistItem: BaseItem
    {
        public int Dj_id { get; set; }
        public string Title { get; set; }
    }
}
