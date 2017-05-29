// NativeResourceLibSandbox.cpp : Defines the entry point for the console application.
//

#include "stdafx.h"
#include "../../../Core/InfinityEngineNativeResourceStaticLibrary/ChitinKeyManager.h"

using namespace InfinityEngineNativeResourceStaticLibrary;

int main()
{
	KeyFile key_file;

	char* filePath = "D:\\Beamdog Library\\00783\\chitin.key";

	bool isRead = ChitinKeyManager::ReadKeyFile(filePath, key_file);

    return 0;
}

