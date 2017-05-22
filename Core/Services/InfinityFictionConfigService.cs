using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using CodeFiction.DarkMatterFramework.Libraries.ConfigLibrary.IniManager;
using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.ServiceContracts;

namespace CodeFiction.InfinityFiction.Core.Services
{
    public class InfinityFictionConfigService : IInfinityFictionConfigService
    {
        private const string DialogFilename = "dialog.tlk";
        private const string OverrideFolder = "Override";

        private readonly IKeyResourceService _keyResourceService;
        private readonly string[] _bgdirs =
            {
                "Characters", "MPSave", "Music", "Portraits", "Save", "Screenshots",
                "Scripts", "ScrnShot", "Sounds", "Temp", "TempSave"
            };

        private readonly Dictionary<GameEnum, GameConfig> _games;
        private List<string> _extraDirs;
        private IList<ResourceFile> _resourceFiles;
        private List<string> _biffDirs;

        private readonly string _bgeeHomePath;
        private string _rootPath;
        private string _keyFilePath;
        private string _dialogFilePath;
        private GameEnum _gameEnum;
        private KeyResource _keyResource;

        public InfinityFictionConfigService(IKeyResourceService keyResourceService)
        {
            _keyResourceService = keyResourceService;
            _games = new Dictionary<GameEnum, GameConfig>(16);

            string homePath = Environment.ExpandEnvironmentVariables("%HOMEDRIVE%%HOMEPATH%");
            _bgeeHomePath = Path.Combine(homePath, @"Documents\Baldur's Gate - Enhanced Edition");

            BuildGames();
        }

        public KeyResource KeyResource
        {
            get
            {
                return _keyResource;
            }
        }

        public IList<ResourceFile> ResourceFiles
        {
            get
            {
                return _resourceFiles;
            }
        }

        public void InitializeConfiguration(string keyFilePath)
        {
            _extraDirs = new List<string>();
            _resourceFiles = new List<ResourceFile>();
            _biffDirs = new List<string>();
            _keyFilePath = Path.GetFullPath(keyFilePath);

            bool exists = File.Exists(keyFilePath);
            if (!exists)
            {
                // TODO : exception fırlat;
            }

            _rootPath = Path.GetDirectoryName(_keyFilePath);

            FindGameType();
            LoadResources();
            ReCheckGameType();
            SetBiffDirectories();
        }

