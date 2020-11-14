using LeafWorld.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LeafWorld.Game.Item;
namespace LeafWorld.Game.account.character
{
    public class Character
    {
        public List<Spell.Spell> SpellsCharacter = new List<Spell.Spell>();

        public Inventaire.Inventaire Invertaire = new Inventaire.Inventaire();

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
        public int TotalVie { get; set; }
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
        public int EquipementVie { get; set; }
        public int EquipementPA { get; set; }
        public int EquipementPM { get; set; }
        public int EquipementIntell { get; set; }
        public int EquipementForce { get; set; }
        public int EquipementSagesse { get; set; }
        public int EquipementChance { get; set; }
        public int EquipementAgi { get; set; }
        public int EquipementPO { get; set; }
        public int EquipementDommages { get; set; }
        public int EquipementDommagesPieges { get; set; }
        public int EquipementCoupsCritique { get; set; }
        public int EquipementInitiative { get; set; }
        public int EquipementResistanceForce { get; set; }
        public int EquipementResistanceIntell { get; set; }
        public int EquipementResistanceEau { get; set; }
        public int EquipementResistanceAgi { get; set; }

        public int TotalPA { get; set; }
        public int TotalPM { get; set; }
        public int TotalIntell { get; set; }
        public int TotalForce { get; set; }
        public int TotalSagesse { get; set; }
        public int TotalChance { get; set; }
        public int TotalAgi { get; set; }


        public readonly byte classe;
        public Character(int _id, string _speudo, int _level, int _isDead, int _gfxID, int _cellID, int _mapID, int _couleur1, int _couleur2, int _couleur3, int _sexe, byte _classe, int _pods, int _podsMax,
             int _XP, int _kamas, int _capital, int _PSorts, int _vie, int _energie, int _PA, int _PM, int _force, int _sagesse, int _chance, int _agi, int _intell, bool _newCharac)
        {
            Invertaire = new Inventaire.Inventaire();
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
            resCaract();
        }


        public void AddSpell(int id, int level = 1)
        {
            SpellsCharacter.Add(new Game.Spell.Spell(id, level));
        }

        public void UpdateEquipentStats()
        {
            if (fight.InFight == 1 || fight.InFight == 2)
            {
                return;
            }
            resCaract();
            EquipementVie = 0;
            EquipementPA = 0;
            EquipementPM = 0;
            EquipementIntell = 0;
            EquipementAgi = 0;
            EquipementChance = 0;
            EquipementSagesse = 0;
            EquipementForce = 0;
            EquipementPO = 0;
            EquipementDommages = 0;
            EquipementDommagesPieges = 0;
             EquipementCoupsCritique = 0;
            EquipementInitiative = 0;
            EquipementResistanceForce = 0;
            EquipementResistanceIntell = 0;
            EquipementResistanceEau = 0;
            EquipementResistanceAgi = 0;

            List<string> stats = new List<string>();
            if (Invertaire.Stuff.Any(x => x.Position == 1))
                stats.Add(Invertaire.Stuff.First(x => x.Position == 1).Stats);
            
            if (Invertaire.Stuff.Any(x => x.Position == 6))
                stats.Add(Invertaire.Stuff.First(x => x.Position == 6).Stats);
            
            if (Invertaire.Stuff.Any(x => x.Position == 7))
                stats.Add(Invertaire.Stuff.First(x => x.Position == 7).Stats);
            
            if (Invertaire.Stuff.Any(x => x.Position == 8))
                stats.Add(Invertaire.Stuff.First(x => x.Position == 8).Stats);

            if (Invertaire.Stuff.Any(x => x.Position == 15))
                stats.Add(Invertaire.Stuff.First(x => x.Position == 15).Stats);

            foreach (string stat in stats)
            {
                foreach (string oneStat in stat.Split(","))
                {
                    string[] datas = oneStat.Split("#");
                    int ID = Convert.ToInt32(datas[0], 16);
                    int valeur = Convert.ToInt32(datas[1], 16);
                    //string Effect = "0d0+0";
                    switch ((Item.EffectEnum)ID)
                    {
                        case EffectEnum.AddPA:
                            EquipementPA += valeur;
                            break;
                        case EffectEnum.AddDamageCritic:
                            EquipementCoupsCritique += valeur;
                            break;
                        case EffectEnum.AddInitiative:
                            EquipementInitiative += valeur;
                            break;
                        case EffectEnum.AddVitalite:
                            EquipementVie += valeur;
                            vie += valeur;
                            break;
                        case EffectEnum.AddDamage:
                            EquipementDommages += valeur;
                            break;
                        case EffectEnum.AddPO:
                            EquipementPO += valeur;
                            break;
                        case EffectEnum.AddPM:
                            EquipementPM += valeur;
                            break;
                        case EffectEnum.AddForce://force
                            EquipementForce += valeur;
                            break;
                        case EffectEnum.AddAgilite://agi
                            EquipementAgi += valeur;
                            break;
                        case EffectEnum.AddChance://chance
                            EquipementChance += valeur;
                            break;
                        case EffectEnum.AddSagesse://sagesse
                            EquipementSagesse += valeur;
                            break;
                        case EffectEnum.AddIntelligence://intell
                            EquipementIntell += valeur;
                            break;
                        case EffectEnum.AddDamagePiege://DmgPiege
                            EquipementDommagesPieges += valeur;
                            break;
                        default:
                            break;
                    }
                }

            }
            UpdateStat();

        }

        public void UpdateStat()
        {
            resCaract();
            TotalPA = PA + EquipementPA;
            TotalPM = PM + EquipementPM;
            TotalIntell = intell + EquipementIntell;
            TotalAgi = agi + EquipementAgi;
            TotalForce = force + EquipementForce;
            TotalSagesse = sagesse + EquipementSagesse;
            TotalChance = chance + EquipementChance;
        }

        public void resCaract()
        {
            vie = 45 + level * 5 + CaracVie;
            TotalVie = 45 + level * 5 + CaracVie + EquipementVie;
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
