using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonCollection
{
    class MonData
    {
        public string Nickname { get; set; }
        public int Species { get; set; }
        public int Level { get; set; }
        public int Gender { get; set; } = 2;
        public List<int> Moves { get; set; }
        public string Game { get; set; }
        public int AltForm { get; set; }
        public bool Shiny { get; set; }
        [DefaultValue(-1)]
        public int Ability { get; set; }
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
        public int Origin { get; set; }
        public int Ball { get; set; }
        public int Language { get; set; }
        public bool PKRS_Infected { get; set; }
        public bool PKRS_Cured { get; set; }
        public int PKRS_Strain { get; set; }
    }
}
