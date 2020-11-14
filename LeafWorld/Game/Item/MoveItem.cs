using LeafWorld.PacketGestion;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System;

namespace LeafWorld.Game.Item
{
    class MoveItem
    {
        [PacketAttribute("OM")]
        public void MoveStuff(Network.listenClient prmClient, string prmPacket)
        {
            string[] Datas = prmPacket.Substring(2).Split('|');

            int quantity = 1;
            int UID = -1;
            int pos = 1;


            if (Datas.Length >= 3)
            {
                if (!int.TryParse(Datas[2], out quantity))
                    return;
            }

            if (!int.TryParse(Datas[1], out pos))
                return;

            if (!int.TryParse(Datas[0], out UID))
                return;

            if (!prmClient.account.character.Invertaire.Stuff.Any(x => x.UID == UID))
                return;

            Stuff item = prmClient.account.character.Invertaire.Stuff.First(x => x.UID == UID);
            if (quantity > 1)
            {
                return;
            }
            else if (item.Niveau > prmClient.account.character.level && false)
            {
                prmClient.send("OAEL");
                return;
            }
            else if (item.Type <= 20)
            {

                if (prmClient.account.character.Invertaire.Stuff.Any(x => x.Position == pos))
                {
                    Stuff lastItem = prmClient.account.character.Invertaire.Stuff.First(x => x.Position == pos);
                    lastItem.Position = -1;

                }
                string info = $"{prmPacket}|{pos}\0";



                item.Position = pos;
            }
            uptadeStuff(prmClient, prmPacket + "\0");

        }

        public static void uptadeStuff(Network.listenClient prmClient, string info = "")
        {
            
            string packet = Game.Character.GestionCharacter.CreateStuffPacketOM(prmClient);
            prmClient.send(info + packet);
            prmClient.send(Game.Character.GestionCharacter.createAsPacket(prmClient));

            for (int i = 0; i < prmClient.account.character.Map.CharactersOnMap.Count; i++)
            {

                prmClient.account.character.Map.CharactersOnMap[i].send(GetItemsPos(prmClient.account.character));
               
            }
        }


        public static string GetItemsPos(account.character.Character character)
        {
            var packet = $"Oa{character.id}|";
            Inventaire.Inventaire inventaire = character.Invertaire;
            if (inventaire.Stuff.Any(x => x.Position == 1))
                packet += inventaire.Stuff.First(x => x.Position == 1).ID.ToString("X");

            packet += ",";

            if (inventaire.Stuff.Any(x => x.Position == 6))
                packet += inventaire.Stuff.First(x => x.Position == 6).ID.ToString("X");

            packet += ",";

            if (inventaire.Stuff.Any(x => x.Position == 7))
                packet += inventaire.Stuff.First(x => x.Position == 7).ID.ToString("X");

            packet += ",";

            if (inventaire.Stuff.Any(x => x.Position == 8))
                packet += inventaire.Stuff.First(x => x.Position == 8).ID.ToString("X");

            packet += ",";

            if (inventaire.Stuff.Any(x => x.Position == 15))
                packet += inventaire.Stuff.First(x => x.Position == 15).ID.ToString("X");

            return packet;
        }

    }
}
