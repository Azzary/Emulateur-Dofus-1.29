using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Database.table.Item
{
    public  class items_personnage
    {
        MySqlConnection conn;
        public items_personnage(MySqlConnection _conn)
        {
            conn = _conn;
            MySqlCommand Create_table = new MySqlCommand(@"
            CREATE TABLE IF NOT EXISTS `items_personnage`(
            `uid` int (11) NOT NULL AUTO_INCREMENT,
            `modelid` int (11) NOT NULL,
            `personnageid` int (11) NOT NULL,
            `position` int (11) NOT NULL,
            `stats` text NOT NULL,
            `objectif` int (11) NOT NULL DEFAULT 0,
            `price` bigint(31) NOT NULL DEFAULT 0,
            PRIMARY KEY(`uid`))", conn);

            Create_table.ExecuteNonQuery();
        }

        public void LoadItems(Network.listenClient prmClient)
        {
            foreach (Game.account.character.Character character in prmClient.account.ListCharacter)
            {
                string r = $"SELECT uid, modelid, stats, position from items_personnage where personnageid = {character.id};";


                using (MySqlCommand commande = new MySqlCommand(r, conn))
                {
                    MySqlDataReader Reader = commande.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            if (prmClient.database.model_item.AllItems.ContainsKey((int)Reader["modelid"]))
                            {
                                Game.Item.Item item = prmClient.database.model_item.AllItems[(int)Reader["modelid"]];
                                if (item.Type < 0)
                                    return;

                                    character.Invertaire.Stuff.Add(new Game.Item.Stuff(
                                        (int)Reader["modelid"],
                                        (int)Reader["uid"],
                                        item.Niveau,
                                        item.Type,
                                        (int)Reader["position"],
                                        (string)Reader["stats"],
                                        item.Condition,
                                        item.infosWeapon));

                            }
                            
                            
                        }
                    }
                    Reader.Close();
                }
            }
        }

    }
}
