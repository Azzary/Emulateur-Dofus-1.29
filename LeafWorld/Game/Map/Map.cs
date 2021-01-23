using LeafWorld.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeafWorld.Game.Map
{
    public class Map
    {
            public Dictionary<int, int[]> CellTp;

            public List<Cell.Cell> Cells;
            public Dictionary<int,Fight.Fight> FightInMap = new Dictionary<int, Fight.Fight>();

            public List<Network.listenClient> CharactersOnMap;
            public int Id { get; set; }
            public string CreateTime { get; set; }
            public string Data { get; set; }
            public string DataKey { get; set; }
            public string mobs { get; set; }
            public int Width { get; set; }
            public int Height { get; set; }
            public string X { get; set; }
            public string Y { get; set; }
            public string Capabilities { get; set; }
            public string SubArea { get; set; }
            public int MaxMobs { get; set; }
            
            public string PosFight { get; set; }
        public Map(string _id, string _createTime, string _data, string _datakey, string _mobs, string _width, string _height,
            string _x, string _y, string _capabilities, string _subAreaId, int _maxMobs, string _PosFight)
            {
                PosFight = _PosFight;
                CellTp = new Dictionary<int, int[]>();
                CharactersOnMap = new List<Network.listenClient>();
                Id = int.Parse(_id);
                CreateTime = _createTime;
                DataKey = _datakey;
                Data = _data;
                mobs = _mobs;
                Width = int.Parse(_width);
                Height = int.Parse(_height);
                X = _x;
                Y = _y;
                Capabilities = _capabilities;
                SubArea = _subAreaId;
                MaxMobs = _maxMobs;
                Cells = new List<Cell.Cell>();
                UncompressDatas();
            }
        
            public void Remove(listenClient prmClient)
            {
                account.character.Character character;
                prmClient.account.character.Map.CharactersOnMap.Remove(prmClient);
                for (int i = 0; i < prmClient.account.character.Map.CharactersOnMap.Count; i++)
                {
                    character = prmClient.account.character.Map.CharactersOnMap[i].account.character;
                    prmClient.account.character.Map.CharactersOnMap[i].send($"GM|-{prmClient.account.character.id}");
                }
            }


        public void UncompressDatas()
        {
            string data = Data;
            for (int i = 0; i < data.Length; i += 10)
            {
                string CurrentCellData = data.Substring(i, 10);
                Cells.Add(new Cell.Cell(i/10, CurrentCellData));
            }

        }
    }
}


