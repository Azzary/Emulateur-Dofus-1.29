using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using LeafAuth.Database.table;

namespace LeafAuth.Network
{
    public class LinkServer
    {
        public List<int> ListIDAccount;
        Socket server;
        public LinkServer(List<int> _ListIDAccount)
        {
            ListIDAccount = _ListIDAccount;
            server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 602));
            server.Listen(5);
            server = server.Accept();
            new Thread(AcceptServerConnection).Start();
        }

        private void AcceptServerConnection()
        {
            byte[] buffer = new byte[server.ReceiveBufferSize];
            int len = server.Receive(buffer);
            string packets = Encoding.UTF8.GetString(buffer, 0, len);
            if (packets.Substring(0,2) == "NS")
            {
                while (true)
                {
                    buffer = new byte[server.ReceiveBufferSize];
                    len = server.Receive(buffer);
                    packets = Encoding.UTF8.GetString(buffer, 0, len);
                    foreach (var item in packets.Split(";"))
                    {
                        if (item == "")
                            break;
                        if (item.Substring(0, 2) == "NC")
                        {
                            ListIDAccount.Add(int.Parse(item.Substring(2)));
                        }
                        if (item.Substring(0, 2) == "DC")
                        {
                            ListIDAccount.Remove(int.Parse(item.Substring(2)));
                        }
                    }


                }

            }
           
        }
    
        public void sendConnectionToServer(int ID, string guid, int Role)
        {
            server.Send(Encoding.ASCII.GetBytes($"NC{ID}|{guid}|{Role};"));

        }

        public void RemoveAccount(int ID)
        {
            ListIDAccount.Remove(ID);

        }


    }
}
