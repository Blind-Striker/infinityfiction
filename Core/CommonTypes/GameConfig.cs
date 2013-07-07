using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeFiction.InfinityFiction.Core.CommonTypes
{
    public class GameConfig
    {
        private readonly GameEnum _gameEnum;
        private readonly string _name;
        private string _inifile;
        private string[] _extraDirs;

        public GameConfig(GameEnum gameEnum, string name, string inifile, string[] extraDirs)
        {
            _gameEnum = gameEnum;
            _name = name;
            _inifile = inifile;
            _extraDirs = extraDirs;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
