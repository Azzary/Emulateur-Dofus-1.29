using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Spell
{
    public class Spell
    {
        public int id;
        public int level;
        public int pos;

        public Spell(int _id, int _level)
        {
            id = _id;
            level = _level;
        }

    }
}
