using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CodeFiction.InfinityFiction.Core.CommonTypes;
using CodeFiction.InfinityFiction.Core.ResourceBuilderContracts;
using CodeFiction.InfinityFiction.Core.Resources.Key;
using CodeFiction.InfinityFiction.Core.ServiceContracts;

namespace CodeFiction.InfinityFiction.Core.Services
{
    public class InfinityFictionConfigService : IInfinityFictionConfigService
    {
        private readonly IKeyResourceBuilder _keyResourceBuilder;

        private string _rootPath;
        private readonly string[] _bgdirs =
            {
                "Characters", "MPSave", "Music", "Portraits", "Save", "Screenshots",
                "Scripts", "ScrnShot", "Sounds", "Temp", "TempSave"
            };

        private string _keyFilePath;
        private string _dialogFilePath;
        private const string DialogFilename = "dialog.tlk";
        private const string OverrideFolder = "Override";
        private readonly GameConfig[] _games;
        private GameEnum _gameEnum;
        private readonly List<string> _extraDirs;
        private readonly List<ResourceFile> _resourceFiles;

        private KeyResource _keyResource;

        public InfinityFictionConfigService(IKeyResourceBuilder keyResourceBuilder)
        {
            _keyResourceBuilder = keyResourceBuilder;
            _extraDirs= new List<string>();
            _resourceFiles = new List<ResourceFile>();
            _games = new GameConfig[16];
            BuildGames();
        }

        public KeyResource KeyResource
        {
            get
            {
                return _keyResource;
            }
        }

        public GameConfig BuildResourceFile(string keyFilePath)
        {
            _keyFilePath = Path.GetFullPath(keyFilePath);
            bool exists = File.Exists(keyFilePath);
            if (!exists)
            {
                // TODO : exception fırlat;
            }

            _rootPath = Path.GetDirectoryName(_keyFilePath);
            _gameEnum = GetGameEnum();
           // GameConfig gameConfig = GetGameConfig(_gameEnum);
//            ResourceFile file = new ResourceFile { GameConfig = gameConfig, GameEnum = _gameEnum, RootPath = RootPath };

            return null;
        }

        private void BuildGames()
        {
            _games[0] = new GameConfig(GameEnum.Unknown, "Unknown game", "baldur.ini", _bgdirs);
            _games[1] = new GameConfig(GameEnum.BaldursGate, "Baldur's Gate", "baldur.ini", _bgdirs);
            _games[2] = new GameConfig(GameEnum.BaldursGateTotsc, "Baldur's Gate - Tales of the Sword Coast", "baldur.ini", _bgdirs);
            _games[4] = new GameConfig(GameEnum.PlanescapeTorment, "Planescape: Torment", "torment.ini", new[] {"Music", "Save", "Temp"});
            _games[5] = new GameConfig(GameEnum.IceWindDale, "Icewind Dale", "icewind.ini", _bgdirs);
            _games[6] = new GameConfig(GameEnum.IceWindDaleHeartofWinter, "Icewind Dale - Heart of Winter", "icewind.ini", _bgdirs);
            _games[7] = new GameConfig(GameEnum.IceWindDaleTrailsOfRuleMaster, "Icewind Dale - Trials of the Luremaster", "icewind.ini", _bgdirs);
            _games[8] = new GameConfig(GameEnum.BaldursGate2, "Baldur's Gate 2 - Shadows of Amn", "baldur.ini", _bgdirs);
            _games[9] = new GameConfig(GameEnum.BaldursGateTob, "Baldur's Gate 2 - Throne of Bhaal", "baldur.ini", _bgdirs);
            _games[10] = new GameConfig(GameEnum.IceWindDale2, "Icewind Dale 2", "icewind2.ini", _bgdirs);
            _games[11] = new GameConfig(GameEnum.NewerwinterNights, "Neverwinter Nights", "nwn.ini",
                                        new[]
                                            {
                                                "Ambient", "DMVault", "Hak", "LocalVault", "Modules", "Music", "NWM",
                                                "Saves", "ServerVault", "Source", "TexturePacks"
                                            });
            _games[12] = new GameConfig(GameEnum.Kotor, "Star Wars: Knights of the Old Republic", "swkotor.ini",
                                        new[]
                                            {
                                                "Lips", "Modules", "Rims", "Saves", "StreamMusic", "StreamSounds",
                                                "TexturePacks"
                                            });
            _games[13] = new GameConfig(GameEnum.BaldursGateTutu, "BG1Tutu", "baldur.ini", _bgdirs);
            _games[14] = new GameConfig(GameEnum.BaldursGateDemo, "Baldur's Gate - Non-Interactive Demo", "chitin.ini", new[] {"Music"});
            _games[15] = new GameConfig(GameEnum.Kotor2, "Star Wars: Knights of the Old Republic 2", "swkotor2.ini",
                                        new[]
                                            {
                                                "Lips", "Modules", "Rims", "Saves", "StreamMusic",
                                                "StreamSounds", "TexturePacks"
                                            });
            _games[16] = new GameConfig(GameEnum.BaldursGateExtented, "Baldur's Gate Extended Edition", "Baldur.ini", _bgdirs);
        }

