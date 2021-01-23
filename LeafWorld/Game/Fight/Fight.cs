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
        public List<listenClient> ListEntity;
        public string GTLpacket { get; set; }
        public int FightStade { get; set; }
        public bool FightRuning { get; set; }
        public int[] InfoJoinConbat { get; set; }
        public Fight(List<listenClient> _ListEntity)
        {
            ListEntity = _ListEntity;
            FightRuning = true;
        }
        public Fight()
        {
            FightRuning = true;
        }

        public void start()
        {
            for (int i = 0; i < ListEntity[0].account.character.Map.CharactersOnMap.Count; i++)
            {
                if (ListEntity.Contains(ListEntity[0].account.character.Map.CharactersOnMap[i]))
                {
                    continue;
                }
                ListEntity[0].account.character.Map.CharactersOnMap[i].send("Gc-"+ ListEntity[0].account.character.fight.FightID);
                //\0
            }
            listenClient TourEntity;
            int pos = 0;
            while (FightRuning)
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
                TourEntity.account.character.UpdateStat();
            }
        }

        private string CreateGTMPacket()
        {
            string GTMpacket = "GTM|";
            for (int i = 0; i < ListEntity.Count; i++)
            {

                account.character.Character Character = ListEntity[i].account.character;


                    //GTM|1257;0;50;6;3;368;;50;0;0;0,0,0,0,0,0,0,|998;0;50;6;3;444;;50;0;0;0,0,0,0,0,0,0,\0
                GTMpacket += $"{Character.id};0;{Character.TotalVie};{Character.TotalPA};{Character.TotalPM};{Character.fight.FightCell};;{Character.TotalVie};0;0;0,0,0,0,0,0,0," +
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
