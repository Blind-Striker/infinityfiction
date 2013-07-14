using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using CodeFiction.InfinityFiction.Core.Resources;
using CodeFiction.InfinityFiction.Core.Resources.DataTypes;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder
{
    public static class ResourceConverterHelper
    {
        public static void Convert<TStruct, TResource>(IGenericStructConverter genericStructConverter,TStruct @struct, TResource resource)
            where TResource : BaseModel
        {
            FieldInfo[] fieldInfos = typeof (TStruct).GetFields();

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                Delegate getter = CreateGetter(fieldInfo);
                object value = getter.DynamicInvoke(@struct);

                resource.Properties.Add(new SimpleDataType {Name = fieldInfo.Name, Value = value, Type = fieldInfo.FieldType});
            }
        }

        static Delegate CreateGetter(FieldInfo field)
        {
            Type fieldType = field.FieldType;
            Type memberType = field.ReflectedType;
            string methodName = field.ReflectedType.FullName + ".get_" + field.Name;
            DynamicMethod setterMethod = new DynamicMethod(methodName, fieldType, new Type[1] { memberType }, true);
            ILGenerator gen = setterMethod.GetILGenerator();
            if (field.IsStatic)
            {
                gen.Emit(OpCodes.Ldsfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldfld, field);
            }
            gen.Emit(OpCodes.Ret);
            Type funcType = Expression.GetFuncType(memberType, fieldType);
            return setterMethod.CreateDelegate(funcType);
        }

        static Action<S, T> CreateSetter<S, T>(FieldInfo field)
        {
            string methodName = field.ReflectedType.FullName + ".set_" + field.Name;
            DynamicMethod setterMethod = new DynamicMethod(methodName, null, new Type[2] { typeof(S), typeof(T) }, true);
            ILGenerator gen = setterMethod.GetILGenerator();
            if (field.IsStatic)
            {
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Stsfld, field);
            }
            else
            {
                gen.Emit(OpCodes.Ldarg_0);
                gen.Emit(OpCodes.Ldarg_1);
                gen.Emit(OpCodes.Stfld, field);
            }
            gen.Emit(OpCodes.Ret);
            return (Action<S, T>)setterMethod.CreateDelegate(typeof(Action<S, T>));
        }
    }
}
