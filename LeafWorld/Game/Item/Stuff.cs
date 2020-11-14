using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Item
{
    public class Stuff : Item
    {
        public int Position { get; set; }
        public readonly int UID;
        public Stuff(int _ID, int _UID, int Niveau, int Type, int _position, string _Stats, string _condition, string _infosWeapon) : base(_ID, _UID, Niveau, Type, _Stats, _condition, _infosWeapon)
        {
            Position = _position;
            UID = _UID;
        }
    }
    
}
