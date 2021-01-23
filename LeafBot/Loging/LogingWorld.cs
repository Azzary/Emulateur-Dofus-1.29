using LeafBot.PacketGestion;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LeafBot.Loging
{
    class LogingWorld
    {
        [PacketAttribute("HG")]
        public void HelloGame(Client.Client client, string Packet, List<string> LastPacket)
        {
            //ATNB1xcQ6\0
            client.send("AT" + client.GUID+"\0");
        }

        [PacketAttribute("ATK0")]
        public void Af(Client.Client client, string Packet, List<string> LastPacket)
        {
            client.send("Ak0\0AV");
        }

        [PacketAttribute("ALK")]
        public void GetListPersonnage(Client.Client client, string Packet, List<string> LastPacket)
        {
            string[] ListPersonnage = Packet.Split('|');
            if (ListPersonnage.Length > 2)
            {
                client.send("AS" + ListPersonnage[2].Split(';')[0]);
                client.send("Af");
            }
            else
            {
                client.send($"AA{RandomString(10)}|{LeafConfig.ClasseToCreate}|{LeafConfig.Sexe}|-1|-1|-1");
            }
        }

        [PacketAttribute("ASK")]
        public void GetCharacterInformation(Client.Client client, string Packet, List<string> LastPacket)
        {
            client.send("GC1");
            client.send("BD");
        }

        [PacketAttribute("As")]
        public void GetSpells(Client.Client client, string Packet, List<string> LastPacket)
        {
            client.send("BD");
        }
        [PacketAttribute("GDM")]
        public void GetMap(Client.Client client, string Packet, List<string> LastPacket)
        {
            client.send("rpong3");
            client.send("Ir742;556;2");
            client.send("GI");
        }

        [PacketAttribute("rpong1")]
        public void rpong(Client.Client client, string Packet, List<string> LastPacket)
        {
            client.send("rpong1\0rpong2");
        }



        private static Random random = new Random();
        public static string RandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