        private void BuildGames()
        {
            _games.Add(GameEnum.Unknown, new GameConfig(GameEnum.Unknown, "Unknown game", "baldur.ini", _bgdirs));
            _games.Add(GameEnum.BaldursGate, new GameConfig(GameEnum.BaldursGate, "Baldur's Gate", "baldur.ini", _bgdirs));
            _games.Add(GameEnum.BaldursGateTotsc, new GameConfig(GameEnum.BaldursGateTotsc, "Baldur's Gate - Tales of the Sword Coast", "baldur.ini", _bgdirs));
            _games.Add(GameEnum.BaldursGateExtented, new GameConfig(
                GameEnum.BaldursGateExtented, 
                "Baldur's Gate Extended Edition", 
                null, 
                new[] { "Movies", "Music", "Scripts", "Sounds", "$HOME$Characters", "$HOME$MPSave", "$HOME$Portraits", "$HOME$Save", "$HOME$ScrnShot", "$HOME$Temp", "$HOME$TempSave" }));
            _games.Add(GameEnum.PlanescapeTorment, new GameConfig(GameEnum.PlanescapeTorment, "Planescape: Torment", "torment.ini", new[] { "Music", "Save", "Temp" }));
            _games.Add(GameEnum.IceWindDale, new GameConfig(GameEnum.IceWindDale, "Icewind Dale", "icewind.ini", _bgdirs));
            _games.Add(GameEnum.IceWindDaleHeartofWinter, new GameConfig(GameEnum.IceWindDaleHeartofWinter, "Icewind Dale - Heart of Winter", "icewind.ini", _bgdirs));
            _games.Add(GameEnum.IceWindDaleTrailsOfRuleMaster, new GameConfig(GameEnum.IceWindDaleTrailsOfRuleMaster, "Icewind Dale - Trials of the Luremaster", "icewind.ini", _bgdirs));
            _games.Add(GameEnum.BaldursGate2, new GameConfig(GameEnum.BaldursGate2, "Baldur's Gate 2 - Shadows of Amn", "baldur.ini", _bgdirs));
            _games.Add(GameEnum.BaldursGateTob, new GameConfig(GameEnum.BaldursGateTob, "Baldur's Gate 2 - Throne of Bhaal", "baldur.ini", _bgdirs));
            _games.Add(GameEnum.IceWindDale2, new GameConfig(GameEnum.IceWindDale2, "Icewind Dale 2", "icewind2.ini", _bgdirs));
            _games.Add(GameEnum.NewerwinterNights, new GameConfig(
                GameEnum.NewerwinterNights, 
                "Neverwinter Nights", 
                "nwn.ini", 
                new[] { "Ambient", "DMVault", "Hak", "LocalVault", "Modules", "Music", "NWM", "Saves", "ServerVault", "Source", "TexturePacks" }));

            _games.Add(GameEnum.Kotor, new GameConfig(
                GameEnum.Kotor, 
                "Star Wars: Knights of the Old Republic", 
                "swkotor.ini", 
                new[] { "Lips", "Modules", "Rims", "Saves", "StreamMusic", "StreamSounds", "TexturePacks" }));

            _games.Add(GameEnum.BaldursGateTutu, new GameConfig(GameEnum.BaldursGateTutu, "BG1Tutu", "baldur.ini", _bgdirs));
            _games.Add(GameEnum.BaldursGateDemo, new GameConfig(GameEnum.BaldursGateDemo, "Baldur's Gate - Non-Interactive Demo", "chitin.ini", new[] { "Music" }));

            _games.Add(GameEnum.Kotor2, new GameConfig(
                GameEnum.Kotor2, 
                "Star Wars: Knights of the Old Republic 2", 
                "swkotor2.ini", 
                new[] { "Lips", "Modules", "Rims", "Saves", "StreamMusic", "StreamSounds", "TexturePacks" }));
        }

