using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Database.table.Character
{
    public class Spell
    {

        MySqlConnection conn;

        public Dictionary<int, Game.Spell.SpellData> SpellList = new Dictionary<int, Game.Spell.SpellData>();

        public Spell(MySqlConnection _conn)
        {
            conn = _conn;
            MySqlCommand Create_table = new MySqlCommand(@"
            CREATE TABLE IF NOT EXISTS `spell` (
          `id` int(11) NOT NULL,
          `name` varchar(100)  NOT NULL,
          `sprite` int(11) NOT NULL,
          `spriteinf` text NOT NULL,
          `level1` text  NOT NULL,
          `level2` text  NOT NULL,
          `level3` text  NOT NULL,
          `level4` text  NOT NULL,
          `level5` text  NOT NULL,
          `level6` text  NOT NULL,
          `affected` text NOT NULL,
          `description` text NOT NULL,
           PRIMARY KEY (`id`))", conn);

            Create_table.ExecuteNonQuery();
            LoadSpells();
        }

        private void LoadSpells()
        {
            string r = "SELECT id, level1, level2, level3," +
    "level4, level5, level6 from spell;";


            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        SpellList.Add((int)Reader["id"],new Game.Spell.SpellData((int)Reader["id"], (string)Reader["level1"], (string)Reader["level2"], (string)Reader["level3"],
                            (string)Reader["level4"], (string)Reader["level5"], (string)Reader["level6"]));

                    }
                }
                Reader.Close();
            }
        }

    }
}
