using LeafWorld.Database;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Threading;

namespace LeafWorld.Network
{
    class WorldServer
    {
        List<listenClient> queue = new List<listenClient>();

        List<listenClient> ListSauvegarde = new List<listenClient>();

        List<listenClient> CharacterInWorld = new List<listenClient>();

        List<string[]> ListOfGUID = new List<string[]>();
        bool sauvegarde = false;
        LinkServer linkServer;
        LoadDataBase DataBase;
        public WorldServer(Database.LoadDataBase _database)
        {
            Console.WriteLine("Connection To Auth...");
            linkServer = new LinkServer(ListOfGUID);
            DataBase = _database;
            Console.WriteLine("Opening Socket...");
            Socket server = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            server.Bind(new IPEndPoint(IPAddress.Parse(World.WorldConfig.IP), World.WorldConfig.port));
            server.Listen(5);
            new Thread(wait_queue).Start();
            Console.WriteLine("Waiting Connection...");
            AcceptConnection(server);

        }

        private void AcceptConnection(object o)
        {
            Socket server = (Socket)o;
            while (true)
            {
                Socket socketClient = server.Accept();
                listenClient li = new listenClient(socketClient, DataBase, queue, linkServer, CharacterInWorld);
                new Thread(ThreadlistenClient).Start(li);
                queue.Add(li);

            }

        }

        private void send_af()
        {
            while (true)
            {
                for (int i = 0; i < queue.Count; i++)
                {
                    if (i < queue.Count)
                    {
                        listenClient var = queue[0];
                        if (var == null)
                        {
                            continue;
                        }
                        if (var.isCo)
                        {

                            var.send($"Af{queue.IndexOf(var) + 1}");

                        }
                       
                       
                    }
                    Thread.Sleep((int)(3000/queue.Count));
                    
                }
            }
        }
        int nbclient = 0;

        private void wait_queue()
        {
            new Thread(send_af).Start();
            while (!sauvegarde)
            {
                if (queue.Count > 0)
                {
                    listenClient var = queue[0];
                    if (var == null)
                    {
                        continue;
                    }
                    if (var.isCo)
                    {
                        var.YourTurn = true;
                        for (int i = 0; i < 10; i++)
                        {
                            if (var.DbLoad)
                            {
                                break;
                            }
                            Thread.Sleep(40);
                        }
                        if (!var.DbLoad)
                        {
                            var.isCo = false;
                        }

                        nbclient++;
                        Console.WriteLine($"Client");
                        
                    }
                    else
                    {
                        var.database.tablecharacter.updateDatabase(var);
                    }
                    queue.Remove(var);
                }


            }

        }

        private void ThreadlistenClient(object o)
        {
            listenClient li = (listenClient)o;
            try
            {
                li.startlisten();
            }
            catch (Exception s)
            { Console.WriteLine(s); }
            nbclient--;
            Console.WriteLine("Client Deconnected");
            li.linkServer.RemoveAccount(li.account.ID,li.account.GUID);
            queue.Remove(li);
            li.remove(li);
            ListSauvegarde.Add(li);
            queue.Add(li);

        }
    }

}

