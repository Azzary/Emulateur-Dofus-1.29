using LeafWorld.Network;
using LeafWorld.PacketGestion;
using System;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using System.Security;
using System.Text;

namespace LeafWorld.Game.Map
{


    class MapMouvement
    {
        //horizontal,vertical,diagonal
        public static double[] WALK_SPEEDS = {0.75, 0.5, 0.5};
        public static double[] RUN_SPEEDS = {0.3, 0.2, 0.2 };
        //public static double[] MOUNT_SPEEDS = { 2.300000E-001, 2.000000E-001, 2.000000E-001, 2.000000E-001, 2.300000E-001, 2.000000E-001, 2.000000E-001, 2.000000E-001 };
        int[] ListDir = new int[] { 0, 1, 2, 3, 4, 5, 6, 7 };
        List<int> ListDirFight = new List<int> { 1, 3, 5, 7 };

        [PacketAttribute("GA001")]
        public void ConfirmMove(Network.listenClient prmClient, string prmPacket)
        {


            if (DateTimeOffset.Now.ToUnixTimeSeconds() < prmClient.account.character.WaitMoving)
            {
                
                return;
            }
            prmPacket = prmPacket.Substring(5).Split('\0')[0];

            if (!prmClient.account.character.fight.YourTurn && prmClient.account.character.fight.InFight == 2 || !prmClient.account.character.IsAvailable && prmClient.account.character.fight.InFight != 2
                || prmClient.account.character.fight.InFight == 2 && prmPacket.Length/3 > prmClient.account.character.TotalPM)
            {
                return;
            }
            Cell.Cell Cell = prmClient.account.character.Map.Cells[prmClient.account.character.cellID];
            if (prmClient.account.character.fight.InFight == 2)
                Cell = prmClient.account.character.Map.Cells[prmClient.account.character.fight.FightCell];


            Map map = prmClient.account.character.Map;
            

            List<int> ListCell = new List<int>();
            string chemain = "";
            for (int i = 0; i < prmPacket.Length; i += 3)
            {
                int dir = Util.GetDirNum(prmPacket.Substring(i, 1));
                int CellSend = Util.CharToCell(prmPacket.Substring(i+1, 2));
                if (prmClient.account.character.fight.InFight == 2)
                {
                    if (!ListDirFight.Contains(dir))
                        return;
                    for (int nb = 0; nb < prmClient.account.character.fight.EntityInFight.Count; nb++)
                    {
                        if (prmClient.account.character.fight.EntityInFight[nb].account.character.fight.FightCell == CellSend)
                            return;
                    }

                }
                while (true)
                {
                    int index = Pathfinding.NextCell(map, Cell.ID, dir);
                    if (index < 0 || index > map.Cells[map.Cells.Count - 1].ID)
                    {
                        prmClient.send($"BN\0GA0; 1;{prmClient.account.character.id}; b{Util.CellToChar(prmClient.account.character.cellID)}");
                        return;
                    }
                    Cell = prmClient.account.character.Map.Cells[index];

                    if (!(!Cell.UnWalkable || !Cell.Door) || (Cell.Paddock && Cell.ID != CellSend))
                    {
                        prmClient.send($"BN\0GA0; 1;{prmClient.account.character.id}; b{Util.CellToChar(prmClient.account.character.cellID)}");
                        return;
                    }
                    ListCell.Add(Cell.ID);
                    if (Cell.ID == CellSend)
                    {
                        if (!Cell.Paddock)
                            chemain += Util.GetDirChar(dir) + Util.CellToChar(Cell.ID);
                        else
                        {
                            //use iteractif
                        }
                        break;
                    }
                    

                }
                
            }

            List<listenClient> CharactersOnMap;
            if (prmClient.account.character.fight.InFight == 2)
            {
                if (ListCell.Count > prmClient.account.character.TotalPM)
                    return;
                chemain = ($"GA0;1;{prmClient.account.character.id};b{Util.CellToChar(prmClient.account.character.fight.FightCell)}{chemain}");
                prmClient.account.character.TotalPM -= ListCell.Count;
                chemain += $"\0GA;129;{prmClient.account.character.id};{prmClient.account.character.id},-{ListCell.Count}";
                CharactersOnMap = prmClient.account.character.fight.EntityInFight;
                prmClient.account.character.fight.FightCell = Util.CharToCell(chemain.Substring(chemain.Length - 2));
            }
            else
            {
                chemain = ($"GA0;1;{prmClient.account.character.id};b{Util.CellToChar(prmClient.account.character.cellID)}{chemain}");
                CharactersOnMap = map.CharactersOnMap;
                prmClient.account.character.cellID = Util.CharToCell(chemain.Substring(chemain.Length - 2));
            }
            prmClient.account.character.ListCellMove = ListCell;
            prmClient.account.character.WaitMoving = DateTimeOffset.Now.ToUnixTimeSeconds() + timing(ListCell, map);
            prmClient.account.character.IsAvailable = false;
            Console.WriteLine(CharactersOnMap.Count);
            for (int i = 0; i < CharactersOnMap.Count; i++)
            {
                CharactersOnMap[i].send(chemain);
            }

        }




