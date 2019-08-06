using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    [Serializable]
    public class SongItem
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Length { get; set; }
        public bool Explicit { get; set; }
    }
}
