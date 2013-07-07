using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.Resources.DataTypes;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;
using CodeFiction.InfinityFiction.Structure.Structures.Key;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder
{
    public class KeyResourceBuilder
    {
        private readonly IGenericStructConverter _genericStructConverter;

        public KeyResourceBuilder(IGenericStructConverter genericStructConverter)
        {
            _genericStructConverter = genericStructConverter;
        }

        public KeyResource GetKeyResource()
        {
            KeyResource keyResource = new KeyResource();

            string savefile = Path.Combine(@"G:\Games\BGOrg\BGII - SoA", "CHITIN.KEY");
            byte[] content = IoHelper.ReadBinaryFile(savefile);

            Header header = _genericStructConverter.ConvertToStruct<Header>(content, 0);

            uint biffEntriesCount = header.BiffEntriesCount;
            uint resourceEntriesCount = header.ResourceEntriesCount;
            uint biffEntriesOffset = header.BiffEntriesOffset;
            uint resourceEntriesOffset = header.ResourceEntriesOffset;

            BiffEntry[] biffEntryResources = new BiffEntry[biffEntriesCount];
            ResourceEntry[] resourceEntries = new ResourceEntry[resourceEntriesCount];
            keyResource.BiffEntries = new BiffEntryResource[biffEntriesCount];
            keyResource.ResourceEntries = new ResourceEntryResource[resourceEntriesOffset];


            for (int i = 0; i < biffEntriesCount; i++)
            {
                byte[] biffEntryContent = BinaryHelper.GetBytes(content, (int) biffEntriesOffset, 12);

                BiffEntry biffEntry = _genericStructConverter.ConvertToStruct<BiffEntry>(biffEntryContent, 0);

                biffEntryResources[i] = biffEntry;

                biffEntriesOffset += 12;
            }


            for (int i = 0; i < resourceEntriesCount; i++)
            {
                byte[] resourceEntryContent = BinaryHelper.GetBytes(content, (int) resourceEntriesOffset, 14);

                ResourceEntry resourceEntry =
                    _genericStructConverter.ConvertToStruct<ResourceEntry>(resourceEntryContent, 0);

                resourceEntries[i] = resourceEntry;

                resourceEntriesOffset += 14;
            }

            keyResource.Header = new HeaderResource();

            FieldInfo[] fieldInfos = header.GetType().GetFields();

            for (int i = 0; i < fieldInfos.Length; i++)
            {
                FieldInfo fieldInfo = fieldInfos[i];
                Delegate getter = CreateGetter(fieldInfo);
                object value = getter.DynamicInvoke(header);

                keyResource.Header.Properties.Add(new SimpleDataType() { Name = fieldInfo.Name,Value = value});
            }

            FieldInfo[] fields = typeof(BiffEntry).GetFields();

            for (int i = 0; i < biffEntryResources.Length; i++)
            {
                BiffEntry biffEntry = biffEntryResources[i];

                for (int j = 0; j < fields.Length; j++)
                {
                    FieldInfo fieldInfo = fields[j];
                    Delegate getter = CreateGetter(fieldInfo);
                    object value = getter.DynamicInvoke(biffEntry);

                    keyResource.BiffEntries[i].Properties.Add(new SimpleDataType() { Name = fieldInfo.Name, Value = value });
                }
            }

            //for (int i = 0; i < fieldInfos.Length; i++)
            //{
            //    FieldInfo fieldInfo = fieldInfos[i];
            //    Delegate getter = CreateGetter(fieldInfo);
            //    object value = getter.DynamicInvoke(header);

            //    keyResource.Header.Properties.Add(new SimpleDataType() { Name = fieldInfo.Name, Value = value });
            //}


            //Delegate action = Delegate.CreateDelegate(field.FieldType, obj, method);

            return null;
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
