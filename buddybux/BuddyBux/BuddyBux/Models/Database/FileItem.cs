using BuddyBux.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuddyBux.Models.Database
{
    [Serializable]
    public class FileItem : BaseItem
    {
        public string OriginalName { get; set; }
        public string StorageName { get; set; }
        public string StoragePath { get; set; }
        public int Size { get; set; }
        public string Hash { get; set; }

        public FileItem Clone()
        {
            return ObjectCopier.Clone(this);
        }
    }
}
