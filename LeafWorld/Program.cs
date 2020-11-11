using System;

namespace LeafWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.LoadDataBase database = new Database.LoadDataBase(); ;
            PacketGestion.PacketGestion.init();
            Network.WorldServer AuthServ = new Network.WorldServer(database);
        }
    }
}
