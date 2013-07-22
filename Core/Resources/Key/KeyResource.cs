using System.Collections.Generic;

using CodeFiction.InfinityFiction.Core.Resources.Dlg;

namespace CodeFiction.InfinityFiction.Core.Resources.Key
{
    public class KeyResource
    {
        public HeaderResource Header { get; set; }

        public BiffEntryResource[] BiffEntries { get; set; }

        public ResourceEntryResource[] ResourceEntries { get; set; }

        public Dictionary<int,string> ExtensionMap { get; set; }
    }
}
