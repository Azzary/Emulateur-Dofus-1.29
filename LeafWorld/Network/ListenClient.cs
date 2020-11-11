using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Linq;
using System.Threading;

namespace LeafWorld.Network
{
    public class listenClient
    {

        public event Action<string> packetReceivedEvent;
        public List<listenClient> CharacterInWorld;

        public Database.LoadDataBase database;
        public Socket ClientSocket { get; set; }
        public LinkServer linkServer;
        public bool isCo = true;

        public bool YourTurn = false;
        public Game.account.Account account = new Game.account.Account();


        private List<listenClient> queue;

        public listenClient(Socket _ClientSocket, Database.LoadDataBase _database, List<listenClient> _queue, LinkServer _linkServer , List<listenClient> _CharacterInWorld)
        {
            CharacterInWorld = _CharacterInWorld;
            linkServer = _linkServer;
            queue = _queue;
            database = _database;
            ClientSocket = _ClientSocket;

        }

        public void send(string packet)
        {
            try
            {
                ClientSocket.Send(Encoding.ASCII.GetBytes(packet + "\0"));
            }
            catch (Exception) { }

        }

        public bool DbLoad = false;
        public void startlisten()
        {
            send("HG");
            byte[] buffer = new byte[ClientSocket.ReceiveBufferSize];
            int len = ClientSocket.Receive(buffer);
            string packets = Encoding.UTF8.GetString(buffer, 0, len);
            string guid = packets.Substring(2,7);
            if (isCo)
            {
                for (int i = 0; i < linkServer.ListOfGUID.Count; i++)
                {
                    if (linkServer.ListOfGUID[i][1] == guid)
                    {
                        account.GUID = guid;
                        account.ID = int.Parse(linkServer.ListOfGUID[i][0]);
                        linkServer.ListOfGUID.RemoveAt(i);
                        linkServer.addAccount(account.ID);
                        database.tablecharacter.getCharacter(this);
                        DbLoad = true;
                        send("ATK0");
                        send("ALK10|0");
                        break;
                    }

                }
                
            }

            while (true)
            {
                if (YourTurn)
                {
                    break;
                }
                else if (!isCo)
                {
                    break;
                }
                Thread.Sleep(80);
            }

            while (isCo)
            {
                len = ClientSocket.Receive(buffer);
                packets = Encoding.UTF8.GetString(buffer, 0, len);

                if (packets == string.Empty)
                    break;

                Console.WriteLine(packets);

                foreach (string packet in packets.Replace("\x0a", string.Empty).Split('\0').Where(x => x != string.Empty))
                {
                    packetReceivedEvent?.Invoke(packet);

                    PacketGestion.PacketGestion.Gestion(this, packet);
                }

                buffer = new byte[ClientSocket.ReceiveBufferSize];
            }

            ClientSocket.Close();
        }


        public void remove(listenClient prmClient)
        {
            prmClient.isCo = false;
            
            if (account.character != null && account.character.Map != null)
            {
                prmClient.CharacterInWorld.Remove(prmClient);
                account.character.Map.remove(prmClient);
            }
        }

    }
}
