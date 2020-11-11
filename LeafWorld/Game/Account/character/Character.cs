using LeafWorld.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.account.character
{
    public class Character
    {
        public List<Spell.Spell> SpellsCharacter = new List<Spell.Spell>();

        public Fight fight;
        public bool IsAvailable { get; set; }
        
        public readonly int id;

        public readonly string speudo;
        public int NewCell { get; set; }
        public List<int> ListCellMove { get; set; }
        public int sexe { get; set; }
        public Map.Map Map { get; set; }
        public bool newCharac = false;
        public int level { get; set; }
        public int PO { get; set; }
        public int invo { get; set; }
        public int isDead { get; set; }
        public int mapID { get; set; }
        public int gfxID { get; set; }
        public int cellID { get; set; }
        public int subArea { get; set; }
        public int couleur1 { get; set; }
        public int couleur2 { get; set; }
        public int couleur3 { get; set; }
        public int pods { get; set; }
        public int podsMax { get; set; }
        public int XP { get; set; }
        public int kamas { get; set; }
        public int capital { get; set; }
        public int PSorts { get; set; }
        public int vie { get; set; }
        public int vieTotal { get; set; }
        public int energie { get; set; }
        public int PA { get; set; }
        public int PM { get; set; }
        public double WaitMoving { get; set; }
        public int force { get; set; }
        public int sagesse { get; set; }
        public int CaracVie { get; set; }
        public int chance { get; set; }
        public int agi { get; set; }
        public int intell { get; set; }

        public readonly byte classe;
        public Character(int _id, string _speudo, int _level, int _isDead, int _gfxID, int _cellID, int _mapID, int _couleur1, int _couleur2, int _couleur3, int _sexe, byte _classe, int _pods, int _podsMax,
             int _XP, int _kamas, int _capital, int _PSorts, int _vie, int _energie, int _PA, int _PM, int _force, int _sagesse, int _chance, int _agi, int _intell, bool _newCharac)
        {
            ListCellMove = new List<int>();
            fight = new Fight();
            IsAvailable = true;
            NewCell = -1;
            id = _id;
            speudo = _speudo;
            level = _level;
            mapID = _mapID;
            cellID = _cellID;
            isDead = _isDead;
            gfxID = _gfxID;
            couleur1 = _couleur1;
            couleur2 = _couleur2;
            couleur3 = _couleur3;
            classe = _classe;
            sexe = _sexe;
            pods = _pods;
            podsMax = _podsMax;
            XP = _XP;
            kamas = _kamas;
            capital = _capital;
            PSorts = _PSorts;
            energie = _energie;
            PA = _PA;
            PM = _PM;
            force = _force;
            sagesse = _sagesse;
            chance = _chance;
            agi = _agi;
            intell = _intell;
            newCharac = _newCharac;
            CaracVie = _vie;
            UpdateCarac();
        }

        public void UpdateCarac()
        {
            vie = 45 + level * 5 + CaracVie;
            vieTotal = 45 + level * 5 + CaracVie;
        }

        public void AddSpell(int id, int level = 1)
        {
            SpellsCharacter.Add(new Game.Spell.Spell(id, level));
        }

        public void resCaract()
        {
            PM = 3;
            if (level > 99)
            {
                PA = 7;
            }
            else
                PA = 6;
        }
    }
}
