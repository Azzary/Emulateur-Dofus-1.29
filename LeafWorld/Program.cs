using System;
using System.IO;

namespace LeafWorld
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.LoadDataBase database = new Database.LoadDataBase();
            //SynMap(database);
            PacketGestion.PacketGestion.init();
            Game.Command.Command.init();
            Network.WorldServer WorldServ = new Network.WorldServer(database);
        }

        public static void SynMap(Database.LoadDataBase database)
        {
            DirectoryInfo d = new DirectoryInfo(@"C:\wamp64\www\dofus\maps");
            FileInfo[] Files = d.GetFiles("*.swf"); //Getting Text files
            foreach (FileInfo file in Files)
            {

                if (Int32.TryParse(file.Name.Split("_")[0], out int res))
                {

                    if (database.tablemap.Maps.ContainsKey(res))
                    {
                        if (database.tablemap.Maps[res].CreateTime + ".swf" != file.Name.Split("_")[1] && file.Name.Substring(file.Name.Length - 5) == "X.swf")
                        {
                            bool x = File.Exists(file.Directory + $"\\{res}_{database.tablemap.Maps[res].CreateTime}X.swf");
                            if (x == false)
                            {

                                try
                                {
                                    File.Move(file.FullName, file.Directory + $"\\{res}_{database.tablemap.Maps[res].CreateTime}X.swf");
                                }
                                catch (Exception)
                                {

                                    Console.WriteLine(file.Name);
                                }

                            }

                        }


                    }
                }




            }
            Console.WriteLine("Check Map End");
            Console.ReadKey();
            return;
        }
    }
}


