using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Spell
{
    class SpellManagement
    {
        [PacketAttribute("SB")]
        public void UpSpell(Network.listenClient prmClient, string prmPacket)
        {
            if (int.TryParse(prmPacket.Split("\0")[0].Substring(2),out int spellID))
            {
                foreach (Game.Spell.Spell spell in prmClient.account.character.SpellsCharacter)
                {
                    
                    if (spell.id == spellID)
                    {
                        if (prmClient.account.character.PSorts < spell.level || spell.level == 6)
                            return;
                        Game.account.character.Character character = prmClient.account.character;
                        prmClient.account.character.PSorts -= spell.level;
                        spell.level++;
                        prmClient.send($"SUK{spell.id}~{spell.level}");
                        prmClient.send($"Ak{prmClient.database.experience.ExperienceStat[character.level][0]},{character.XP},{prmClient.database.experience.ExperienceStat[character.level + 1][0]}" +
                            $"|0|{character.capital}|{character.PSorts}|0~0,0,1,0,0,0|{character.vie},{character.vieTotal}|{character.energie},10000|\0Ab");

                    }
                }
            }

        }
    }
}
