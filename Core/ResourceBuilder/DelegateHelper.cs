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
        private const BindingFlags Bflags = BindingFlags.Instance
                                    | BindingFlags.Public
                                    | BindingFlags.NonPublic
                                    | BindingFlags.ExactBinding;

        private static readonly Dictionary<string, Delegate> DelegatesDictionary = new Dictionary<string, Delegate>();

        public Delegate CreateGetter(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }
            Type fieldType = field.FieldType;
            Type memberType = field.ReflectedType;
            string methodName = field.ReflectedType.FullName + ".get_" + field.Name;

            if (DelegatesDictionary.ContainsKey(methodName))
            {
                return DelegatesDictionary[methodName];
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
            Delegate @delegate = setterMethod.CreateDelegate(funcType);
            DelegatesDictionary.Add(methodName, @delegate);

            return @delegate;
        }

        public Action<TType, TFieldType> CreateSetter<TType, TFieldType>(FieldInfo field)
        {
            if (field == null)
            {
                throw new ArgumentNullException("field");
            }

            string methodName = field.ReflectedType.FullName + ".set_" + field.Name;

            if (DelegatesDictionary.ContainsKey(methodName))
            {
                return (Action<TType, TFieldType>)DelegatesDictionary[methodName];
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
            var action = setterMethod.CreateDelegate(typeof(Action<TType, TFieldType>));
            DelegatesDictionary.Add(methodName, action);
            return (Action<TType, TFieldType>)action;
        }

        public Delegate DynamicNew<T>()
            where T : class, new()
        {
            Type type = typeof(T);

            string methodName = type.FullName + ".Ctor";

            if (DelegatesDictionary.ContainsKey(methodName))
            {
                return DelegatesDictionary[methodName];
            }

            var dm = new DynamicMethod(methodName, type, null, type.Module);

            ILGenerator il = dm.GetILGenerator();

            il.Emit(OpCodes.Newobj, type.GetConstructor(Bflags, null, Type.EmptyTypes, null));
            il.Emit(OpCodes.Ret);

            Delegate @delegate = dm.CreateDelegate(typeof(Func<T>));
            DelegatesDictionary.Add(methodName, @delegate);
            return @delegate;
        }
    }
}
