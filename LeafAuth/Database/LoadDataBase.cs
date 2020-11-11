using System;
using System.Data;

using MySql.Data.MySqlClient;


namespace LeafAuth.Database
{
    class LoadDataBase
    {
        public table.account tableaccount;
        public table.server tableserver;
        public LoadDataBase()
        {

            string connStr = "server = localhost; user = root; database = leafauth;";
            MySqlConnection conn = new MySqlConnection(connStr);
            Console.WriteLine("Opening Connection Database");
            conn.Open();
            Console.WriteLine("load Table:");
            Console.WriteLine("           Account...");
            tableaccount = new table.account(conn);
            Console.WriteLine("           Server...");
            tableserver = new table.server(conn);
        }



    }
}
