using LeafWorld.Network;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Database.table.Character
{
    public class CharacterSpell
    {

        MySqlConnection conn;

        public CharacterSpell(MySqlConnection _conn)
        {
            conn = _conn;
            MySqlCommand Create_table = new MySqlCommand(
           @"CREATE TABLE IF NOT EXISTS `characterSpells` (
          `characterID` int(11) NOT NULL,
          `level` int(11) NOT NULL,
          `id` int(11) NOT NULL)", conn);

            Create_table.ExecuteNonQuery();
        }

        public void LoadSpells(listenClient prmClient)
        {
            foreach (Game.account.character.Character character in prmClient.account.ListCharacter)
            {
                string r = "SELECT id, level from characterSpells where characterID='" + character.id + "'";


                using (MySqlCommand commande = new MySqlCommand(r, conn))
                {
                    MySqlDataReader Reader = commande.ExecuteReader();
                    if (Reader.HasRows)
                    {
                        while (Reader.Read())
                        {
                            character.SpellsCharacter.Add(new Game.Spell.Spell((int)Reader["id"], (int)Reader["level"])); 

                        }
                    }
                    Reader.Close();
                }
            }
        }

    }
}
