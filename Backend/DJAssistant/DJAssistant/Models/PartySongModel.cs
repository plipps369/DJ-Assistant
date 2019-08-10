using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJAssistantAPI.Models
{
    [Serializable]
    public class PartySongModel
    {
        public int PartyId { get; set; }
        public int SongId { get; set; }
    }
}
