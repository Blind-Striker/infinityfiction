﻿using System;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;

namespace CodeFiction.InfinityFiction.Core.Utilities
{
    public static class DelegateHelper
    {
        public static Delegate CreateGetter(FieldInfo field)
        {
            Type fieldType = field.FieldType;
            Type memberType = field.ReflectedType;
            string methodName = field.ReflectedType.FullName + ".get_" + field.Name;
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
            return setterMethod.CreateDelegate(funcType);
        }

        public static Action<TType, TFieldType> CreateSetter<TType, TFieldType>(FieldInfo field)
        {
            string methodName = field.ReflectedType.FullName + ".set_" + field.Name;
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
            return (Action<TType, TFieldType>)setterMethod.CreateDelegate(typeof(Action<TType, TFieldType>));
        }
    }
}
