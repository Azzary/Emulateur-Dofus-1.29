using System.Reflection;



namespace LeafWorld.Game.Command.Gestion
{
    public class CommandDatas
    {
        public object instance { get; set; }
        public string name_command { get; set; }
        public short RoleNeeded { get; set; }
        public short MinimalLen { get; set; }
        public MethodInfo information { get; set; }

        public CommandDatas(object _instance, string _name_packet, MethodInfo _information, short _RoleNeeded, short _MinimalLen)
        {
            instance = _instance;
            name_command = _name_packet;
            information = _information;
            RoleNeeded = _RoleNeeded;
            MinimalLen = _MinimalLen;
        }
    }
}