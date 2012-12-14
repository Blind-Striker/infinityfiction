using System.Collections.Generic;
using CodeFiction.InfinityFiction.Core.Resources.DataTypes;

namespace CodeFiction.InfinityFiction.Core.Resources
{
    public abstract class BaseModel
    {
        protected BaseModel()
        {
            Properties = new List<BaseDataType>();    
        }

        public List<BaseDataType> Properties { get; set; }
    }
}
