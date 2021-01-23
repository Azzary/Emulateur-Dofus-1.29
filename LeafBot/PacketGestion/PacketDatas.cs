using System.Reflection;



namespace LeafBot.PacketGestion
{
    public class PacketDatas
    {
        public object instance { get; set; }
        public string name_packet { get; set; }
        public MethodInfo information { get; set; }

        public PacketDatas(object _instance, string _name_packet, MethodInfo _information)
        {
            instance = _instance;
            name_packet = _name_packet;
            information = _information;
        }
    }
}