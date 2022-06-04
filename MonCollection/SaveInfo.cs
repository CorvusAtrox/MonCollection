using PKHeX.Core;
using System;

namespace MonCollection
{
    public class SaveInfo
    {
        public string language;
        public GameVersion version;
        public int id;
        public string ot;
        public int index;

        public SaveInfo(string l, string v, string d, string o, int i)
        {
            language = l;
            Enum.TryParse(v, out GameVersion ver);
            version = ver;
            id = int.Parse(d);
            ot = o;
            index = i;
        }

        public bool hasId()
        {
            bool val = (ot == null);

            if (val)
                val = (ot.Length > 0);

            val = (val || id > 0);

            return val;
        }

        public int getGen()
        {
            switch (version)
            {
                case GameVersion.RD:
                case GameVersion.GN:
                case GameVersion.BU:
                case GameVersion.YW:
                    return 1;
                case GameVersion.GD:
                case GameVersion.SI:
                case GameVersion.C:
                    return 2;
                case GameVersion.R:
                case GameVersion.S:
                case GameVersion.FR:
                case GameVersion.LG:
                case GameVersion.E:
                case GameVersion.CXD:
                    return 3;
                case GameVersion.D:
                case GameVersion.P:
                case GameVersion.Pt:
                case GameVersion.HG:
                case GameVersion.SS:
                    return 4;
                case GameVersion.B:
                case GameVersion.W:
                case GameVersion.B2:
                case GameVersion.W2:
                    return 5;
                case GameVersion.X:
                case GameVersion.Y:
                case GameVersion.OR:
                case GameVersion.AS:
                    return 6;
                case GameVersion.SN:
                case GameVersion.MN:
                case GameVersion.US:
                case GameVersion.UM:
                case GameVersion.GP:
                case GameVersion.GE:
                    return 7;
                case GameVersion.SW:
                case GameVersion.SH:
                case GameVersion.SWSH:
                    return 8;
                default:
                    return 8;
            }
        }

    }
}
