﻿using System.Collections.Generic;
using System.ComponentModel;

namespace MonCollection
{
    public class MonData
    {
        public string Nickname { get; set; }
        public ushort Species { get; set; }
        public int Level { get; set; }
        public int Gender { get; set; } = 2;
        public List<int> Moves { get; set; }
        public string Game { get; set; }
        public byte AltForm { get; set; }
        public bool Shiny { get; set; }
        [DefaultValue(-1)]
        public int Ability { get; set; }
        public int Boon { get; set; }
        public int Bane { get; set; }
        public int Nature { get; set; }
        public int HP { get; set; }
        public int ATK { get; set; }
        public int DEF { get; set; }
        public int SPA { get; set; }
        public int SPD { get; set; }
        public int SPE { get; set; }
        public int Gen { get; set; }
        public int ID { get; set; }
        public string OT { get; set; }
        public string Origin { get; set; }
        public int Ball { get; set; }
        public int Language { get; set; }
        public bool PKRS_Infected { get; set; }
        public bool PKRS_Cured { get; set; }
        public string[] Ribbons { get; set; }
        public int dynaLevel { get; set; }
        public bool gMax { get; set; }
        public bool alpha { get; set; }
        public byte teraType { get; set; }
        public List<string> availableVersions { get; set; }
        public Dictionary<string,Movepools> movepools { get; set; }
    }

    public class Movepools
    {
        public List<int> moves;
        public List<int> special;
    }
}
