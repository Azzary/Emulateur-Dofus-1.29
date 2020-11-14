using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Database.table.Invetaire
{
    public class model_item
    {

        MySqlConnection conn;

        public Dictionary<int, Game.Item.Item> AllItems = new Dictionary<int, Game.Item.Item>();

        public model_item(MySqlConnection _conn)
        {
            conn = _conn;
            MySqlCommand Create_table = new MySqlCommand(@"
            CREATE TABLE IF NOT EXISTS `model_items`  (
          `id` int(11) NOT NULL DEFAULT -1,
          `type` int(11) NOT NULL DEFAULT -1,
          `name` varchar(50) NOT NULL DEFAULT '',
          `niveau` int(11) NOT NULL DEFAULT 1,
          `ModelStat` varchar(300) NOT NULL DEFAULT '',
          `pods` int(11) NOT NULL DEFAULT 0,
          `set` int(11) NOT NULL DEFAULT -1,
          `price` int(11) NOT NULL DEFAULT 0,
          `ogrines` int(11) NOT NULL DEFAULT 0,
          `ConditionForUse` varchar(100) NOT NULL DEFAULT '',
          `infosWeapon` varchar(100) NOT NULL DEFAULT '',
          `sell` int(32) NOT NULL DEFAULT 0,
          `halfPrice` int(32) NOT NULL DEFAULT 0,
          PRIMARY KEY (`id`))", conn);

            Create_table.ExecuteNonQuery();
            LoadItems();
        }

        private void LoadItems()
        {

            string r = "SELECT id, type, niveau, ModelStat," +
    "pods, price, ogrines, ConditionForUse, infosWeapon from model_items;";

            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        AllItems.Add((int)Reader["id"],
                            new Game.Item.Item((int)Reader["id"], (int)Reader["pods"], (int)Reader["niveau"], Convert.ToByte((int)Reader["type"]), (string)Reader["ModelStat"], (string)Reader["ConditionForUse"], (string)Reader["infosWeapon"]));

                    }
                }
                Reader.Close();
            }
        }

    }
}
