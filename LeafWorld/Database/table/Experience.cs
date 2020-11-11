using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Database.table
{
    public class Experience
    {

        MySqlConnection conn;
        
        public Dictionary<int, Int64[]> ExperienceStat = new Dictionary<int, Int64[]>();

        public Experience(MySqlConnection _conn)
        {
            conn = _conn;
            MySqlCommand Create_table = new MySqlCommand(@"
            CREATE TABLE IF NOT EXISTS `experience`
            (`niveau` int NOT NULL,
            `personnage` int NOT NULL,
            `job` int NOT NULL,
            `dragodinde` int NOT NULL,
            `guilde` int NOT NULL,
            `pvp` int NOT NULL,
            `incarnation` int NOT NULL,
            PRIMARY KEY (`niveau`))", conn);

            Create_table.ExecuteNonQuery();
            LoadExp();
        }

        private void LoadExp()
        {
            string r = "SELECT niveau, personnage, dragodinde, guilde," +
    "pvp, job, incarnation from experience;";


            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        ExperienceStat.Add((int)Reader["niveau"],
                            new Int64[]{ (Int64)Reader["personnage"],
                                       (int)Reader["job"],
                                       (int)Reader["dragodinde"],
                                       (Int64)Reader["guilde"],
                                       (int)Reader["pvp"],
                                       (int)Reader["incarnation"]});

                    }
                }
                Reader.Close();
            }
        }

    }
}
