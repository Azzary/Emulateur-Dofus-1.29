using System;
using MySql.Data.MySqlClient;


namespace LeafWorld.Database
{
    public class LoadDataBase
    {
        public table.Character.Character tablecharacter;
        public table.Character.Spell tablespells;
        public table.MapTemplate tablemap;
        public table.Experience experience;
        public table.Invetaire.model_item model_item;
        public table.Item.items_personnage tableCharacterItems;
        public table.Character.CharacterSpell tableCharacterSpells;
        private int idOfCreation = 1;
        public LoadDataBase()
        {
            string connStr = $"server = localhost; user = root; database = leafworld{World.WorldConfig.ServerID};";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Opening Connection Database");
            conn.Open();
            Console.WriteLine("load Table:");
            Console.WriteLine("           Character...");
            tablecharacter = new table.Character.Character(conn);
            GetId(conn);
            tableCharacterSpells = new table.Character.CharacterSpell(conn);
            Console.WriteLine("           Spell...");
            tablespells = new table.Character.Spell(conn);
            Console.WriteLine("           Items...");
            model_item = new table.Invetaire.model_item(conn);
            tableCharacterItems = new table.Item.items_personnage(conn);
            Console.WriteLine("           Map...");
            tablemap = new table.MapTemplate(conn);
            Console.WriteLine("           Experience...");
            experience = new table.Experience(conn);
        }
        public int getidOfCreation()
        {
            idOfCreation++;
            return idOfCreation;
        }

        private void GetId(MySqlConnection conn)
        {
            string strSQL = "Select id from personnage";

            using (MySqlCommand commande = new MySqlCommand(strSQL, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                if (Reader.HasRows)
                {
                    while (Reader.Read())
                    {
                        if ((int)Reader["id"] > idOfCreation)
                            idOfCreation = (int)Reader["id"];

                    }
                }
                Reader.Close();
            }
        }
    }

}
