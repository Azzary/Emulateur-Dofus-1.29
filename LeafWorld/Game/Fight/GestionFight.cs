using LeafWorld.Network;
using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LeafWorld.Game.Fight
{
    class GestionFight
    {

        [PacketAttribute("Gp")]
        public void MovingplacementCell(Network.listenClient prmClient, string prmPacket)
        {

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
                        prmClient.account.character.fight.FightCell = cellID;
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
                    GICpacket += $"{Character.id};{Character.fight.FightCell}|";
                    GTLpacket += $"|{Character.id}";

                
                }
                entity.send($"GR1{prmClient.account.character.id}");
            }
            if (allReady)
            {
                prmClient.account.character.Map.FightInMap[prmClient.account.character.fight.FightID].FightStade = 1;
                for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
                {
                    listenClient entity = prmClient.account.character.fight.EntityInFight[i];
                    entity.send(GICpacket);
                    entity.send("GS");
                    entity.account.character.fight.InFight = 2;
                    entity.send(GTLpacket);
                }
                prmClient.account.character.Map.FightInMap[prmClient.account.character.fight.FightID].GTLpacket = GTLpacket;
                new Thread(prmClient.account.character.Map.FightInMap[prmClient.account.character.fight.FightID].start).Start();

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

        [PacketAttribute("GA903")]
        public void JoinFight(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.character.IsAvailable && int.TryParse(prmPacket.Split("\0")[0].Substring(7), out int id) && prmClient.account.character.IsAvailable && int.TryParse(prmPacket.Split(";")[0].Substring(5), out int FightID))
            {


                    if (prmClient.account.character.Map.FightInMap[FightID].FightStade != 0)
                    {
                        return;
                    }
                    for (int x = 0; x < prmClient.account.character.Map.FightInMap[FightID].ListEntity.Count; x++)
                    {
                        if (prmClient.account.character.Map.FightInMap[FightID].ListEntity[x].account.character.id == id)
                        {
                            prmClient.account.character.IsAvailable = false;
                            prmClient.account.character.fight.equipeID = prmClient.account.character.Map.FightInMap[FightID].ListEntity[x].account.character.fight.equipeID;
                            prmClient.account.character.fight.InFight = 1;
                            prmClient.account.character.fight.FightID = prmClient.account.character.Map.FightInMap[FightID].ListEntity[x].account.character.fight.FightID;
                            duel.LauchDuel(prmClient, null, prmClient.account.character.Map.FightInMap[FightID]);
                            return;
                        }
                    }
                

            }

        }



        [PacketAttribute("GQ")]
        public void GiveUp(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.character.fight.InFight == 0)
            {
                return;
            }

            if (prmClient.account.character.fight.EntityInFight.Count <= 2 && prmClient.account.character.Map.FightInMap.ContainsKey(prmClient.account.character.fight.FightID))
            {
                prmClient.account.character.Map.FightInMap.Remove(prmClient.account.character.fight.FightID);

                for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
                {
                    prmClient.account.character.fight.EntityInFight[i].send($"GM|-{prmClient.account.character.id}");
                    prmClient.account.character.fight.EntityInFight[i].send("GV\0GCK|1\0ILS1000");
                }
                for (int i = 0; i < prmClient.account.character.Map.CharactersOnMap.Count; i++)
                {
                    prmClient.account.character.Map.CharactersOnMap[0].send("Gc-" + prmClient.account.character.fight.FightID);
                    Map.MapGestion.CreateMapPacketInfo(prmClient);
                }
            }
            else
            {
                prmClient.send("GV\0GCK|1\0ILS1000");
                prmClient.account.character.fight.EntityInFight.Remove(prmClient);
                for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
                {
                    prmClient.account.character.fight.EntityInFight[i].send($"GM|-{prmClient.account.character.id}");
                }
            }

        }
    }
}
