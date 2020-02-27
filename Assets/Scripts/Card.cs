using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets.Scripts
{
    public class Card
    {
        public int Index { get; private set; }
        public Vector4 YESValue { get; private set; }
        public Vector4 NOValue { get; private set; }
        public string Name { get; private set; }
        public string YES { get; private set; }
        public string NO { get; private set; }
        public Card(int index,INIParser ini)
        {
            Index = index;           
            Name = ini.ReadValue(Index.ToString(), "name", "error");
            YES = ini.ReadValue(Index.ToString(), "Y", "error");
            NO = ini.ReadValue(Index.ToString(), "N", "error");
            float YH = (float)ini.ReadValue(Index.ToString(), "YH", 0.0);
            float YS = (float)ini.ReadValue(Index.ToString(), "YS", 0.0);
            float YD = (float)ini.ReadValue(Index.ToString(), "YD", 0.0);
            float YP = (float)ini.ReadValue(Index.ToString(), "YP", 0.0);
            YESValue = new Vector4(YS, YH, YP, YD);
            float NH = (float)ini.ReadValue(Index.ToString(), "NH", 0.0);
            float NS = (float)ini.ReadValue(Index.ToString(), "NS", 0.0);
            float ND = (float)ini.ReadValue(Index.ToString(), "ND", 0.0);
            float NP = (float)ini.ReadValue(Index.ToString(), "NP", 0.0);
            NOValue = new Vector4(NS, NH, NP, ND);
        }

    }
}
