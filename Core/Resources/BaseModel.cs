using System.Collections.Generic;
using CodeFiction.InfinityFiction.Core.Resources.DataTypes;

namespace CodeFiction.InfinityFiction.Core.Resources
{
    public abstract class BaseModel
    {
        protected BaseModel()
        {
            Properties = new Dictionary<string, BaseDataType>(); 
        }

        public Dictionary<string ,BaseDataType> Properties { get; set; }
    }
}
