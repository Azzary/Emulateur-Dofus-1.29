using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace LeafBot.Client
{
    public class Client
    {
        public  Map.Map map;
        public string GUID;
        public Socket socket;
        public Dictionary<string, string> Servers;
        public int ID;
        byte[] buffer;
        int len;
        List<Client> Clients;
        public Client(int _id, List<Client> _Clients)
        {
            Clients = _Clients;
            ID = _id;
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }
       
        
        public void StartConnection()
        {
            for (int i = 0; i < 5; i++)
            {
                try
                {
                    socket.Connect(new IPEndPoint(IPAddress.Parse(LeafConfig.ServerIP), LeafConfig.ServerPORT));
                    return;
                }
                catch (Exception)
                {

                }
            }
        
            
        }

        public void send(string packet)
        {
            try
            {
                socket.Send(Encoding.ASCII.GetBytes(packet + "\0"));
            }
            catch (Exception) { }

        }

        public void Recv()
        {
            if (socket.Poll(10000, SelectMode.SelectRead))
            {
                buffer = new byte[socket.ReceiveBufferSize];
                len = socket.Receive(buffer);
                if (len == 0)
                    Clients.Remove(this);
                string Packets = Encoding.ASCII.GetString(buffer, 0, len);
                Console.WriteLine(Packets);
                foreach (string Packet in Packets.Split('\0'))
                {
                    PacketGestion.PacketGestion.Gestion(this, Packet);
                }

            }
        }
    }

}
