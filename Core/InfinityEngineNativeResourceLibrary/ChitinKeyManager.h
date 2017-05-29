#pragma once
#include <string>
#include "Structs.h"

struct KeyFile;
using std::string;

namespace InfinityEngineNativeResourceLibrary
{
	class ChitinKeyManager
	{
	public:
		ChitinKeyManager();

		static bool ReadKeyFile(const char* filePath, KeyFile &pKeyFile);

		~ChitinKeyManager();
	};
}