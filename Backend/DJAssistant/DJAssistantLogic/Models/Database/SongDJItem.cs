using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    [Serializable]
    public class SongDJItem
    {
        public int SongId { get; set; }
        public int DJId { get; set; }
    }
}
