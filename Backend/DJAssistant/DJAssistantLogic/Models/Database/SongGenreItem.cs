using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    [Serializable]
    public class SongGenreItem
    {
        public int SongId { get; set; }
        public int GenreId { get; set; }
    }
}
