using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    [Serializable]
    public class DJItem: BaseItem
    {
        public string Email { get; set; }
        public string DisplayName { get; set; }
        public string Hash { get; set; }
        public string Salt { get; set; }
    }
}
