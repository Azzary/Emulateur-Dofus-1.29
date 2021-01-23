using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq;
using LeafWorld.PacketGestion;

namespace LeafWorld.Game.Command.Gestion
{
    class CommandGestion
    {
        public static readonly List<CommandDatas> metodos = new List<CommandDatas>();


        public static void init()
        {
            Assembly asm = typeof(Frame).GetTypeInfo().Assembly;

            foreach (MethodInfo type in asm.GetTypes().SelectMany(x => x.GetMethods()).Where(m => m.GetCustomAttributes(typeof(CommandAttribute), false).Length > 0))
            {
                CommandAttribute attribute = type.GetCustomAttributes(typeof(CommandAttribute), true)[0] as CommandAttribute;
                Type type_string = Type.GetType(type.DeclaringType.FullName);
                
                object instance = Activator.CreateInstance(type_string, null);
                metodos.Add(new CommandDatas(instance, attribute.Command, type, attribute.Role, attribute.MinimalLen));
            }
        }


        public static bool Gestion(Network.listenClient client, string command)
        {
            string[] CommandSplit = command.Split(' ');
            CommandDatas method = metodos.Find(m => CommandSplit[0].Substring(1) == m.name_command);
            try
            {
                if (method != null && client.account.Role >= method.RoleNeeded && CommandSplit.Length - 1 >= method.MinimalLen)
                {
                    method.information.Invoke(method.instance, new object[2] { client, command });
                    return true;
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }


    }
}
