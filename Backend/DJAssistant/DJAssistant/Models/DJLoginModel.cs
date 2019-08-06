using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DJAssistantAPI.Models
{
    [Serializable]
    public class DJLoginModel
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}
