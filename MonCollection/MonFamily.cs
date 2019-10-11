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
            families.Add(new int[] { 10, 11, 12 }); //Caterpie
            families.Add(new int[] { 13, 14, 15 }); //Weedle
            families.Add(new int[] { 16, 17, 18 }); //Pidgey
            families.Add(new int[] { 19, 20 }); //Rattata
            families.Add(new int[] { 21, 22 }); //Spearow
            families.Add(new int[] { 23, 24 }); //Ekans
            families.Add(new int[] { 172, 25, 26 }); //Pichu
            families.Add(new int[] { 27, 28 }); //Sandshrew
            families.Add(new int[] { 29, 30, 31 }); //Nidoran F
            families.Add(new int[] { 32, 33, 34 }); //Nidoran M
            families.Add(new int[] { 173, 35, 36 }); //Cleffa
            families.Add(new int[] { 37, 38 }); //Vulpix
            families.Add(new int[] { 174, 39, 40 }); //Igglybuff
            families.Add(new int[] { 41, 42, 169 }); //Zubat
            families.Add(new int[] { 43, 44, 45, 182 }); //Oddish
            families.Add(new int[] { 46, 47 }); //Paras
            families.Add(new int[] { 48, 49 }); //Venonat
            families.Add(new int[] { 50, 51 }); //Diglett
            families.Add(new int[] { 52, 53 }); //Meowth
            families.Add(new int[] { 54, 55 }); //Psyduck
            families.Add(new int[] { 56, 57 }); //Mankey
            families.Add(new int[] { 58, 59 }); //Growlithe
            families.Add(new int[] { 60, 61, 62, 186 }); //Poliwag
            families.Add(new int[] { 63, 64, 65 }); //Abra
            families.Add(new int[] { 66, 67, 68 }); //Machop
            families.Add(new int[] { 69, 70, 71 }); //Bellsprout
            families.Add(new int[] { 72, 73 }); //Tentacool
            families.Add(new int[] { 74, 75, 76 }); //Geodude
            families.Add(new int[] { 77, 78 }); //Ponyta
            families.Add(new int[] { 79, 80, 199 }); //Slowpoke
            families.Add(new int[] { 81, 82, 462 }); //Magnemite
            families.Add(new int[] { 83 }); //Farfetch'd
            families.Add(new int[] { 84, 85 }); //Doduo
            families.Add(new int[] { 86, 87 }); //Ponyta
            families.Add(new int[] { 88, 89 }); //Grimer
            families.Add(new int[] { 90, 91 }); //Shellder
            families.Add(new int[] { 92, 93, 94 }); //Gastly
            families.Add(new int[] { 95, 208 }); //Onix
            families.Add(new int[] { 96, 97 }); //Drowzee
            families.Add(new int[] { 98, 99 }); //Krabby
            families.Add(new int[] { 100, 101 }); //Voltorb
            families.Add(new int[] { 102, 103 }); //Exeggcute
            families.Add(new int[] { 104, 105 }); //Cubone
            families.Add(new int[] { 236, 106, 107, 237 }); //Tyrogue
            families.Add(new int[] { 108, 463 }); //Lickitung
            families.Add(new int[] { 109, 110 }); //Koffing
            families.Add(new int[] { 111, 112, 464 }); //Rhyhorn
            families.Add(new int[] { 440, 113, 242 }); //Happiny
            families.Add(new int[] { 114, 465 }); //Tangela
            families.Add(new int[] { 115 }); //Kangaskhan
            families.Add(new int[] { 116, 117, 230 }); //Horsea
            families.Add(new int[] { 118, 119 }); //Goldeen
            families.Add(new int[] { 120, 121 }); //Staryu
            families.Add(new int[] { 439, 122 }); //Mime Jr.
            families.Add(new int[] { 123, 212 }); //Scyther
            families.Add(new int[] { 238, 124 }); //Smoochum
            families.Add(new int[] { 239, 125, 466 }); //Elekid
            families.Add(new int[] { 240, 126, 467 }); //Magby
            families.Add(new int[] { 127 }); //Pinsir
            families.Add(new int[] { 128 }); //Tauros
            families.Add(new int[] { 129, 130 }); //Magikarp
            families.Add(new int[] { 131 }); //Lapras
            families.Add(new int[] { 132 }); //Ditto
            families.Add(new int[] { 133, 134, 135, 136, 196, 197, 470, 471, 700 }); //Eevee
            families.Add(new int[] { 137, 233, 467 }); //Porygon
            families.Add(new int[] { 138, 139 }); //Omanyte
            families.Add(new int[] { 140, 141 }); //Kabuto
            families.Add(new int[] { 142 }); //Aerodactyl
            families.Add(new int[] { 446, 143 }); //Munchlax
            families.Add(new int[] { 144 }); //Articuno
            families.Add(new int[] { 145 }); //Zapdos
            families.Add(new int[] { 146 }); //Moltres
            families.Add(new int[] { 147, 148, 149 }); //Dratini
            families.Add(new int[] { 150 }); //Mewtwo
            families.Add(new int[] { 151 }); //Mew
            families.Add(new int[] { 152, 153, 154 }); //Chikorita
            families.Add(new int[] { 155, 156, 157 }); //Cyndaquil
            families.Add(new int[] { 158, 159, 160 }); //Totodile
            families.Add(new int[] { 161, 162 }); //Sentret
            families.Add(new int[] { 163, 164 }); //Hoothoot
            families.Add(new int[] { 165, 166 }); //Ledyba
            families.Add(new int[] { 167, 168 }); //Spinarak
            families.Add(new int[] { 170, 171 }); //Chinchou
            families.Add(new int[] { 175, 176, 468 }); //Togetic
            families.Add(new int[] { 177, 178 }); //Natu
            families.Add(new int[] { 179, 180, 181 }); //Mareep
            families.Add(new int[] { 298, 183, 184 }); //Azurill
            families.Add(new int[] { 438, 185 }); //Bonsly
            families.Add(new int[] { 187, 188, 189 }); //Hoppip
            families.Add(new int[] { 190, 424 }); //Aipom
            families.Add(new int[] { 191, 192 }); //Sunkern
            families.Add(new int[] { 193, 469 }); //Yanma
            families.Add(new int[] { 194, 195 }); //Wooper
            families.Add(new int[] { 198, 430 }); //Murkrow
            families.Add(new int[] { 200, 429 }); //Misdreavus
            families.Add(new int[] { 201 }); //Unown
            families.Add(new int[] { 360, 202 }); //Wynaut
            families.Add(new int[] { 203 }); //Girafarig
            families.Add(new int[] { 204, 205 }); //Pineco
            families.Add(new int[] { 206 }); //Dunsparce
            families.Add(new int[] { 207, 472 }); //Gligar
            families.Add(new int[] { 209, 210 }); //Snubbul
            families.Add(new int[] { 211 }); //Qwilfish
            families.Add(new int[] { 213 }); //Shuckle
            families.Add(new int[] { 214 }); //Heracross
            families.Add(new int[] { 215, 461 }); //Sneasel
            families.Add(new int[] { 216, 217 }); //Teddiursa
            families.Add(new int[] { 218, 219 }); //Slugma
            families.Add(new int[] { 220, 221, 473 }); //Swinub
            families.Add(new int[] { 222 }); //Corsola
            families.Add(new int[] { 223, 224 }); //Remoraid
            families.Add(new int[] { 225 }); //Delibird
            families.Add(new int[] { 458, 226 }); //Mantyke
            families.Add(new int[] { 227 }); //Skarmory
            families.Add(new int[] { 228, 229 }); //Houndour
            families.Add(new int[] { 231, 232 }); //Phanpy
            families.Add(new int[] { 234 }); //Stantler
            families.Add(new int[] { 235 }); //Smeargle
            families.Add(new int[] { 241 }); //Miltank
            families.Add(new int[] { 243 }); //Raikou
            families.Add(new int[] { 244 }); //Entei
            families.Add(new int[] { 245 }); //Suicune
            families.Add(new int[] { 246, 247, 248 }); //Larvitar
            families.Add(new int[] { 249 }); //Lugia
            families.Add(new int[] { 250 }); //Ho-oh
            families.Add(new int[] { 251 }); //Celebi
            families.Add(new int[] { 252, 253, 254 }); //Treecko
            families.Add(new int[] { 255, 256, 257 }); //Torchic
            families.Add(new int[] { 258, 259, 260 }); //Mudkip
            families.Add(new int[] { 261, 262 }); //Poochyena
            families.Add(new int[] { 263, 264 }); //Zigzagoon
            families.Add(new int[] { 265, 266, 267, 268, 269 }); //Wurmple
            families.Add(new int[] { 270, 271, 272 }); //Lotad
            families.Add(new int[] { 273, 274, 275 }); //Seedot
            families.Add(new int[] { 276, 277 }); //Taillow
            families.Add(new int[] { 278, 279 }); //Wingull
            families.Add(new int[] { 280, 281, 282, 475 }); //Ralts
            families.Add(new int[] { 283, 284 }); //Surskit
            families.Add(new int[] { 285, 286 }); //Shroomish
            families.Add(new int[] { 287, 288, 289 }); //Slakoth
            families.Add(new int[] { 290, 291, 292 }); //Nincada
            families.Add(new int[] { 293, 294, 295 }); //Whismur
            families.Add(new int[] { 296, 297 }); //Makuhita
            families.Add(new int[] { 299, 476 }); //Probopass
            families.Add(new int[] { 300, 301 }); //Skitty
            families.Add(new int[] { 302 }); //Sableye
            families.Add(new int[] { 303 }); //Mawile
            families.Add(new int[] { 304, 305, 306 }); //Aron
            families.Add(new int[] { 307, 308 }); //Meditite
            families.Add(new int[] { 309, 310 }); //Electrike
            families.Add(new int[] { 311 }); //Plusle
            families.Add(new int[] { 312 }); //Minun
            families.Add(new int[] { 313 }); //Volbeat
            families.Add(new int[] { 314 }); //Illumise
            families.Add(new int[] { 406, 315, 407 }); //Budew
            families.Add(new int[] { 316, 317 }); //Gulpin
            families.Add(new int[] { 318, 319 }); //Carvanha
            families.Add(new int[] { 320, 321 }); //Wailmer
            families.Add(new int[] { 322, 323 }); //Numel
            families.Add(new int[] { 324 }); //Torkoal
            families.Add(new int[] { 325, 326 }); //Spoink
            families.Add(new int[] { 327 }); //Spinda
            families.Add(new int[] { 328, 329, 330 }); //Trapinch
            families.Add(new int[] { 331, 332 }); //Cacnea
            families.Add(new int[] { 333, 334 }); //Swablu
            families.Add(new int[] { 335 }); //Zangoose
            families.Add(new int[] { 336 }); //Seviper
            families.Add(new int[] { 337 }); //Lunatone
            families.Add(new int[] { 338 }); //Solrock
            families.Add(new int[] { 339, 340 }); //Barboach
            families.Add(new int[] { 341, 342 }); //Corphish
            families.Add(new int[] { 343, 344 }); //Baltoy
            families.Add(new int[] { 345, 346 }); //Lileep
            families.Add(new int[] { 347, 348 }); //Anorith
            families.Add(new int[] { 349, 350 }); //Feebas
            families.Add(new int[] { 351 }); //Castform
            families.Add(new int[] { 352 }); //Kecleon
            families.Add(new int[] { 353, 354 }); //Shuppet
            families.Add(new int[] { 355, 356, 477 }); //Duskull
            families.Add(new int[] { 357 }); //Tropius
            families.Add(new int[] { 433, 358 }); //Chingling
            families.Add(new int[] { 359 }); //Absol
            families.Add(new int[] { 361, 362, 478 }); //Snorunt
            families.Add(new int[] { 363, 364, 365 }); //Spheal
            families.Add(new int[] { 366, 367, 368 }); //Clamperl
            families.Add(new int[] { 369 }); //Relicanth
            families.Add(new int[] { 376 }); //Luvdisc
            families.Add(new int[] { 371, 372, 373 }); //Bagon
            families.Add(new int[] { 374, 375, 376 }); //Beldum
            families.Add(new int[] { 377 }); //Regirock
            families.Add(new int[] { 378 }); //Regice
            families.Add(new int[] { 379 }); //Registeel
            families.Add(new int[] { 380 }); //Latios
            families.Add(new int[] { 381 }); //Latias
            families.Add(new int[] { 382 }); //Kyogre
            families.Add(new int[] { 383 }); //Groudon
            families.Add(new int[] { 384 }); //Rayquaza
            families.Add(new int[] { 385 }); //Jirachi
            families.Add(new int[] { 386 }); //Deoxys

            families.Add(new int[] { 425, 426 }); //Drifloon

            families.Add(new int[] { 722, 723, 724 }); //Rowlet
            families.Add(new int[] { 725, 726, 727 }); //Litten
            families.Add(new int[] { 728, 729, 730 }); //Popplio
            families.Add(new int[] { 731, 732, 733 }); //Pikipek
            families.Add(new int[] { 734, 735 }); //Yungoos

            families.Add(new int[] { 742, 743 }); //Cutiefly

        }
    }
}
