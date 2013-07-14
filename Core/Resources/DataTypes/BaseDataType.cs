using System;

namespace CodeFiction.InfinityFiction.Core.Resources.DataTypes
{
    public abstract class BaseDataType
    {
        public string Name { get; set; }
        public object Value { get; set; }
        public Type Type { get; set; }
    }
}
