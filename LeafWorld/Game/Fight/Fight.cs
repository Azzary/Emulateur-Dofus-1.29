using LeafWorld.Network;
using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace LeafWorld.Game.Fight
{
    public class Fight
    {
        List<listenClient> ListEntity;
        string GTLpacket;
        public Fight(List<listenClient> _ListEntity, string _GTLpacket)
        {
            GTLpacket = _GTLpacket;
            ListEntity = _ListEntity;
        }
        public Fight()
        {}

        public void start()
        {
            listenClient TourEntity;
            int pos = 0;
            while (true)
            {
                TourEntity = ListEntity[pos];
                while (true)
                {
                    if (pos >= ListEntity.Count - 1)
                    {
                        pos = 0;
                        if (TourEntity.account.character.fight.InFight == 2)
                        {
                            break;
                        }
                    }
                    else if(TourEntity.account.character.fight.InFight == 2)
                    {
                        pos++;
                        break;
                    }
                    pos++;
                    TourEntity = ListEntity[pos];
                }

                string packetGTF = "GTF" + TourEntity.account.character.id;
                string packetGTM = CreateGTMPacket();
                for (int x = 0; x < ListEntity.Count; x++)
                {
                    ListEntity[x].send(packetGTF);
                    ListEntity[x].send(GTLpacket+ "\0" +packetGTM);
                    ListEntity[x].send($"GTS{TourEntity.account.character.id}|40000\0");
                }
                //GAC\0GA;940\0As...
                TourEntity.account.character.fight.YourTurn = true;
                TourEntity.send("GAC\0GA;940\0");// + Character.GestionCharacter.createAsPacket(TourEntity));
                for (int i = 0; i < 400; i++)
                {
                    Thread.Sleep(100);
                    if (!TourEntity.account.character.fight.YourTurn)
                    {
                        break;
                    }
                }
                TourEntity.account.character.resCaract();
            }
        }

        private string CreateGTMPacket()
        {
            string GTMpacket = "GTM|";
            for (int i = 0; i < ListEntity.Count; i++)
            {

                account.character.Character Character = ListEntity[i].account.character;


                    //GTM|1257;0;50;6;3;368;;50;0;0;0,0,0,0,0,0,0,|998;0;50;6;3;444;;50;0;0;0,0,0,0,0,0,0,\0
                GTMpacket += $"{Character.id};0;{Character.vie};{Character.PA};{Character.PM};{Character.cellID};;{Character.vieTotal};0;0;0,0,0,0,0,0,0," +
                    $"|998;0;50;6;3;444;;50;0;0;0,0,0,0,0,0,0,;|";

                


            }

            return GTMpacket;
        }

        [PacketAttribute("Gt")]
        public void AcceptRequetDuel(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.character.fight.YourTurn)
                prmClient.account.character.fight.YourTurn = false; 
        }

    }
}
