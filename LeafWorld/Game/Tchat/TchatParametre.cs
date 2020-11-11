using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Tchat
{
    class TchatParametre
    {
        [PacketAttribute("cC")]
        public void TchatOption(Network.listenClient prmClient, string prmPacket)
        {
            prmClient.send(prmPacket);
        }

    }
}
