#pragma once
#include <string>
#include "ChitinKeyManager.h"

using namespace std;

#ifdef INFINITY_STRUCT_READER_EXPORTS
#define INFINITY_STRUCT_READER_API __declspec(dllexport) 
#else
#define INFINITY_STRUCT_READER_API __declspec(dllimport) 
#endif

extern "C" INFINITY_STRUCT_READER_API bool __cdecl ReadKeyFile(const char* filePath, KeyFile& pKeyFile);