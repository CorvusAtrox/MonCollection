using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonCollection
{
    class MonFamily
    {

        List<int[]> families;

        public MonFamily()
        {
            families = new List<int[]>();
            addMons();
        }

        public int[] getFamily(int species)
        {
            foreach(int[] fam in families)
            {
                if (fam.Contains(species))
                    return fam;
            }
            return new int[] { species };
        }

        private void addMons()
        {
            families.Add(new int[] { 1, 2, 3 }); //Bulbasaur
            families.Add(new int[] { 4, 5, 6 }); //Charmander
            families.Add(new int[] { 7, 8, 9 }); //Squirtle
        }
    }
}
