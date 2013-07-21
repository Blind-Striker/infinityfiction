using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeFiction.InfinityFiction.Core.CommonTypes
{
    public class ResourceFile
    {
        public string Folder { get; set; }

        public string File { get; set; }

        public string FullPath { get; set; }

        public string Extension { get; set; }

        public bool ResourceEntry { get; set; }
    }
}
