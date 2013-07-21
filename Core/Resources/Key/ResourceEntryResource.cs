using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeFiction.InfinityFiction.Core.Resources.Key
{
    public class ResourceEntryResource : BaseModel
    {
        public string Extension { get; set; }

        public string FileName { get; set; }

        public bool HasOverride { get; set; }
    }
}
