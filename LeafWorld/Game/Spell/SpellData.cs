using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Spell
{
    public class SpellData
    {
        public int id { get; set; }
        public string level1 { get; set; }
        public string level2 { get; set; }
        public string level3 { get; set; }
        public string level4 { get; set; }
        public string level5 { get; set; }
        public string level6 { get; set; }

        public SpellData(int _id, string _level1, string _level2, string _level3, string _level4, string _level5, string _level6)
        {
            id = _id;
            level1 = _level1;
            level2 = _level2;
            level3 = _level3;
            level4 = _level4;
            level5 = _level5;
            level6 = _level6;
        }
    }
}
