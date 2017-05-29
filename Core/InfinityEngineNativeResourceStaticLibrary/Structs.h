#pragma once

#include <Windows.h>

#pragma pack(push)
#pragma pack(1)

struct KeyHeader
{
	char Signature[4];
	char Version[4];
	DWORD BiffEntriesCount;
	DWORD ResourceEntriesCount;
	DWORD BiffEntriesOffset;
	DWORD ResourceEntriesOffset;
};

struct BiffEntry
{
	DWORD BiffFileLength;
	DWORD BIFFileNameStartOffset;
	WORD BIFFileNameLength;
	WORD FileLocationMark;
};

struct ResourceEntry
{
	char ResourceName[8];
	WORD ResourceType;
	DWORD ResourceLocator;
};

struct BiffInfo
{
	char* FileName;
	SHORT Location;
};

#pragma pack(pop)

struct	KeyFile
{
	KeyHeader KeyHeader;
	BiffEntry* BiffEntries;
	ResourceEntry* ResourceEntries;
	BiffInfo* BiffInfos;
};