        [PacketAttribute("GKK0")]
        public void FinDeplacement(Network.listenClient prmClient, string prmPacket)
        {
            if (prmClient.account.character.fight.InFight != 0)
            {
                if (prmClient.account.character.fight.InFight == 2)
                {
                    for (int i = 0; i < prmClient.account.character.fight.EntityInFight.Count; i++)
                    {
                        prmClient.account.character.fight.EntityInFight[i].send("GAF" + prmClient.account.character.id);
                    }
                    
                }
                return;
            }

            if (DateTimeOffset.Now.ToUnixTimeSeconds() < prmClient.account.character.WaitMoving)
            {
                prmClient.account.character.WaitMoving = DateTimeOffset.Now.ToUnixTimeSeconds() + 10000000;
                return;
            }

            if (prmClient.account.character.Map.CellTp.ContainsKey(prmClient.account.character.cellID))
            {
                int[] arg = prmClient.account.character.Map.CellTp[prmClient.account.character.cellID];

                SwitchMap(prmClient, arg[0], arg[1]);
            }
            prmClient.account.character.IsAvailable = true;
            prmClient.account.character.ListCellMove.Clear();
        }
        
        public static void SwitchMap(Network.listenClient prmClient, int mapID, int CellID)
        {
            Map NewMap = prmClient.database.tablemap.Maps[mapID];
            prmClient.account.character.Map.CharactersOnMap.Remove(prmClient);
            NewMap.CharactersOnMap.Add(prmClient);
            Map LastMap = prmClient.account.character.Map;

            for (int i = 0; i < LastMap.CharactersOnMap.Count; i++)
            {
                LastMap.CharactersOnMap[i].send($"GM|-{prmClient.account.character.id}");
            }
            prmClient.account.character.Map = NewMap;
            prmClient.account.character.mapID = NewMap.Id;
            prmClient.account.character.cellID = CellID;
            prmClient.send($"GDM|{NewMap.Id}|{NewMap.CreateTime}|{NewMap.DataKey}");

        }

        [PacketAttribute("GKE0")]
        public void SwitchMoving(Network.listenClient prmClient, string prmPacket)
        {
            prmPacket = prmPacket.Substring(5).Split("\0")[0];
            if (int.TryParse(prmPacket, out int var))
            {
                List<int> parthTemp = prmClient.account.character.ListCellMove;
                for (int i = 0; i < parthTemp.Count - 1; i++)
                {
                    if (var == parthTemp[i])
                    {
                        prmClient.account.character.cellID = parthTemp[i];
                        prmClient.account.character.WaitMoving = DateTimeOffset.Now.ToUnixTimeSeconds()-1;
                        prmClient.account.character.IsAvailable = true;
                        for (int x = 0; x < prmClient.account.character.Map.CharactersOnMap.Count; x++)
                        {
                            prmClient.account.character.Map.CharactersOnMap[x].send($"GA0;1;{prmClient.account.character.id};b{Util.CellToChar(prmClient.account.character.cellID)}");
                        }
                        return;
                    }

                }

            }
        }

        private double timing(List<int> path, Map map)
        {
            double run_time = 0;
            double[] time;
            if (path.Count > 2)
            {
                time = RUN_SPEEDS;
            }
            else
            {
                time = WALK_SPEEDS;
            }
            for (int i = 0; i < path.Count- 1; i++)
            {
                int dir = find_direction(map, path[i], path[i+1]);
                if (dir == 2 || dir == 6)
                {
                    run_time += time[1];
                }
                else if (dir == 0 || dir == 4)
                {
                    run_time += time[0];
                }
                else
                {
                    run_time += time[2];
                }

            }

            return run_time - 1;
        }

        private int find_direction(Map map,int startCell, int endCell)
        {
            foreach (int dir in ListDir)
            {
                int Cell = Pathfinding.NextCell(map, startCell, dir);
                if (Cell == endCell)
                {
                    return dir;
                }
            }
            return 0;
        }
    }
    

}
