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

            keyResource.BiffEntries = new BiffEntryResource[biffEntriesCount];
            keyResource.ResourceEntries = new ResourceEntryResource[resourceEntriesOffset];


            for (int i = 0; i < biffEntriesCount; i++)
            {
                int sizeOfBiffEntry;
                unsafe
                {
                    sizeOfBiffEntry = sizeof (BiffEntry);
                }

                byte[] biffEntryContent = BinaryHelper.GetBytes(content, (int) biffEntriesOffset, sizeOfBiffEntry);
                BiffEntry biffEntry = _genericStructConverter.ConvertToStruct<BiffEntry>(biffEntryContent, 0);
                keyResource.BiffEntries[i] = new BiffEntryResource();

                FieldInfo[] biffEntryFields = biffEntry.GetType().GetFields();

                for (int j = 0; j < biffEntryFields.Length; j++)
                {
                    FieldInfo fieldInfo = biffEntryFields[j];
                    Delegate getter = CreateGetter(fieldInfo);
                    object value = getter.DynamicInvoke(biffEntry);

                    keyResource.BiffEntries[i].Properties.Add(new SimpleDataType {Name = fieldInfo.Name, Value = value});
                }

                biffEntriesOffset += (uint) sizeOfBiffEntry;
            }

            keyResource.Header = new HeaderResource();

            ResourceConverterHelper.Convert<Header, HeaderResource>(_genericStructConverter, header, keyResource.Header);

            return keyResource;
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
    }
}
