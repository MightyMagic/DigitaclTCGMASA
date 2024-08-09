using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AndreiScripts
{
    public class PlayerData
    {
        public string ID { get; private set; }
        public string Name { get; private set; }

        public PlayerData(string iD, string name)
        {
            ID = iD;
            Name = name;
        }
    }
}
