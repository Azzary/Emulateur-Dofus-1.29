using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.CreateCharacter
{
    class NameGen
    {

        [PacketAttribute("AP")]
        public void GenName(Network.listenClient prmClient, string prmPacket)
        {

            Random r = new Random();
            int len = r.Next(3, 8);
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "l", "n", "p", "q", "r", "s", "sh", "zh", "t", "v", "w", "x" };
            string[] vowels = { "a", "e", "i", "o", "u", "ae", "y" };
            string Name = "";
            Name += consonants[r.Next(consonants.Length)].ToUpper();
            Name += vowels[r.Next(vowels.Length)];
            int b = 2; //b tells how many times a new letter has been added. It's 2 right now because the first two letters are already in the name.
            while (b < len)
            {
                Name += consonants[r.Next(consonants.Length)];
                b++;
                Name += vowels[r.Next(vowels.Length)];
                b++;
            }

            prmClient.send("APK" + Name);
        }

    }
}
