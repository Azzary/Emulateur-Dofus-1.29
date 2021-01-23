using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Linq;
using System.Threading;
using System.Collections;

namespace LeafWorld.Network
{
    public class listenClient
    {

        //public event Action<string> packetReceivedEvent;
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
        public bool IsLoadDb = false;
        public void startlisten()
        {
            send("HG");
            byte[] buffer = new byte[ClientSocket.ReceiveBufferSize];
            int len = ClientSocket.Receive(buffer);
            string packets = Encoding.UTF8.GetString(buffer, 0, len);
            string guid = packets.Substring(2,7);

            string[] VerifAccount = null;

            for (int i = 0; i < 3; i++)
            {
                if (linkServer.ListOfGUID.Count != 0)
                    VerifAccount = linkServer.ListOfGUID.Find(x => x != null && x[1] == guid);
                Thread.Sleep(50);
            }
            if (VerifAccount == null)
            {
                ClientSocket.Close();
                isCo = false;
                return;
            }
            account.GUID = guid;
            account.ID = int.Parse(VerifAccount[0]);
            account.Role = short.Parse(VerifAccount[2]);
            linkServer.ListOfGUID.Remove(VerifAccount);
            linkServer.addAccount(account.ID);

            while (true)
            {
                if (YourTurn || !isCo)
                {
                    break;
                }
                send($"Af{queue.IndexOf(this) + 1}");
                Thread.Sleep(80);
            }
            IsLoadDb = true;
            if (isCo)
            {
                database.tablecharacter.LoadCharacter(this);
                send("ATK0");
                send("ALK10|0");
                DbLoad = true;
            }

            if (DbLoad == false)
            {
                isCo = false;
                Console.WriteLine("hoho");
            }

            while (isCo)
            {
                try
                {
                    len = ClientSocket.Receive(buffer);
                    packets = Encoding.UTF8.GetString(buffer, 0, len);
                }
                catch (Exception)
                { break; }


                if (packets == string.Empty)
                    break;

                //Console.WriteLine(packets);

                foreach (string packet in packets.Replace("\x0a", string.Empty).Split('\0').Where(x => x != string.Empty))
                {
                    Console.WriteLine(packet);
                    //packetReceivedEvent?.Invoke(packet);
                    PacketGestion.PacketGestion.Gestion(this, packet);
                }

                buffer = new byte[ClientSocket.ReceiveBufferSize];
            }

            ClientSocket.Close();
        }

        public bool recv()
        {
            byte[] buffer = new byte[ClientSocket.ReceiveBufferSize];
            int len = ClientSocket.Receive(buffer);
            string packets = Encoding.UTF8.GetString(buffer, 0, len);
            if (ClientSocket.Poll(10000, SelectMode.SelectRead))
            {
                buffer = new byte[1024];
                len = ClientSocket.Receive(buffer);
                if (len == 0)
                    return false;
                packets = Encoding.ASCII.GetString(buffer, 0, len);
                foreach (string packet in packets.Replace("\x0a", string.Empty).Split('\0').Where(x => x != string.Empty))
                {
                    Console.WriteLine(packet);
                    PacketGestion.PacketGestion.Gestion(this, packet);
                }
                return true;
            }
            return false;
        }


        public void remove(listenClient prmClient)
        {
            linkServer.RemoveAccount(account.ID, account.GUID);
            prmClient.isCo = false;
            
            if (account.character != null && account.character.Map != null)
            {
                prmClient.account.character.Map.Remove(prmClient);
                prmClient.CharacterInWorld.Remove(prmClient);
                //account.character.Map.remove(prmClient);
            }
        }

    }
}
