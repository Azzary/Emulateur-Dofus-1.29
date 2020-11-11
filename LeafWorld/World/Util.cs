using System;
using System.Collections.Generic;
using System.Text;

namespace LeafWorld
{
    class Util
    {
        public static List<char> HASH = new List<char>() {'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's',
                't', 'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q', 'R', 'S', 'T', 'U',
                'V', 'W', 'X', 'Y', 'Z', '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '-', '_'};

        public static string GetDirChar(int dirNum)
        {
            var hash = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";

            if (dirNum >= hash.Length)
                return "";

            return hash[dirNum].ToString();
        }
        public static int GetDirNum(string dirChar)
        {
            var hash = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-_";
            return hash.IndexOf(dirChar);
        }

        public static int CharToCell(string cellCode)
        {
            char char1 = cellCode.ToCharArray()[0];
            char char2 = cellCode.ToCharArray()[1];
            short code1 = 0;
            short code2 = 0;
            short a = 0;
            while (a < HASH.Count)
            {
                if (HASH[a] == char1)
                {
                    code1 = (short)(a * 64);
                }
                if (HASH[a] == char2)
                {
                    code2 = a;
                }
                a = (short)(a + 1);
            }
            return (short)(code1 + code2);
        }

        public static string CellToChar(int cellId)
        {
            int char1 = cellId / 64;
            int char2 = cellId % 64;
            return HASH[char1] + "" + HASH[char2];

        }

    }
}
