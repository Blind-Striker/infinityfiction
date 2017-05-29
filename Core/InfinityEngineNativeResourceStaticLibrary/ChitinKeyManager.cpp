#include "stdafx.h"
#include <fstream>
#include "ChitinKeyManager.h"
#include <memory>

using namespace std;

namespace InfinityEngineNativeResourceStaticLibrary
{
	ChitinKeyManager::ChitinKeyManager()
	{
	}


	ChitinKeyManager::~ChitinKeyManager()
	{
	}

	bool ChitinKeyManager::ReadKeyFile(const char* filePath, KeyFile& pKeyFile)
	{
		KeyHeader header;

		ifstream keyFileReader;

		keyFileReader.open(filePath, ios::out | ios::app | ios::binary);

		printf("%s\n", filePath);

		if (!keyFileReader.read(reinterpret_cast<char*>(&header), sizeof(KeyHeader)))
		{
			return false;
		}

		BiffEntry *biffEntries = new BiffEntry[header.BiffEntriesCount];
		ResourceEntry *resourceEntries = new ResourceEntry[header.ResourceEntriesCount];
		BiffInfo *biffInfos = new BiffInfo[header.BiffEntriesCount];

		if (!keyFileReader.seekg(header.BiffEntriesOffset, ios::beg))
		{
			return false;
		}

		if (!keyFileReader.read(reinterpret_cast<char*>(biffEntries), sizeof(BiffEntry) * header.BiffEntriesCount))
		{
			return false;
		}

		if (!keyFileReader.seekg(header.ResourceEntriesOffset, ios::beg))
		{
			return false;
		}

		if (!keyFileReader.read(reinterpret_cast<char*>(resourceEntries), sizeof(ResourceEntry) * header.ResourceEntriesCount))
		{
			return false;
		}

		for (int i = 0; i < header.BiffEntriesCount; i++)
		{
			if (!keyFileReader.seekg(biffEntries[i].BIFFileNameStartOffset, ios::beg))
			{
				return false;
			}

			char* szPath = new char[biffEntries[i].BIFFileNameLength];

			if (!keyFileReader.read(szPath, biffEntries[i].BIFFileNameLength))
			{
				return false;
			}

			biffInfos[i].FileName = szPath;
			biffInfos[i].Location = biffEntries[i].FileLocationMark;
		}

		pKeyFile.KeyHeader = header;
		pKeyFile.BiffEntries = biffEntries;
		pKeyFile.ResourceEntries = resourceEntries;
		pKeyFile.BiffInfos = biffInfos;

		keyFileReader.close();

		return true;
	}
}
