using LeafWorld.Network;
using LeafWorld.PacketGestion;
using Org.BouncyCastle.Asn1.Crmf;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Text;

namespace LeafWorld.Game.Fight
{
    class duel
    {

        [PacketAttribute("GA900")]
        public void RequetDuel(Network.listenClient prmClient, string prmPacket)
        {
            if (!prmClient.account.character.IsAvailable)
            {
                return;
            }
            if (int.TryParse(prmPacket.Substring(5),out int id))
            {
                Map.Map map = prmClient.account.character.Map;
                for (int i = 0; i < map.CharactersOnMap.Count; i++)
                {
                    if (map.CharactersOnMap[i].account.character.id == id)
                    {
                        if (map.CharactersOnMap[i].account.character.IsAvailable)
                        {
                            prmClient.account.character.IsAvailable = false;
                            map.CharactersOnMap[i].account.character.IsAvailable = false;
                            prmClient.account.character.fight.RequestDuel = map.CharactersOnMap[i];
                            map.CharactersOnMap[i].account.character.fight.RequestDuel = prmClient;
                            prmClient.send($"GA;900;{prmClient.account.character.id};{id}");
                            map.CharactersOnMap[i].send($"GA;900;{prmClient.account.character.id};{id}");
                        }
                        return;
                    }
                }
            }


        }


        [PacketAttribute("GA902")]
        public void CancelRequetDuel(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.character.fight.RequestDuel == null)
            {
                return;
            }

            Network.listenClient temp = prmClient.account.character.fight.RequestDuel;

            prmClient.send($"GA;902;{prmClient.account.character.id};{temp.account.character.id}");
            temp.send($"GA;902;{temp.account.character.id};{prmClient.account.character.id}");

            prmClient.account.character.fight.IsLauncherDuel = false;
            prmClient.account.character.IsAvailable = true;
            temp.account.character.IsAvailable = true;
            prmClient.account.character.fight.RequestDuel = null;
            temp.account.character.fight.RequestDuel = null;
        }

        [PacketAttribute("GA901")]
        public void AcceptRequetDuel(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.character.fight.RequestDuel == null || prmClient.account.character.fight.IsLauncherDuel)
            {
                return;
            }
            Network.listenClient ennemie = prmClient.account.character.fight.RequestDuel;
            ennemie.account.character.fight.InFight = 1;
            prmClient.account.character.fight.InFight = 1;
            string packet = $"GA;901;";
            ennemie.send($"{packet}{ennemie.account.character.id};{prmClient.account.character.id}");
            prmClient.send($"{packet}{ennemie.account.character.id};{prmClient.account.character.id}");

            LauchDuel(prmClient, ennemie);

        }

        public static void LauchDuel(listenClient prmClient, listenClient ennemie, Fight JoinFight = null)
        {
            List<listenClient> EntityInFight;
            if (ennemie != null)
            {
                EntityInFight = new List<listenClient>() { ennemie, prmClient };
                prmClient.account.character.fight.equipeID = 0;
                ennemie.account.character.fight.equipeID = 1;
                prmClient.account.character.fight.EntityInFight = EntityInFight;
                ennemie.account.character.fight.EntityInFight = EntityInFight;
                Fight fight = new Fight(EntityInFight);
                fight.FightStade = 0;
                int keys = 1;
                while (prmClient.account.character.Map.FightInMap.ContainsKey(keys))
                {
                    keys++;
                }
                prmClient.account.character.Map.FightInMap.Add(keys,fight);
                prmClient.account.character.fight.FightID = keys;
                ennemie.account.character.fight.FightID = keys;
                fight.InfoJoinConbat = new int[] { prmClient.account.character.id, prmClient.account.character.cellID, ennemie.account.character.id, ennemie.account.character.cellID, prmClient.account.character.fight.FightID };

            }
            else
            {
                JoinFight.ListEntity.Add(prmClient);
                prmClient.account.character.fight.EntityInFight = JoinFight.ListEntity;
                EntityInFight = JoinFight.ListEntity;
            }

            string GMPacket = "";
            for (int i = 0; i < prmClient.account.character.Map.CharactersOnMap.Count; i++)
            {
                prmClient.account.character.Map.CharactersOnMap[i].send($"GM|-{prmClient.account.character.id}");
                if (ennemie != null)
                {
                    prmClient.account.character.Map.CharactersOnMap[i].send($"GM|-{ennemie.account.character.id}");
                    prmClient.account.character.Map.CharactersOnMap[i].send($"Gc+{prmClient.account.character.fight.FightID};0|{prmClient.account.character.id};{prmClient.account.character.cellID};0;-1|{ennemie.account.character.id};{ennemie.account.character.cellID};0;-1");
                }
            }
            int y = prmClient.account.character.fight.FightID;
            for (int x = 0; x < prmClient.account.character.Map.FightInMap[y].ListEntity.Count; x++)
            {
                Game.account.character.Character character = prmClient.account.character.Map.FightInMap[y].ListEntity[x].account.character;
                prmClient.account.character.Map.FightInMap[y].ListEntity[x].send($"Gt{prmClient.account.character.id}|+{prmClient.account.character.id};{prmClient.account.character.speudo};{prmClient.account.character.level}");
                prmClient.send($"Gt{character.id}|+{character.id};{character.speudo};{character.level}");

            }

            prmClient.send("fC1");

            
            if (ennemie!= null)
            {
                ennemie.account.character.fight.AtributePosFight(ennemie);
            }
            
            prmClient.account.character.fight.AtributePosFight(prmClient);
            foreach (listenClient Entity in EntityInFight)
            {
                        account.character.Character character = Entity.account.character;
                character.UpdateEquipentStats();
                GMPacket += $"|+{character.cellID};1;0^false;{character.id};{character.speudo}^-1;{character.classe};{character.gfxID}^100;{character.sexe};{character.level};0,0,0,{character.id + 1},0;{character.couleur1};{character.couleur2};{character.couleur3};null,null,null,null,null,1,;" +
                    $"{character.TotalVie};{character.TotalPA};{character.TotalPM};0;0;0;0;0;0;0;0;;0;0;";
                                                                                                                                                                      
            }

            foreach (listenClient Entity in EntityInFight)
            {
                Entity.send($"GJK2|1|1|0|0|0\0GP{prmClient.account.character.Map.PosFight}|{Entity.account.character.fight.equipeID}\0ILF0\0GM" + GMPacket);
                foreach (listenClient Entity2 in EntityInFight)
                {
                    Entity.send(Item.MoveItem.GetItemsPos(Entity2.account.character));
                }
            }

        }
    }
}
