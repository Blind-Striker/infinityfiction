#include "stdafx.h"
#include "Imports.h"

using namespace InfinityEngineNativeResourceStaticLibrary;

extern "C" INFINITY_STRUCT_READER_API bool __cdecl ReadKeyFile(const char* filePath, KeyFile& pKeyFile)
{
	return ChitinKeyManager::ReadKeyFile(filePath, pKeyFile);
}
