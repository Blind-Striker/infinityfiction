﻿using System;
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

        public ResourceConverter(IGenericStructConverter genericStructConverter, IDelegateHelper delegateHelper)
        {
            _genericStructConverter = genericStructConverter;
            _delegateHelper = delegateHelper;
        }

        public void Convert<TStruct, TResource>(TStruct @struct, TResource resource) where TResource : BaseModel
        {
            FieldInfo[] fieldInfos = typeof(TStruct).GetFields();

            Convert(@struct, resource, fieldInfos);
        }

        public void Convert<TStruct, TResource>(TStruct @struct, TResource resource, FieldInfo[] fieldInfos) where TResource : BaseModel
        {
            foreach (FieldInfo fieldInfo in fieldInfos)
            {
                Delegate getter = _delegateHelper.CreateGetter(fieldInfo);
                object value = getter.DynamicInvoke(@struct);
                string fieldName = fieldInfo.Name;

                resource.Properties.Add(fieldName, new SimpleDataType { Name = fieldName, Value = value, Type = fieldInfo.FieldType });
            }
        }

        //public void Convert<TStruct, TResource>(byte[] content, TResource[] resources, uint offset, uint sizeofStruct, Action<TResource> onResourceConverted = null)
        //    where TStruct : struct
        //    where TResource : BaseModel, new()
        //{
        //    if (content == null)
        //    {
        //        throw new ArgumentNullException("content");
        //    }

        //    if (resources == null)
        //    {
        //        throw new ArgumentNullException("resources");
        //    }

        //    for (int i = 0; i < resources.Length; i++)
        //    {
        //        byte[] biffEntryContent = BinaryHelper.GetBytes(content, offset, sizeofStruct);

        //        var biffEntry = _genericStructConverter.ConvertToStruct<TStruct>(biffEntryContent, 0);

        //        var resource = new TResource();

        //        Convert(biffEntry, resource);

        //        if (onResourceConverted != null)
        //        {
        //            onResourceConverted(resource);
        //        }

        //        resources[i] = resource;

        //        offset += sizeofStruct;
        //    }
        //}

        public void Convert<TStruct, TResource>(byte[] content, TResource[] resources, uint offset, uint sizeofStruct, Action<TResource> onResourceConverted = null)
            where TStruct : struct
            where TResource : BaseModel, new()
        {
            if (content == null)
            {
                throw new ArgumentNullException("content");
            }

            if (resources == null)
            {
                throw new ArgumentNullException("resources");
            }

            var entries = new TStruct[resources.Length];

            for (int i = 0; i < resources.Length; i++)
            {
                byte[] entryContent = BinaryHelper.GetBytes(content, offset, sizeofStruct);

                var entry = _genericStructConverter.ConvertToStruct<TStruct>(entryContent, 0);
                entries[i] = entry;

                offset += sizeofStruct;
            }

            FieldInfo[] fieldInfos = typeof(TStruct).GetFields();

            for (int i = 0; i < entries.Length; i++)
            {
                var entry = entries[i];
                var resource = new TResource();

                Convert(entry, resource, fieldInfos);

                if (onResourceConverted != null)
                {
                    onResourceConverted(resource);
                }

                resources[i] = resource;
            }
        }
    }
}