        private GameEnum GetGameEnum()
        {
            GameEnum gameEnum = GameEnum.Unknown;

            if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "Config.exe")))
            {
                gameEnum = GameEnum.BaldursGate;
            }
            else if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "BGConfig.exe")))
            {
                gameEnum = GameEnum.BaldursGate2;
            }
            else if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "movies/TSRLOGO.wbm")))
            {
                gameEnum = GameEnum.BaldursGateTob;
            }
            else if (File.Exists(Path.Combine(_rootPath, "torment.exe")))
            {
                gameEnum = GameEnum.PlanescapeTorment;
            }
            else if (File.Exists(Path.Combine(_rootPath, "idmain.exe")))
            {
                gameEnum = GameEnum.IceWindDale;
            }
            else if (File.Exists(Path.Combine(_rootPath, "iwd2.exe")))
            {
                gameEnum = GameEnum.IceWindDale2;
            }
            else if (File.Exists(Path.Combine(_rootPath, "nwn.exe")) || File.Exists(Path.Combine(_rootPath, "Neverwinter Nights.app/Contents/MacOS/Neverwinter Nights")))
            {
                gameEnum = GameEnum.NewerwinterNights;
            }
            else if (File.Exists(Path.Combine(_rootPath, "swkotor.exe")))
            {
                gameEnum = GameEnum.Kotor;
            }
            else if (File.Exists(Path.Combine(_rootPath, "swkotor2.exe")))
            {
                gameEnum = GameEnum.Kotor2;
            }
            else if (File.Exists(Path.Combine(_rootPath, "bg1tutu.exe")))
            {
                gameEnum = GameEnum.BaldursGateTutu;
            }
            else if (File.Exists(Path.Combine(_rootPath, "baldur.exe")) && File.Exists(Path.Combine(_rootPath, "chitin.ini")))
            {
                gameEnum = GameEnum.BaldursGateDemo;
            }

            LoadResources();

            return gameEnum;
        }

        private void LoadResources()
        {
            _keyResource = _keyResourceBuilder.GetKeyResource(_gameEnum, _keyFilePath);

            string[] currentExtraDirs = _games.Where(config => config.GameEnum == _gameEnum).Select(config => config.ExtraDirs).FirstOrDefault();

            if (currentExtraDirs == null)
            {
                //TODO: throw Exception
            }

            foreach (var extraDir in currentExtraDirs)
            {
                string fullPath = Path.Combine(_rootPath, extraDir);
                if (Directory.Exists(fullPath))
                {
                    _extraDirs.Add(fullPath);
                    LoadResourceFiles(fullPath);
                }
            }
        }

        private void LoadResourceFiles(string directory)
        {
            string[] filePaths = Directory.GetFiles(directory);
            string[] directoryPaths = Directory.GetDirectories(directory);

            List<string> filesAndDirectories = new List<string>();
            filesAndDirectories.AddRange(filePaths);
            filesAndDirectories.AddRange(directoryPaths);

            foreach (var file in filesAndDirectories)
            {
                FileAttributes attr = File.GetAttributes(file);

                if ((attr & FileAttributes.Directory) == FileAttributes.Directory)
                {
                    LoadResourceFiles(file);
                    continue;
                }

                ResourceFile resourceFile = new ResourceFile();
                string fileName = Path.GetFileNameWithoutExtension(file);
                resourceFile.File = fileName;
                resourceFile.FullPath = file;
                resourceFile.Folder = directory;
                
                _resourceFiles.Add(resourceFile);
            }
        }
    }
}
