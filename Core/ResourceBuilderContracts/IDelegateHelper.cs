using System;
using System.Reflection;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilderContracts
{
    public interface IDelegateHelper
    {
        Delegate CreateGetter(FieldInfo field);

        Action<TType, TFieldType> CreateSetter<TType, TFieldType>(FieldInfo field);
    }
}