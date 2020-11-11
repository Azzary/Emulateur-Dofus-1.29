using LeafWorld.Network;
using Microsoft.Win32.SafeHandles;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Database.table.Character
{
    public class Character
    {
        MySqlConnection conn;
        public Character(MySqlConnection _conn)
        {
            conn = _conn;

            MySqlCommand Create_table = new MySqlCommand(@"
                    CREATE TABLE IF NOT EXISTS `personnage` 
                    (id INT NOT NULL AUTO_INCREMENT,
                    accountid int NOT NULL,
                    level int DEFAULT 1,
                    classe int NOT NULL,
                    sexe int NOT NULL,
                    speudo TEXT(20) NOT NULL,
                    couleur1 int NOT NULL,
                    couleur2 int NOT NULL,
                    couleur3 int NOT NULL,
                    pods int NOT NULL DEFAULT 0,
                    gfxID int NOT NULL,
                    isDead int DEFAULT 0,
                    SubArea int DEFAULT 7669,
                    mapID int DEFAULT 10295,
                    cellID int DEFAULT 299,
                    XP int DEFAULT 0,
                    kamas int DEFAULT 1000,
                    capital int DEFAULT 0,
                    PSorts int DEFAULT 0,
                    vie int DEFAULT 50,
                    energie int DEFAULT 10000,
                    PA int DEFAULT 6,
                    PM int DEFAULT 3,
                    forcee int DEFAULT 0,
                    sagesse int DEFAULT 0,
                    chance int DEFAULT 0,
                    agi int DEFAULT 0,
                    intell int DEFAULT 0,
                    podsMax int NOT NULL DEFAULT 1000,
                    PRIMARY KEY (id)) ", conn);
            Create_table.ExecuteNonQuery();
        }

        public void CreateCharacter(int accountID, Game.account.character.Character charac)
        {
            string cmd = "INSERT INTO personnage SET ";
            if (!charac.newCharac)
            {
                cmd += "id=" + charac.id + ",";
            }

            cmd += "accountid=" + accountID + "," +
                "speudo='" + charac.speudo + "'," +
                "classe=" + charac.classe + "," +
                "sexe=" + charac.sexe + "," +
                "couleur1=" + charac.couleur1 + "," +
                "couleur2=" + charac.couleur2 + "," +
                "couleur3=" + charac.couleur3 + "," +
                "gfxID=" + charac.gfxID + "," +
                "cellID=" + charac.cellID + "," +
                "mapID=" + charac.mapID + "," +
                "pods=" + charac.pods + "," +
                "podsMax=" + charac.podsMax + "," +
                "XP=" + charac.XP + "," +
                "kamas=" + charac.kamas + "," +
                "capital=" + charac.capital + "," +
                "PSorts=" + charac.PSorts + "," +
                "vie=" + charac.CaracVie + "," +
                "PA=" + charac.PA + "," +
                "PM=" + charac.PM + "," +
                "energie=" + charac.energie + "," +
                "forcee=" + charac.force + "," +
                "sagesse=" + charac.sagesse + "," +
                "chance=" + charac.chance + "," +
                "agi=" + charac.agi + "," +
                "intell=" + charac.intell + "";

            MySqlCommand command = new MySqlCommand(cmd, conn);
            command.ExecuteNonQuery();

        }

        public void getCharacter(listenClient prmClient)
        {
            string r = "SELECT id, speudo, level, gfxID, classe," +
            " couleur1, couleur2, couleur3, isDead, mapID, cellID," +
            "sexe, pods, podsMax, XP, PA,PM, kamas, capital, PSorts," +
            " vie, forcee, sagesse, chance, agi, intell, energie " +
            "from personnage where accountid='" + prmClient.account.ID + "';";

            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();

                if (Reader.HasRows)
                {
                    bool flag = false;
                    while (Reader.Read())
                    {
                        foreach (Game.account.character.Character item in prmClient.account.ListCharacter)
                        {
                            if (item.id == (int)Reader["id"])
                            {
                                flag = true;
                                break;
                            }
                        }
                        if (!flag)
                        {
                            Game.account.character.Character character = new Game.account.character.Character((int)Reader["id"], (string)Reader["speudo"], (int)Reader["level"],
                               (int)Reader["isDead"], (int)Reader["gfxID"], (int)Reader["cellID"], (int)Reader["mapID"], (int)Reader["couleur1"],
                               (int)Reader["couleur2"], (int)Reader["couleur3"], (int)Reader["sexe"], Convert.ToByte((int)Reader["classe"]), (int)Reader["pods"], (int)Reader["podsMax"],
                               (int)Reader["XP"], (int)Reader["kamas"], (int)Reader["capital"], (int)Reader["PSorts"], (int)Reader["vie"], (int)Reader["energie"], (int)Reader["PA"],
                               (int)Reader["PM"], (int)Reader["forcee"], (int)Reader["sagesse"], (int)Reader["chance"], (int)Reader["agi"], (int)Reader["intell"], false);

                            prmClient.account.ListCharacter.Add(character);
                        }

                    }
                }

                Reader.Close();
                prmClient.database.tableCharacterSpells.LoadSpells(prmClient);

            }
        }


        private void removeSpell(int id)
        {
           string r = "DELETE from characterSpells WHERE" +
            " characterID='" + id + "';";
            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                Reader.Close();
            }
            
        }

        private void AddSpells(int id, List<Game.Spell.Spell> spells)
        {
            string r = "";
            foreach (Game.Spell.Spell item in spells)
            {
                r += "INSERT INTO characterSpells SET characterID=" + id + "," +
                "id=" + item.id + "," +
                "level=" + item.level + ";";

            }
            if (r == "")
                return;

            
            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {

                MySqlDataReader Reader = commande.ExecuteReader();
                Reader.Close();
            }

        }

        public void updateDatabase(listenClient prmClient)
        {
            foreach (Game.account.character.Character item in prmClient.account.ListRemoveCharacter)
            {
                removeCharacter(item.id.ToString());
                removeSpell(item.id);
            }

            foreach (Game.account.character.Character item in prmClient.account.ListCharacter)
            {
                removeCharacter(item.id.ToString());
                removeSpell(item.id);
                CreateCharacter(prmClient.account.ID, item);
                AddSpells(item.id, item.SpellsCharacter);
            }
        }

        public void removeCharacter(string id)
        {
            string r = "DELETE from personnage WHERE" +
            " id='" + id + "';";
            using (MySqlCommand commande = new MySqlCommand(r, conn))
            {
                MySqlDataReader Reader = commande.ExecuteReader();
                Reader.Close();
            }
        }
    }
    
}