        private void FindGameType()
        {
            _gameEnum = GameEnum.Unknown;

            if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "Config.exe")))
            {
                _gameEnum = GameEnum.BaldursGate;
            }
            else if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "BGConfig.exe")))
            {
                _gameEnum = GameEnum.BaldursGate2;
            }
            else if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "movies/TSRLOGO.wbm")))
            {
                _gameEnum = GameEnum.BaldursGateTob;
            }
            else if (File.Exists(Path.Combine(_rootPath, "torment.exe")))
            {
                _gameEnum = GameEnum.PlanescapeTorment;
            }
            else if (File.Exists(Path.Combine(_rootPath, "idmain.exe")))
            {
                _gameEnum = GameEnum.IceWindDale;
            }
            else if (File.Exists(Path.Combine(_rootPath, "iwd2.exe")))
            {
                _gameEnum = GameEnum.IceWindDale2;
            }
            else if (File.Exists(Path.Combine(_rootPath, "nwn.exe")) || File.Exists(Path.Combine(_rootPath, "Neverwinter Nights.app/Contents/MacOS/Neverwinter Nights")))
            {
                _gameEnum = GameEnum.NewerwinterNights;
            }
            else if (File.Exists(Path.Combine(_rootPath, "swkotor.exe")))
            {
                _gameEnum = GameEnum.Kotor;
            }
            else if (File.Exists(Path.Combine(_rootPath, "swkotor2.exe")))
            {
                _gameEnum = GameEnum.Kotor2;
            }
            else if (File.Exists(Path.Combine(_rootPath, "bg1tutu.exe")))
            {
                _gameEnum = GameEnum.BaldursGateTutu;
            }
            else if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "chitin.ini")))
            {
                _gameEnum = GameEnum.BaldursGateDemo;
            }
            else if (File.Exists(Path.Combine(_rootPath, "Baldur.exe")) && File.Exists(Path.Combine(_rootPath, "data/OBJANIM.BIF")))
            {
                _gameEnum = GameEnum.BaldursGateExtented;
            }
        }

        private void LoadResources()
        {
            GameConfig game = _games[_gameEnum];
            _dialogFilePath = Path.Combine(_rootPath, DialogFilename);
            bool dialogfileExists = File.Exists(_dialogFilePath);
            if (!dialogfileExists)
            {
                _dialogFilePath = Path.Combine(_rootPath, "lang", "en_US", DialogFilename);
            }

            _keyResource = _keyResourceService.GetKeyResource(_gameEnum, _keyFilePath);

            foreach (var resourceEntryResource in _keyResource.ResourceEntries)
            {
                var resourceFile = new ResourceFile();
                resourceFile.Extension = resourceEntryResource.Extension;
                resourceFile.Folder = resourceEntryResource.Extension;
                resourceFile.RootFolder = resourceEntryResource.Extension;
                resourceFile.File = resourceEntryResource.FileName;
                resourceFile.ResourceEntry = true;
                _resourceFiles.Add(resourceFile);
            }

            List<string> currentExtraDirs = game.ExtraDirs.Select(GetFullPath).ToList();

            if (currentExtraDirs.IsNullOrEmpty())
            {
                // TODO: throw Exception
            }

            foreach (var extraDir in currentExtraDirs)
            {
                if (Directory.Exists(extraDir))
                {
                    _extraDirs.Add(extraDir);
                    LoadResourceFiles(extraDir, currentExtraDirs);
                }
            }

            string overrideFullPath = Path.Combine(_rootPath, OverrideFolder);
            if (Directory.Exists(overrideFullPath))
            {
                string[] overrideFiles = Directory.GetFiles(overrideFullPath);

                foreach (var overrideFile in overrideFiles)
                {
                    string fileName = Path.GetFileName(overrideFile);
                    string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(overrideFile);
                    string extension = Path.GetExtension(overrideFile);
                    ResourceFile resourceFile = _resourceFiles.FirstOrDefault(file => file.File == fileNameWithoutExtension);

                    string overrideFolder = OverrideFolder.Split(Path.DirectorySeparatorChar).Last();

                    if (resourceFile == null)
                    {
                        resourceFile = new ResourceFile();
                        resourceFile.File = fileName;
                        resourceFile.FullPath = Path.Combine(OverrideFolder, overrideFile);
                        resourceFile.RootFolder = overrideFolder;
                        resourceFile.Folder = overrideFolder;
                        resourceFile.Extension = string.IsNullOrEmpty(extension)
                             ? string.Empty
                             : extension.ToUpper().Replace(".", string.Empty);

                        _resourceFiles.Add(resourceFile);
                    }
                    else
                    {
                        ResourceEntryResource resourceEntryResource = _keyResource.ResourceEntries.FirstOrDefault(resource => resource.FileName == fileName);
                        if (resourceEntryResource != null)
                        {
                            resourceEntryResource.HasOverride = true;
                        }
                    }
                }
            }
        }

        private string GetFullPath(string extraDir)
        {
            string fullPath;
            if (_gameEnum == GameEnum.BaldursGateExtented&& new[] { "save", "temp", "tempsave" }.Any(s => s.Equals(extraDir, StringComparison.InvariantCultureIgnoreCase)))
            {
                string documentsPath = Path.Combine(Environment.ExpandEnvironmentVariables("%userprofile%"), "Documents");
                string alternatePath = Path.Combine(documentsPath, "Baldur's Gate - Enhanced Edition");
                fullPath = Path.Combine(alternatePath, extraDir);

            }
            if (extraDir.StartsWith("$HOME$"))
            {
                string extraDriveHome = extraDir.Replace("$HOME$", string.Empty);
                fullPath = Path.Combine(_bgeeHomePath, extraDriveHome);
            }
            else
            {
                fullPath = Path.Combine(_rootPath, extraDir);
            }
            return fullPath;
        }

        private void LoadResourceFiles(string directory, ICollection<string> extraDirectories)
        {
            string[] filePaths = Directory.GetFiles(directory);
            string[] directoryPaths = Directory.GetDirectories(directory);

            var filesAndDirectories = new List<string>();
            filesAndDirectories.AddRange(filePaths);
            filesAndDirectories.AddRange(directoryPaths);

            string parentFolder = GetParentOfExtraSubDirectory(extraDirectories, directory);

            foreach (var file in filesAndDirectories)
            {
                FileAttributes attr = File.GetAttributes(file);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    LoadResourceFiles(file, extraDirectories);
                    continue;
                }

                var resourceFile = new ResourceFile();
                string fileName = Path.GetFileName(file);
                string extension = Path.GetExtension(file);

                if (string.IsNullOrEmpty(fileName))
                {
                    // TODO : throw Exception
                }

                resourceFile.RootFolder = parentFolder;
                resourceFile.File = fileName.ToUpper();
                resourceFile.FullPath = file;
                resourceFile.Folder = directory.Split(Path.DirectorySeparatorChar).Last();
                resourceFile.Extension = string.IsNullOrEmpty(extension)
                                             ? string.Empty
                                             : extension.ToUpper().Replace(".", string.Empty);
                
                _resourceFiles.Add(resourceFile);
            }
        }

        private bool ResourceExists(string resourceName)
        {
           return _resourceFiles.Any(file => file.File == resourceName);
        }

        private void ReCheckGameType()
        {
            if (_gameEnum == GameEnum.IceWindDale && ResourceExists("HOWDRAG.MVE"))
            {
                // Detect Trials of the Luremaster
                _gameEnum = ResourceExists("AR9715.ARE")
                                ? GameEnum.IceWindDaleTrailsOfRuleMaster
                                : GameEnum.IceWindDaleHeartofWinter;
            }

            if (_gameEnum == GameEnum.BaldursGate2 && ResourceExists("SARADUSH.MVE"))
            {
                _gameEnum = GameEnum.BaldursGateTob;
            }

            if (_gameEnum == GameEnum.BaldursGate && ResourceExists("DURLAG.MVE"))
            {
                _gameEnum = GameEnum.BaldursGateTotsc;
            }
        }

        private void SetBiffDirectories()
        {
            GameConfig game = _games[_gameEnum];

            if (game == null || game.IniFile.IsNullOrEmpty())
            {
                return;
            }

            var reader = new IniFileReader();

            string iniPath = Path.Combine(_rootPath, game.IniFile);
            IniFile iniFile = reader.GetIniFile(iniPath);

            foreach (var iniCategory in iniFile.IniCategories)
            {
                foreach (var iniKey in iniCategory.Keys)
                {
                    string value = iniKey.Value;

                    string[] directories = value.Split(';');

                    foreach (var directory in directories)
                    {
                        if (Directory.Exists(directory) && !_biffDirs.Contains(directory))
                        {
                            _biffDirs.Add(directory);
                        }   
                    }
                }
            }
        }

        private static string GetParentOfExtraSubDirectory(ICollection<string> extraDirectoriesPaths, string currentDirectoryPath)
        {
            if (extraDirectoriesPaths.Any(s => s == currentDirectoryPath))
            {
                return currentDirectoryPath.Split(Path.DirectorySeparatorChar).Last();
            }

            foreach (var extraDirectoryPath in extraDirectoriesPaths)
            {
                DirectoryInfo extraDirectory = new DirectoryInfo(extraDirectoryPath);
                DirectoryInfo currentDirectory = new DirectoryInfo(currentDirectoryPath);

                while (currentDirectory.Parent != null)
                {
                    if (currentDirectory.Parent.FullName == extraDirectory.FullName)
                    {
                        return extraDirectory.FullName.Split(Path.DirectorySeparatorChar).Last();
                    }
                    
                    currentDirectory = currentDirectory.Parent;
                }
            }

            return null;
        }
    }
}
