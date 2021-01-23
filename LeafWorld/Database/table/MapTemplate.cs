using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Database.table
{
    public class MapTemplate
    {
        public Dictionary<int, Game.Map.Map> Maps = new Dictionary<int, Game.Map.Map>();
        MySqlConnection conn;

        public MapTemplate(MySqlConnection _conn)
        {
            conn = _conn;
            MySqlCommand Create_table = new MySqlCommand(@"
            CREATE TABLE IF NOT EXISTS `maptemplate`  (
            `id` int(11) NOT NULL,
            `CreateTime` varchar(50) NOT NULL,
            `Width` int(2) NOT NULL DEFAULT -1,
            `Height` int(2) NOT NULL DEFAULT -1,
            `PosFight` varchar(300) NOT NULL DEFAULT '|',
            `DataKey` text  NOT NULL,
            `Data` text NOT NULL,
            `cells` text NOT NULL,
            `mobs` text NOT NULL,
            `X` int(3) NOT NULL,
            `Y` int(3) NOT NULL,
            `subArea` int(3) NOT NULL,
            `nroGrupo` int(2) NOT NULL DEFAULT 5,
            `maxMobs` int(2) NOT NULL DEFAULT 8,
            `Capaciter` int(5) NOT NULL DEFAULT 0,
            `descripcion` int(5) NOT NULL DEFAULT 0,
            PRIMARY KEY (`id`))", conn);
            Create_table.ExecuteNonQuery();
            get_map();
        }

        private void get_map()
        {
            string r = "SELECT Id, CreateTime, Data, DataKey," +
                "mobs, Width, Height, X, Y, Capaciter, maxMobs, PosFight," +
                "subArea from maptemplate;";

            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        Maps.Add((int)Reader["Id"], new Game.Map.Map(Reader["Id"].ToString(), Reader["CreateTime"].ToString(), Reader["Data"].ToString(), Reader["DataKey"].ToString(),
                            Reader["mobs"].ToString(), Reader["Width"].ToString(), Reader["Height"].ToString(), Reader["X"].ToString(),
                            Reader["Y"].ToString(), Reader["Capaciter"].ToString(), Reader["subArea"].ToString(), (int)Reader["maxMobs"], (string)Reader["PosFight"]));

                    }
                }
                Reader.Close();
            }
            r = "SELECT map, cell, args from cellTp;";
            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        int mapid = (int)Reader["map"];
                        if (Maps.ContainsKey(mapid))
                        {
                            int cellID = (int)Reader["cell"];
                            if (!Maps[mapid].CellTp.ContainsKey(cellID))
                            {
                                string[] strArgs = Reader["args"].ToString().Split(',');
                                int[] intArgs = new int[2] { Convert.ToInt32(strArgs[0]), Convert.ToInt32(strArgs[1])};
                                Maps[mapid].CellTp.Add(cellID, intArgs);
                            }
                        }
                        //     1947               
                        //    532
                    }
                }
                Reader.Close();
            }
        }
    }


}


