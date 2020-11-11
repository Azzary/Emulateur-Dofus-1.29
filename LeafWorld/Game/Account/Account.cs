using System;
using System.Collections.Generic;
using System.Text;


namespace LeafWorld.Game.account
{
    public class Account
    {
        public int statue = 0;
        public int ID { get; set; }
        public List<character.Character> ListCharacter = new List<character.Character>();

        public List<character.Character> ListRemoveCharacter = new List<character.Character>();

        public character.Character character;
        public string GUID { get; set; }
        

    }
}
