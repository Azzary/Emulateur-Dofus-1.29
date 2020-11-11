using LeafWorld.Network;
using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LeafWorld.Game.Fight
{
    class PreFight
    {

        [PacketAttribute("Gp")]
        public void MovingplacementCell(Network.listenClient prmClient, string prmPacket)
        {
            //GIC|998;373\0
            if (prmClient.account.character.fight.InFight != 1)
            {
                return;
            }

            if (int.TryParse(prmPacket.Split("\0")[0].Substring(2), out int cellID))
            {
                if (prmClient.account.character.fight.ListCellIDPlacement.Contains(cellID))
                {
                    for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
                    {
                        prmClient.account.character.fight.EntityInFight[i].send($"GIC|{prmClient.account.character.id};{cellID}");
                        prmClient.account.character.cellID = cellID;
                    }
                }
            }
        }


        [PacketAttribute("GR1")]
        public void IsReady(Network.listenClient prmClient, string prmPacket)
        {


            if (prmClient.account.character.fight.InFight != 1 || prmClient.account.character.fight.IsReady)
            {
                return;
            }
            bool allReady = true;

            string GICpacket = "GIC|";
            string GTLpacket = "GTL";

            prmClient.account.character.fight.IsReady = true;
            for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
            {
                listenClient entity = prmClient.account.character.fight.EntityInFight[i];
                account.character.Character Character = entity.account.character;
                if (allReady && !entity.account.character.fight.IsReady)
                    allReady = false;
                else
                {
                    GICpacket += $"{Character.id};{Character.cellID}|";
                    GTLpacket += $"|{Character.id}";

                
                }
                entity.send($"GR1{prmClient.account.character.id}");
            }
            if (allReady)
            {
                for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
                {
                    listenClient entity = prmClient.account.character.fight.EntityInFight[i];
                    entity.send(GICpacket);
                    entity.send("GS");
                    entity.account.character.fight.InFight = 2;
                    entity.send(GTLpacket);
                }
                Fight fight = new Fight(prmClient.account.character.fight.EntityInFight, GTLpacket);
                new Thread(fight.start).Start();

            }
        }

        [PacketAttribute("GR0")]
        public void IsNotReady(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.character.fight.InFight != 1)
            {
                return;
            }
            prmClient.account.character.fight.IsReady = false;
            for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
            {
                listenClient entity = prmClient.account.character.fight.EntityInFight[i];
                entity.send($"GR0{prmClient.account.character.id}");
            }

        }

    }
}
