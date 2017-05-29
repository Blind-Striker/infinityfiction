using CodeFiction.InfinityFiction.Structure.Resources.Structures;

namespace CodeFiction.InfinityFiction.Structure.Resources.Models
{
    public class ChitinKeyResource
    {
        public Header Header { get; set; }

        public BiffEntryStruct[] BiffEntries { get; set; }

        public BiffInfoStruct[] BiffInfos { get; set; }

        public ResourceEntryStruct[] ResourceEntries { get; set; }
    }
}
