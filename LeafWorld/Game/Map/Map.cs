using LeafWorld.Network;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld.Game.Map
{
    public class Map
    {
            public Dictionary<int, string> CellTp;

            public List<int> Cells;
            public List<Fight.Fight> FightInMap = new List<Fight.Fight>();
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
                CellTp = new Dictionary<int, string>();
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
                Cells = UncompressDatas();
            }
        
            public void remove(listenClient prmClient)
            {
                account.character.Character character;
                prmClient.account.character.Map.CharactersOnMap.Remove(prmClient);
                for (int i = 0; i < prmClient.account.character.Map.CharactersOnMap.Count; i++)
                {
                    character = prmClient.account.character.Map.CharactersOnMap[i].account.character;
                    prmClient.account.character.Map.CharactersOnMap[i].send($"GM|-{prmClient.account.character.id}");
                }
            }


        private List<int> UncompressDatas()
        {
            List<int> newList = new List<int>();

            string data = Data;

            for (int i = 0; i < data.Length; i += 10)
            {
                string CurrentCell = data.Substring(i, 10);
                byte[] CellInfo = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                for (int i2 = CurrentCell.Length - 1; i2 >= 0; i2--)
                    CellInfo[i2] = (byte)hash.IndexOf(CurrentCell[i2]);

                var type = (CellInfo[2] & 56) >> 3;

                if (type != 0)
                    newList.Add(i / 10);
            }

            return newList;
         }

        public static string DecypherData(string Data, string DecryptKey)
        {
            try
            {
                string result = string.Empty;

                if (DecryptKey != "")
                {
                    DecryptKey = PrepareKey(DecryptKey);
                    

                    //for (int i = 0, k = 0; i < Data.Length; i += 2)
                        //result += (char)(int.Parse(Data.Substring(i, 2), System.Globalization.NumberStyles.HexNumber) ^ (int)(DecryptKey[(k++ + checkSum) % DecryptKey.Length]));

                    return Uri.UnescapeDataString(result);
                }
                else return Data;
            }
            catch { return ""; }
        }
        private static string PrepareKey(string Key)
        {
            string keyResult = "";

            for (int i = 0; i < Key.Length; i += 2)
                keyResult += Convert.ToChar(int.Parse(Key.Substring(i, 2), System.Globalization.NumberStyles.HexNumber));

            return Uri.UnescapeDataString(keyResult);
        }



        private string hash = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";

        private int hashCodes(char a)
        {
            return hash.IndexOf(a);
        }
    }
}


