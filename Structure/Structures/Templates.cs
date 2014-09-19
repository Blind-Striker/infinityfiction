using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeFiction.InfinityFiction.Structure.Structures
{
	public static class Templates
	{
	    public const string KeyStructTemplate = @"
			using System.Runtime.InteropServices;

			namespace CodeFiction.InfinityFiction.Structure.Structures.Key
			{{
				[StructLayout(LayoutKind.Explicit)]
				public struct KeyStruct
				{{
					[FieldOffset(0)] 
					public Header Header;

					[FieldOffset({0}), MarshalAs(UnmanagedType.ByValArray, SizeConst = {2})]
					public BiffEntry[] BiffEntries;

					[FieldOffset({1}), MarshalAs(UnmanagedType.ByValArray, SizeConst = {3})]
					public ResourceEntry[] ResourceEntries;
				}}
			}}";
	}
}
