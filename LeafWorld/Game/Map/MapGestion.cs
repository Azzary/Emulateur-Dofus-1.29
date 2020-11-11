﻿using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
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
            else if (prmClient.account.character.Map != null)
            {
                
            }
            else
                if (prmClient.database.tablemap.Maps.ContainsKey(prmClient.account.character.mapID))
                {
                    Map map = prmClient.database.tablemap.Maps[prmClient.account.character.mapID];
                    map.CharactersOnMap.Add(prmClient);
                    prmClient.account.character.Map = map;
                    prmClient.send($"GDM|{map.Id}|{map.CreateTime}|{map.DataKey}");
                    
                }
        }

        [PacketAttribute("rpong2")]
        public void ConfirmMap(Network.listenClient prmClient, string prmPacket)
        {
            prmClient.send("ÃR");
            account.character.Character character = prmClient.account.character;
            //For all character in the map GM+...GM+...
            string packet = $"GM|+{character.cellID};1;-1^0;{character.id};{character.speudo}^-1;{character.classe},~;{character.gfxID}^100;{character.sexe};0,0,0,999,0;{character.couleur1};{character.couleur2};{character.couleur3};null,null,null,null,null,1,;0;;;;;8;;0.0;0;";

            prmClient.send(packet);
            if (prmClient.account.character.Map != null)
            {
                Map _map = prmClient.account.character.Map;
                account.character.Character entitie; 
                for (int i = 0; i < _map.CharactersOnMap.Count; i++)
                {
                    entitie = _map.CharactersOnMap[i].account.character;
                    if (entitie.fight.InFight == 0)
                    {
                        prmClient.send($"GM|+{entitie.cellID};1;-1^0;{entitie.id};{entitie.speudo}^-1;{entitie.classe},~;{entitie.gfxID}^100;{entitie.sexe};0,0,0,999,0;{entitie.couleur1};{entitie.couleur2};{entitie.couleur3};null,null,null,null,null,1,;0;;;;;8;;0.0;0;");
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
            prmClient.send("rpong1\0rpong2");
        }
    }
}

