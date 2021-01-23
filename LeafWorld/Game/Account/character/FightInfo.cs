using LeafWorld.Network;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.account.character
{
    public class Fight
    {
        public int FightID { get; set; }
        public int FightCell { get; set; }
        public bool YourTurn { get; set; }
        public Network.listenClient RequestDuel { get; set; }

        public bool IsReady { get; set; }
        public bool IsLauncherDuel { get; set; }

        //0 = not in fight 1 = In pre Fight 2 = in fight
        public int InFight { get; set; }

        public int PosFight { get; set; }
        public short equipeID { get; set; }

        public List<int> ListCellIDPlacement = new List<int>();

        public List<listenClient> EntityInFight = new List<listenClient>();

        public Fight()
        {
            YourTurn = false;
            IsReady = false;
            InFight = 0;
            IsLauncherDuel = false;
        }

        public void AtributePosFight(listenClient prmClient)
        {
            prmClient.account.character.fight.FightCell = -1;
            bool flag = true;
            string cells = prmClient.account.character.Map.PosFight.Split("|")[equipeID];
            for (int i = 0; i < cells.Length; i+=2)
            {
                ListCellIDPlacement.Add(Util.CharToCell(cells.Substring(i, 2)));
                if (flag)
                {
                    flag = false;
                    for (int x = 0; x < EntityInFight.Count; x++)
                    {
                        if (EntityInFight[x].account.character.fight.FightCell == ListCellIDPlacement[i / 2])
                        {
                            flag = true;
                            break;
                        }
                    }
                    if (!flag || EntityInFight.Count == 0)
                    {
                        prmClient.account.character.fight.FightCell = ListCellIDPlacement[i / 2];
                    }

                }

            }
        }
    }
}
