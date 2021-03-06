﻿using System;
using System.Collections.Generic;
using System.Text;

namespace DJAssistantLogic.Models.Database
{
    [Serializable]
    public class BaseItem
    {
        public const int InvalidId = -1;

        public int Id { get; set; } = InvalidId;
    }
}
