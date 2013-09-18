using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder
{
    public class DelegateHelper : IDelegateHelper
    {
        private static readonly Dictionary<string, Delegate> DelegatesDictionary = new Dictionary<string, Delegate>();
        private static readonly Dictionary<string, Delegate> ConstructorsDictionary = new Dictionary<string, Delegate>();

        public Delegate CreateGetter(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            Type fieldType = field.FieldType;
            Type memberType = field.ReflectedType;
            string methodName = string.Format("{0}{1}{2}", memberType.FullName, ".get_", field.Name);

            Delegate @delegate;
            if (DelegatesDictionary.TryGetValue(methodName, out @delegate))
            {
                return @delegate;
            }

            var setterMethod = new DynamicMethod(methodName, fieldType, new[] { memberType }, true);
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
            @delegate = setterMethod.CreateDelegate(funcType);
            DelegatesDictionary.Add(methodName, @delegate);

            return @delegate;
        }

        public Action<TType, TFieldType> CreateSetter<TType, TFieldType>(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            string methodName = string.Format("{0}{1}{2}", field.ReflectedType.FullName, ".set_", field.Name);

            Delegate action;
            if (DelegatesDictionary.TryGetValue(methodName, out action))
            {
                return (Action<TType, TFieldType>)action;
            }

            var setterMethod = new DynamicMethod(methodName, null, new[] { typeof(TType), typeof(TFieldType) }, true);
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
            action = setterMethod.CreateDelegate(typeof(Action<TType, TFieldType>));
            DelegatesDictionary.Add(methodName, action);
            return (Action<TType, TFieldType>)action;
        }

        public Delegate DynamicNew<T>()
            where T : class, new()
        {
            Type type = typeof(T);
            ConstructorInfo ctor = type.GetConstructor(new Type[0]);
            string methodName = string.Format("{0}{1}", type.Name, ".ctor");

            Delegate @delegate;
            if (ConstructorsDictionary.TryGetValue(methodName, out @delegate))
            {
                return @delegate;
            }

            DynamicMethod dm = new DynamicMethod(methodName, type, new Type[0], type.Module);

            ILGenerator lgen = dm.GetILGenerator();
            lgen.Emit(OpCodes.Newobj, ctor);
            lgen.Emit(OpCodes.Ret);

            @delegate = dm.CreateDelegate(typeof(Func<T>));
            ConstructorsDictionary.Add(methodName, @delegate);
            return @delegate;
        }
    }
}
