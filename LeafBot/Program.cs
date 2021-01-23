using System;
using System.Collections.Generic;
using System.Threading;

namespace LeafBot
{
    class Program
    {

        static void Main(string[] args)
        {
            List<Client.Client> listClient = new List<Client.Client>();
            Console.Write("number of clients: ");
            int nbClient = Convert.ToInt32(Console.ReadLine());
            Console.Clear();


            ConsoleNb spin = new ConsoleNb(nbClient);
            Console.WriteLine($"Creation in progress...");
            for (int i = 0; i <= nbClient; i++)
            {
                listClient.Add(new Client.Client(i, listClient));
                spin.Turn(i);
            }
            Console.Clear();
            Console.WriteLine("PressKey to start...");
            Console.ReadKey();
            new GestionClient(listClient);
        }

        public class ConsoleNb
        {
            int nbClient;
            public ConsoleNb(int nbclient)
            {
                nbClient = nbclient;
            }
            public void Turn(int i)
            {
                Console.Write(i+"/"+nbClient);
                Console.SetCursorPosition(Console.CursorLeft - (i.ToString()+ "/" + nbClient).Length, Console.CursorTop);
            }
        }
    }
}
