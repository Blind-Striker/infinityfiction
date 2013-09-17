using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using CodeFiction.DarkMatterFramework.Libraries.IOLibrary;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources;
using CodeFiction.InfinityFiction.Core.Resources.DataTypes;
using CodeFiction.InfinityFiction.Structure.StructConverterContracts;

namespace CodeFiction.InfinityFiction.Core.ResourceBuilder
{
    public class ResourceConverter : IResourceConverter
    {
        private readonly IGenericStructConverter _genericStructConverter;

        private readonly IDelegateHelper _delegateHelper;

        private static readonly Dictionary<Type, List<FieldInfo>> FileDictionary = new Dictionary<Type, List<FieldInfo>>();

        public ResourceConverter(IGenericStructConverter genericStructConverter, IDelegateHelper delegateHelper)
        {
            _genericStructConverter = genericStructConverter;
            _delegateHelper = delegateHelper;
        }

        public void Convert<TStruct, TResource>(TStruct @struct, TResource resource) where TResource : BaseModel
        {
            FieldInfo[] fieldInfos = typeof(TStruct).GetFields();
            // List<FieldInfo> fieldInfos = GetCachedProperties(typeof(TStruct));

            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Delegate getter = _delegateHelper.CreateGetter(fieldInfo);
                object value = getter.DynamicInvoke(@struct);

                resource.Properties.Add(new SimpleDataType { Name = fieldInfo.Name, Value = value, Type = fieldInfo.FieldType });
            }
        }

        public void Convert<TStruct, TResource>(byte[] content, TResource[] resources, uint offset, uint sizeofStruct, Action<TResource> onResourceConverted = null)
            where TStruct : struct 
            where TResource : BaseModel, new()
        {
            if (resources == null)
            {
                throw new ArgumentNullException("content");
            }
            for (int i = 0; i < resources.Length; i++)
            {
                byte[] biffEntryContent = BinaryHelper.GetBytes(content, offset, sizeofStruct);

                var biffEntry = _genericStructConverter.ConvertToStruct<TStruct>(biffEntryContent, 0);

                resources[i] = new TResource();

                Convert(biffEntry, resources[i]);

                if (onResourceConverted != null)
                {
                    onResourceConverted(resources[i]);
                }

                offset += sizeofStruct;
            }
        }

        //private static List<FieldInfo> GetCachedProperties(Type type)
        //{
        //    List<FieldInfo> fields;
        //    if (FileDictionary.TryGetValue(type, out fields) == false)
        //    {
        //        fields = type.GetFields().ToList();
        //        FileDictionary.Add(type, fields);
        //    }

        //    return fields;
        //}
    }
}
