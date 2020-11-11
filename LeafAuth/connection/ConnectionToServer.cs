using LeafAuth.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafAuth.connection
{
    class ConnectionToServer
    {
        [PacketAttribute("AX")]
        public void CheckQueue(Network.listenClient prmClient, string prmPacket)
        {
            if (Int32.TryParse(prmPacket.Substring(2), out int id))
            {
                if (prmClient.account.ListServ.Contains(id))
                {
                    string packet = "AXK";
                    packet += Util.hash.CryptIP("127.0.0.1");
                    packet += "bwZ"; //port
                    string GUID = Util.hash.GenerateString(7);
                    packet += GUID;
                    prmClient.linkServer.sendConnectionToServer(prmClient.account.ID, GUID);
                    prmClient.send(packet);
                }
            }
            else
                prmClient.isCo = false;


        }


    }
}
