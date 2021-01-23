using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeafWorld.Game.Map
{
    class MapGestion
    {
        [PacketAttribute("BD")]
        public void SendMap(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.statue == 0)
            {
                prmClient.send("GCK|1");
                prmClient.send(Character.GestionCharacter.createAsPacket(prmClient));
                prmClient.account.statue = 1;
            }
            else if (prmClient.database.tablemap.Maps.ContainsKey(prmClient.account.character.mapID))
                {
                    Map map = prmClient.database.tablemap.Maps[prmClient.account.character.mapID];
                    if (!map.CharactersOnMap.Contains(prmClient))
                        map.CharactersOnMap.Add(prmClient);
                    prmClient.account.character.Map = map;
                    prmClient.send($"GDM|{map.Id}|{map.CreateTime}|{map.DataKey}");
                    CreateMapPacketInfo(prmClient);
            }
        }



        [PacketAttribute("rpong2")]
        public void ConfirmMap(Network.listenClient prmClient, string prmPacket)
        {
            prmClient.send("ÃR");
           CreateMapPacketInfo(prmClient);

        }

        public static void CreateMapPacketInfo(LeafWorld.Network.listenClient prmClient)
        {
            account.character.Character character = prmClient.account.character;

            string packet = $"GM|+{character.cellID};1;-1^0;{character.id};{character.speudo}^-1;{character.classe},~;{character.gfxID}^100;{character.sexe};0,0,0,999,0;{character.couleur1};{character.couleur2};{character.couleur3};null,null,null,null,null,1,;0;;;;;8;;0.0;0;" +
                $"\0{Game.Item.MoveItem.GetItemsPos(prmClient.account.character)}";

            prmClient.send(packet);
            if (prmClient.account.character.Map != null)
            {
                string packet2 = "";
                Map _map = prmClient.account.character.Map;
                List<int> keyList = new List<int>(_map.FightInMap.Keys);
                foreach (int key in keyList)
                {
                    if (_map.FightInMap[key].FightStade == 0)
                    {
                        packet2 += $"Gc+{_map.FightInMap[key].InfoJoinConbat[4]};0|{_map.FightInMap[key].InfoJoinConbat[0]};{_map.FightInMap[key].InfoJoinConbat[1]};0;-1|{_map.FightInMap[key].InfoJoinConbat[2]};{_map.FightInMap[key].InfoJoinConbat[3]};0;-1";
                    }
                }
                account.character.Character entitie;
                for (int i = 0; i < _map.CharactersOnMap.Count; i++)
                {
                    entitie = _map.CharactersOnMap[i].account.character;
                    if (entitie.fight.InFight == 0)
                    {
                        prmClient.send($"GM|+{entitie.cellID};1;-1^0;{entitie.id};{entitie.speudo}^-1;{entitie.classe},~;{entitie.gfxID}^100;{entitie.sexe};0,0,0,999,0;{entitie.couleur1};{entitie.couleur2};{entitie.couleur3};null,null,null,null,null,1,;0;;;;;8;;0.0;0;");
                        prmClient.send(Game.Item.MoveItem.GetItemsPos(entitie));
                        prmClient.send(packet2);
                        _map.CharactersOnMap[i].send(packet);

                    }

                }
            }
            prmClient.send("GDK");
            prmClient.send("fC0\0GDD32\0rpong3");
        }

        [PacketAttribute("GI")]
        public void SendPong(Network.listenClient prmClient, string prmPacket)
        {

            if (prmClient.account.character.fight.InFight == 0)
            {
                prmClient.send("rpong1\0rpong2");
            }
            else
            {
                prmClient.account.character.fight = new account.character.Fight();
                prmClient.account.character.IsAvailable = true;
                Character.GestionCharacter.createAsPacket(prmClient);
                CreateMapPacketInfo(prmClient);
            }

        }

    }
}

