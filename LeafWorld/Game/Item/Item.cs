using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Item
{
    public class Item
    {
        public int ID { get; set; }
        
        public int Niveau { get; set; }

        public int Pods { get; set; }

        public int Type { get; set; }

        public string Stats { get; set; }
        public string Condition { get; set; }
        public string infosWeapon { get; set; }


      
        public Item(int _ID, int _pods, int _Niveau, int _Type, string _ModelStat, string _condition, string _infosWeapon)
        {
            ID = _ID;
            Type = _Type;
            Pods = _pods;
            Niveau = _Niveau;
            Stats = _ModelStat;
            Condition = _condition;
            infosWeapon = _infosWeapon;
        }

    }
}
