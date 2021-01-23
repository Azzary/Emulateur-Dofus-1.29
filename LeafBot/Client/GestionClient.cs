using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace LeafBot
{
    class GestionClient
    {
        List<Client.Client> listClients;
        Program.ConsoleNb nb;
        public GestionClient(List<Client.Client> listclients)
        {
            PacketGestion.PacketGestion.init();
            listClients = listclients;
            nb = new Program.ConsoleNb(listClients.Count - 1);
            Start();
        }

        void LoadAcount()
        {
            for (int i = 0; i < listClients.Count; i++)
            {
                try
                {
                    nb.Turn(i);
                    new Thread(listClients[i].StartConnection).Start();
                    Thread.Sleep(10);
                }
                catch (Exception) { }

            }
        }

        void Start()
        {
            Console.Clear();
            Console.WriteLine("Conect to the Server...");
            
            new Thread(LoadAcount).Start();

            List<List<Client.Client>> chunks = SplitClient(listClients, 200);
            for (int i = 0; i < chunks.Count-1; i++)
            {
                new Thread(listen).Start(chunks[i]);
            }
            listen(chunks[chunks.Count - 1]);

        }

        void listen(object o)
        {
            List<Client.Client> ChunklistClients = (List<Client.Client>)o;
            while (true)
            {
                for (int i = 0; i < ChunklistClients.Count; i++)
                {
                    try
                    {
                        ChunklistClients[i].Recv();
                    }
                    catch (Exception ex)
                    {
                    }

                }
            }

        }

        public static List<List<Client.Client>> SplitClient(List<Client.Client> source, int chunksize)
        {
            List<List<Client.Client>> chunk = new List<List<Client.Client>>();
            List<Client.Client> temp = new List<Client.Client>();
            int index = chunksize;
            for (int i = 0; i < source.Count; i++)
            {
                if (i == index)
                {
                    index += chunksize;
                    chunk.Add(new List<Client.Client>(temp));
                    temp.Clear();
                }
                else
                {
                    temp.Add(source[i]);
                }
            }
            if (temp.Count != 0)
            {
                chunk.Add(new List<Client.Client>(temp));
            }
            return chunk;
        }
    }
}
