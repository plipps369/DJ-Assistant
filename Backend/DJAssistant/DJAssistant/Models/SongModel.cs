using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJAssistantAPI.Models
{
    [Serializable]
    public class SongModel
    {
        public int DJId { get; set; }
        public string Title { get; set; }
        public string Artist { get; set; }
        public int Length { get; set; }
        public bool Explicit { get; set; }
        public List<int> GenresId { get; set; }
    }
}
