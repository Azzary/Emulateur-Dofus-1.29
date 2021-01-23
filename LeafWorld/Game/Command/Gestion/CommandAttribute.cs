using System;

namespace LeafWorld.Game.Command.Gestion
{
    class CommandAttribute : Attribute
    {
        public string Command;
        public short Role;
        public short MinimalLen;

        public CommandAttribute(short _Role, string _Command, short _MinimalLen) 
        {
            Command = _Command;
            Role = _Role;
            MinimalLen = _MinimalLen;
        }

    }
}
