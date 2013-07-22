namespace CodeFiction.InfinityFiction.Core.CommonTypes
{
    public class GameConfig
    {
        private readonly GameEnum _gameEnum;
        private readonly string _name;
        private readonly string _inifile;
        private readonly string[] _extraDirs;

        public GameConfig(GameEnum gameEnum, string name, string inifile, string[] extraDirs)
        {
            _gameEnum = gameEnum;
            _name = name;
            _inifile = inifile;
            _extraDirs = extraDirs;
        }

        public GameEnum GameEnum
        {
            get
            {
                return _gameEnum;
            }
        }

        public string Name
        {
            get
            {
                return _name;
            }
        }

        public string IniFile
        {
            get
            {
                return _inifile;
            }
        }

        public string[] ExtraDirs
        {
            get
            {
                return _extraDirs;
            }
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
