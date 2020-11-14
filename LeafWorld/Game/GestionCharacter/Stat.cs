using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Character
{
    class Stat
    {
        [PacketAttribute("AB")]
        public void AddStats(Network.listenClient prmClient, string prmPacket)
        {
            string action = prmPacket.Substring(2, 2);
            string stats = prmPacket.Substring(5).Split("\0")[0];

            if (int.TryParse(stats, out int res))
            {
                if (prmClient.account.character.capital >= res)
                {
                    prmClient.account.character.capital -= res;
                }
                else
                    return;

                if (action == "10")//force
                {
                    prmClient.account.character.force += res;
                }
                else if (action == "11")//vita
                {
                    prmClient.account.character.CaracVie += res;
                }
                else if (action == "12")//sagesse
                {
                    prmClient.account.character.sagesse += res;
                }
                else if (action == "13")//chance
                {
                    prmClient.account.character.chance += res;
                }
                else if (action == "14")//agi
                {
                    prmClient.account.character.agi += res;
                }
                else if (action == "15")//intell
                {
                    prmClient.account.character.intell += res;
                }
                prmClient.account.character.resCaract();
                prmClient.send(GestionCharacter.createAsPacket(prmClient));
            }
            else
                return;


        }

    }
}
