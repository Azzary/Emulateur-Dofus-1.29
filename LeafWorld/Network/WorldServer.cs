using LeafWorld.Database;
using Org.BouncyCastle.Cms;
using System;
using System.Collections.Generic;
using System.Drawing.Printing;
using System.IO;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;
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

            server.Bind(new IPEndPoint(IPAddress.Parse(World.WorldConfig.IP), World.WorldConfig.PORT));
            server.Listen(5);
            Console.WriteLine($"Waiting Connection. {World.WorldConfig.IP}:{World.WorldConfig.PORT}");
            AcceptConnection(server);

        }

        List<listenClient> ListensClients = new List<listenClient>();

        private void AcceptConnection(object o)
        {
            new Thread(wait_queue).Start();
            Socket server = (Socket)o;
            while (true)
            {
                Socket socketClient = server.Accept();
                listenClient li = new listenClient(socketClient, DataBase, queue, linkServer, CharacterInWorld);
                new Thread(ThreadlistenClient).Start(li);
                queue.Add(li);
            }

        }

       private void wait_queue()
        {
            while (true)
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
                            if (var.IsLoadDb)
                            {
                                while (!var.DbLoad && var.isCo)
                                {
                                    Thread.Sleep(40);
                                }
                            }
                            else
                            {
                                var.isCo = false;
                            }
                            
                        }
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
            
            catch (Exception ex)
            {
                StringBuilder sb = new StringBuilder();
                sb.Append(ex);
                string path = Directory.GetCurrentDirectory() + "\\log\\" + li.account.ID + "_log.txt";
                using (StreamWriter sw = new StreamWriter(path, true))
                {
                    sw.WriteLine(ex + "\n");
                }


                File.AppendAllText(path, sb.ToString());
                sb.Clear();
            }


            Console.WriteLine("Client Deconnected");
            queue.Remove(li);
            li.remove(li);
            //ListSauvegarde.Add(li);
            queue.Add(li);


        }
    }

}

