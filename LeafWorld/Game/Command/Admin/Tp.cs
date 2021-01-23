using LeafWorld.Game.Command.Gestion;
using System;

namespace LeafWorld.Game.Command.Admin
{
    public class Tp
    {
        [CommandAttribute(3, "tpTo", 1)]
        public void TpMap(Network.listenClient client, string command)
        {
            string InfoTP = command.Split(' ')[1];
            int MapID = -1;
            int cellID = 0;
            if (Int32.TryParse(InfoTP, out MapID))
            {
                if (client.database.tablemap.Maps.ContainsKey(MapID))
                    cellID = client.database.tablemap.Maps[MapID].Cells.Find(x => x.UnWalkable).ID;
            }
            else
            {
                Network.listenClient TpTo = client.CharacterInWorld.Find(x => x.account.character.speudo == InfoTP);
                if (TpTo != null)
                {
                    MapID = TpTo.account.character.Map.Id;
                    cellID = TpTo.account.character.cellID;
                }
            }
            if (MapID != -1) 
                Map.MapMouvement.SwitchMap(client, MapID, cellID) ;
        }


        [CommandAttribute(3, "tpHere", 1)]
        public void TpPlayer(Network.listenClient client, string command)
        {
            string speudo = command.Split(' ')[1];
            Network.listenClient TpTo = client.CharacterInWorld.Find(x => x.account.character.speudo == speudo);
            if (TpTo != null)
                Map.MapMouvement.SwitchMap(TpTo, client.account.character.Map.Id, client.account.character.cellID);
        }
    }
}
