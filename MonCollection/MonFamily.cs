using System.Collections.Generic;
using System.Linq;

namespace MonCollection
{
    class MonFamily
    {
        private readonly List<ushort[]> families;

        public MonFamily()
        {
            families = new List<ushort[]>();
            AddMons();
        }

        public ushort[] GetFamily(ushort species)
        {
            foreach(ushort[] fam in families)
            {
                if (fam.Contains(species))
                    return fam;
            }
            return new ushort[] { species };
        }

        private void AddMons()
        {
            families.Add(new ushort[] { 1, 2, 3 }); // Bulbasaur
            families.Add(new ushort[] { 4, 5, 6 }); // Charmander
            families.Add(new ushort[] { 7, 8, 9 }); // Squirtle
            families.Add(new ushort[] { 10, 11, 12 }); // Caterpie
            families.Add(new ushort[] { 13, 14, 15 }); // Weedle
            families.Add(new ushort[] { 16, 17, 18 }); // Pidgey
            families.Add(new ushort[] { 19, 20 }); // Rattata
            families.Add(new ushort[] { 21, 22 }); // Spearow
            families.Add(new ushort[] { 23, 24 }); // Ekans
            families.Add(new ushort[] { 172, 25, 26 }); // Pichu
            families.Add(new ushort[] { 27, 28 }); // Sandshrew
            families.Add(new ushort[] { 29, 30, 31, 32, 33, 34 }); // Nidoran
            families.Add(new ushort[] { 173, 35, 36 }); // Cleffa
            families.Add(new ushort[] { 37, 38 }); // Vulpix
            families.Add(new ushort[] { 174, 39, 40 }); // Igglybuff
            families.Add(new ushort[] { 41, 42, 169 }); // Zubat
            families.Add(new ushort[] { 43, 44, 45, 182 }); // Oddish
            families.Add(new ushort[] { 46, 47 }); // Paras
            families.Add(new ushort[] { 48, 49 }); // Venonat
            families.Add(new ushort[] { 50, 51 }); // Diglett
            families.Add(new ushort[] { 52, 53, 863 }); // Meowth
            families.Add(new ushort[] { 54, 55 }); // Psyduck
            families.Add(new ushort[] { 56, 57, 1010 }); // Mankey
            families.Add(new ushort[] { 58, 59 }); // Growlithe
            families.Add(new ushort[] { 60, 61, 62, 186 }); // Poliwag
            families.Add(new ushort[] { 63, 64, 65 }); // Abra
            families.Add(new ushort[] { 66, 67, 68 }); // Machop
            families.Add(new ushort[] { 69, 70, 71 }); // Bellsprout
            families.Add(new ushort[] { 72, 73 }); // Tentacool
            families.Add(new ushort[] { 74, 75, 76 }); // Geodude
            families.Add(new ushort[] { 77, 78 }); // Ponyta
            families.Add(new ushort[] { 79, 80, 199 }); // Slowpoke
            families.Add(new ushort[] { 81, 82, 462 }); // Magnemite
            families.Add(new ushort[] { 83, 865 }); // Farfetch'd
            families.Add(new ushort[] { 84, 85 }); // Doduo
            families.Add(new ushort[] { 86, 87 }); // Ponyta
            families.Add(new ushort[] { 88, 89 }); // Grimer
            families.Add(new ushort[] { 90, 91 }); // Shellder
            families.Add(new ushort[] { 92, 93, 94 }); // Gastly
            families.Add(new ushort[] { 95, 208 }); // Onix
            families.Add(new ushort[] { 96, 97 }); // Drowzee
            families.Add(new ushort[] { 98, 99 }); // Krabby
            families.Add(new ushort[] { 100, 101 }); // Voltorb
            families.Add(new ushort[] { 102, 103 }); // Exeggcute
            families.Add(new ushort[] { 104, 105 }); // Cubone
            families.Add(new ushort[] { 236, 106, 107, 237 }); // Tyrogue
            families.Add(new ushort[] { 108, 463 }); // Lickitung
            families.Add(new ushort[] { 109, 110 }); // Koffing
            families.Add(new ushort[] { 111, 112, 464 }); // Rhyhorn
            families.Add(new ushort[] { 440, 113, 242 }); // Happiny
            families.Add(new ushort[] { 114, 465 }); // Tangela
            families.Add(new ushort[] { 115 }); // Kangaskhan
            families.Add(new ushort[] { 116, 117, 230 }); // Horsea
            families.Add(new ushort[] { 118, 119 }); // Goldeen
            families.Add(new ushort[] { 120, 121 }); // Staryu
            families.Add(new ushort[] { 439, 122, 866 }); // Mime Jr.
            families.Add(new ushort[] { 123, 212, 900 }); // Scyther
            families.Add(new ushort[] { 238, 124 }); // Smoochum
            families.Add(new ushort[] { 239, 125, 466 }); // Elekid
            families.Add(new ushort[] { 240, 126, 467 }); // Magby
            families.Add(new ushort[] { 127 }); // Pinsir
            families.Add(new ushort[] { 128 }); // Tauros
            families.Add(new ushort[] { 129, 130 }); // Magikarp
            families.Add(new ushort[] { 131 }); // Lapras
            families.Add(new ushort[] { 132 }); // Ditto
            families.Add(new ushort[] { 133, 134, 135, 136, 196, 197, 470, 471, 700 }); // Eevee
            families.Add(new ushort[] { 137, 233, 474 }); // Porygon
            families.Add(new ushort[] { 138, 139 }); // Omanyte
            families.Add(new ushort[] { 140, 141 }); // Kabuto
            families.Add(new ushort[] { 142 }); // Aerodactyl
            families.Add(new ushort[] { 446, 143 }); // Munchlax
            families.Add(new ushort[] { 144 }); // Articuno
            families.Add(new ushort[] { 145 }); // Zapdos
            families.Add(new ushort[] { 146 }); // Moltres
            families.Add(new ushort[] { 147, 148, 149 }); // Dratini
            families.Add(new ushort[] { 150 }); // Mewtwo
            families.Add(new ushort[] { 151 }); // Mew
            families.Add(new ushort[] { 152, 153, 154 }); // Chikorita
            families.Add(new ushort[] { 155, 156, 157 }); // Cyndaquil
            families.Add(new ushort[] { 158, 159, 160 }); // Totodile
            families.Add(new ushort[] { 161, 162 }); // Sentret
            families.Add(new ushort[] { 163, 164 }); // Hoothoot
            families.Add(new ushort[] { 165, 166 }); // Ledyba
            families.Add(new ushort[] { 167, 168 }); // Spinarak
            families.Add(new ushort[] { 170, 171 }); // Chinchou
            families.Add(new ushort[] { 175, 176, 468 }); // Togetic
            families.Add(new ushort[] { 177, 178 }); // Natu
            families.Add(new ushort[] { 179, 180, 181 }); // Mareep
            families.Add(new ushort[] { 298, 183, 184 }); // Azurill
            families.Add(new ushort[] { 438, 185 }); // Bonsly
            families.Add(new ushort[] { 187, 188, 189 }); // Hoppip
            families.Add(new ushort[] { 190, 424 }); // Aipom
            families.Add(new ushort[] { 191, 192 }); // Sunkern
            families.Add(new ushort[] { 193, 469 }); // Yanma
            families.Add(new ushort[] { 194, 195, 1009 }); // Wooper
            families.Add(new ushort[] { 198, 430 }); // Murkrow
            families.Add(new ushort[] { 200, 429 }); // Misdreavus
            families.Add(new ushort[] { 201 }); // Unown
            families.Add(new ushort[] { 360, 202 }); // Wynaut
            families.Add(new ushort[] { 203, 928 }); // Girafarig
            families.Add(new ushort[] { 204, 205 }); // Pineco
            families.Add(new ushort[] { 206, 917 }); // Dunsparce
            families.Add(new ushort[] { 207, 472 }); // Gligar
            families.Add(new ushort[] { 209, 210 }); // Snubbull
            families.Add(new ushort[] { 211, 904 }); // Qwilfish
            families.Add(new ushort[] { 213 }); // Shuckle
            families.Add(new ushort[] { 214 }); // Heracross
            families.Add(new ushort[] { 215, 461, 903 }); // Sneasel
            families.Add(new ushort[] { 216, 217, 901 }); // Teddiursa
            families.Add(new ushort[] { 218, 219 }); // Slugma
            families.Add(new ushort[] { 220, 221, 473 }); // Swinub
            families.Add(new ushort[] { 222, 864 }); // Corsola
            families.Add(new ushort[] { 223, 224 }); // Remoraid
            families.Add(new ushort[] { 225 }); // Delibird
            families.Add(new ushort[] { 458, 226 }); // Mantyke
            families.Add(new ushort[] { 227 }); // Skarmory
            families.Add(new ushort[] { 228, 229 }); // Houndour
            families.Add(new ushort[] { 231, 232 }); // Phanpy
            families.Add(new ushort[] { 234, 899 }); // Stantler
            families.Add(new ushort[] { 235 }); // Smeargle
            families.Add(new ushort[] { 241 }); // Miltank
            families.Add(new ushort[] { 243 }); // Raikou
            families.Add(new ushort[] { 244 }); // Entei
            families.Add(new ushort[] { 245 }); // Suicune
            families.Add(new ushort[] { 246, 247, 248 }); // Larvitar
            families.Add(new ushort[] { 249 }); // Lugia
            families.Add(new ushort[] { 250 }); // Ho-oh
            families.Add(new ushort[] { 251 }); // Celebi
            families.Add(new ushort[] { 252, 253, 254 }); // Treecko
            families.Add(new ushort[] { 255, 256, 257 }); // Torchic
            families.Add(new ushort[] { 258, 259, 260 }); // Mudkip
            families.Add(new ushort[] { 261, 262 }); // Poochyena
            families.Add(new ushort[] { 263, 264, 862 }); // Zigzagoon
            families.Add(new ushort[] { 265, 266, 267, 268, 269 }); // Wurmple
            families.Add(new ushort[] { 270, 271, 272 }); // Lotad
            families.Add(new ushort[] { 273, 274, 275 }); // Seedot
            families.Add(new ushort[] { 276, 277 }); // Taillow
            families.Add(new ushort[] { 278, 279 }); // Wingull
            families.Add(new ushort[] { 280, 281, 282, 475 }); // Ralts
            families.Add(new ushort[] { 283, 284 }); // Surskit
            families.Add(new ushort[] { 285, 286 }); // Shroomish
            families.Add(new ushort[] { 287, 288, 289 }); // Slakoth
            families.Add(new ushort[] { 290, 291, 292 }); // Nincada
            families.Add(new ushort[] { 293, 294, 295 }); // Whismur
            families.Add(new ushort[] { 296, 297 }); // Makuhita
            families.Add(new ushort[] { 299, 476 }); // Probopass
            families.Add(new ushort[] { 300, 301 }); // Skitty
            families.Add(new ushort[] { 302 }); // Sableye
            families.Add(new ushort[] { 303 }); // Mawile
            families.Add(new ushort[] { 304, 305, 306 }); // Aron
            families.Add(new ushort[] { 307, 308 }); // Meditite
            families.Add(new ushort[] { 309, 310 }); // Electrike
            families.Add(new ushort[] { 311 }); // Plusle
            families.Add(new ushort[] { 312 }); // Minun
            families.Add(new ushort[] { 313, 314 }); // Volbeat/Illumise
            families.Add(new ushort[] { 406, 315, 407 }); // Budew
            families.Add(new ushort[] { 316, 317 }); // Gulpin
            families.Add(new ushort[] { 318, 319 }); // Carvanha
            families.Add(new ushort[] { 320, 321 }); // Wailmer
            families.Add(new ushort[] { 322, 323 }); // Numel
            families.Add(new ushort[] { 324 }); // Torkoal
            families.Add(new ushort[] { 325, 326 }); // Spoink
            families.Add(new ushort[] { 327 }); // Spinda
            families.Add(new ushort[] { 328, 329, 330 }); // Trapinch
            families.Add(new ushort[] { 331, 332 }); // Cacnea
            families.Add(new ushort[] { 333, 334 }); // Swablu
            families.Add(new ushort[] { 335 }); // Zangoose
            families.Add(new ushort[] { 336 }); // Seviper
            families.Add(new ushort[] { 337 }); // Lunatone
            families.Add(new ushort[] { 338 }); // Solrock
            families.Add(new ushort[] { 339, 340 }); // Barboach
            families.Add(new ushort[] { 341, 342 }); // Corphish
            families.Add(new ushort[] { 343, 344 }); // Baltoy
            families.Add(new ushort[] { 345, 346 }); // Lileep
            families.Add(new ushort[] { 347, 348 }); // Anorith
            families.Add(new ushort[] { 349, 350 }); // Feebas
            families.Add(new ushort[] { 351 }); // Castform
            families.Add(new ushort[] { 352 }); // Kecleon
            families.Add(new ushort[] { 353, 354 }); // Shuppet
            families.Add(new ushort[] { 355, 356, 477 }); // Duskull
            families.Add(new ushort[] { 357 }); // Tropius
            families.Add(new ushort[] { 433, 358 }); // Chingling
            families.Add(new ushort[] { 359 }); // Absol
            families.Add(new ushort[] { 361, 362, 478 }); // Snorunt
            families.Add(new ushort[] { 363, 364, 365 }); // Spheal
            families.Add(new ushort[] { 366, 367, 368 }); // Clamperl
            families.Add(new ushort[] { 369 }); // Relicanth
            families.Add(new ushort[] { 376 }); // Luvdisc
            families.Add(new ushort[] { 371, 372, 373 }); // Bagon
            families.Add(new ushort[] { 374, 375, 376 }); // Beldum
            families.Add(new ushort[] { 377 }); // Regirock
            families.Add(new ushort[] { 378 }); // Regice
            families.Add(new ushort[] { 379 }); // Registeel
            families.Add(new ushort[] { 380 }); // Latios
            families.Add(new ushort[] { 381 }); // Latias
            families.Add(new ushort[] { 382 }); // Kyogre
            families.Add(new ushort[] { 383 }); // Groudon
            families.Add(new ushort[] { 384 }); // Rayquaza
            families.Add(new ushort[] { 385 }); // Jirachi
            families.Add(new ushort[] { 386 }); // Deoxys
            families.Add(new ushort[] { 387, 388, 389 }); // Turtwig
            families.Add(new ushort[] { 390, 391, 392 }); // Chimchar
            families.Add(new ushort[] { 393, 394, 395 }); // Piplup
            families.Add(new ushort[] { 396, 397, 398 }); // Starly
            families.Add(new ushort[] { 399, 400 }); // Bidoof
            families.Add(new ushort[] { 401, 402 }); // Kricketot
            families.Add(new ushort[] { 403, 404, 405 }); // Shinx
            families.Add(new ushort[] { 408, 409 }); // Cranidos
            families.Add(new ushort[] { 410, 411 }); // Shieldon
            families.Add(new ushort[] { 412, 413, 414 }); // Burmy
            families.Add(new ushort[] { 415, 416 }); // Combee
            families.Add(new ushort[] { 417 }); // Pachirisu
            families.Add(new ushort[] { 418, 419 }); // Buizel
            families.Add(new ushort[] { 420, 421 }); // Cherubi
            families.Add(new ushort[] { 422, 423 }); // Shellos
            families.Add(new ushort[] { 425, 426 }); // Drifloon
            families.Add(new ushort[] { 427, 428 }); // Buneary
            families.Add(new ushort[] { 431, 432 }); // Glameow
            families.Add(new ushort[] { 433, 434 }); // Stunky
            families.Add(new ushort[] { 436, 437 }); // Bronzor
            families.Add(new ushort[] { 441 }); // Chatot
            families.Add(new ushort[] { 442 }); // Spiritomb
            families.Add(new ushort[] { 443, 444, 445 }); // Gible
            families.Add(new ushort[] { 447, 448 }); // Riolu
            families.Add(new ushort[] { 449, 450 }); // Hippopotas
            families.Add(new ushort[] { 451, 452 }); // Skorupi
            families.Add(new ushort[] { 453, 454 }); // Croagunk
            families.Add(new ushort[] { 455 }); // Carnivine
            families.Add(new ushort[] { 456, 457 }); // Finneon
            families.Add(new ushort[] { 459, 460 }); // Snover
            families.Add(new ushort[] { 479 }); // Rotom
            families.Add(new ushort[] { 480 }); // Uxie
            families.Add(new ushort[] { 481 }); // Mesprit
            families.Add(new ushort[] { 482 }); // Azelf
            families.Add(new ushort[] { 483 }); // Dialga
            families.Add(new ushort[] { 484 }); // Palkia
            families.Add(new ushort[] { 485 }); // Heatran
            families.Add(new ushort[] { 486 }); // Regigigas
            families.Add(new ushort[] { 487 }); // Giratina
            families.Add(new ushort[] { 488 }); // Cresselia
            families.Add(new ushort[] { 489, 490 }); // Phione
            families.Add(new ushort[] { 491 }); // Darkrai
            families.Add(new ushort[] { 492 }); // Shaymin
            families.Add(new ushort[] { 493 }); // Arceus
            families.Add(new ushort[] { 494 }); // Victini
            families.Add(new ushort[] { 495, 496, 497 }); // Snivy
            families.Add(new ushort[] { 498, 499, 500 }); // Tepig
            families.Add(new ushort[] { 501, 502, 503 }); // Oshawott
            families.Add(new ushort[] { 504, 505 }); // Patrat
            families.Add(new ushort[] { 506, 507, 508 }); // Lillipup
            families.Add(new ushort[] { 509, 510 }); // Purrloin
            families.Add(new ushort[] { 511, 512 }); // Pansage
            families.Add(new ushort[] { 513, 514 }); // Pansear
            families.Add(new ushort[] { 515, 516 }); // Panpour
            families.Add(new ushort[] { 517, 518 }); // Munna
            families.Add(new ushort[] { 519, 520, 521 }); // Pidove
            families.Add(new ushort[] { 522, 523 }); // Blitzle
            families.Add(new ushort[] { 524, 525, 526 }); // Roggenrola
            families.Add(new ushort[] { 527, 528 }); // Woobat
            families.Add(new ushort[] { 529, 530 }); // Drilbur
            families.Add(new ushort[] { 531 }); // Audino
            families.Add(new ushort[] { 532, 533, 534 }); // Timburr
            families.Add(new ushort[] { 535, 536, 537 }); // Tympole
            families.Add(new ushort[] { 538 }); // Throh
            families.Add(new ushort[] { 539 }); // Sawk
            families.Add(new ushort[] { 540, 541, 542 }); // Sewaddle
            families.Add(new ushort[] { 543, 544, 545 }); // Venipede
            families.Add(new ushort[] { 546, 547 }); // Cottonee
            families.Add(new ushort[] { 548, 549 }); // Petilil
            families.Add(new ushort[] { 550, 902 }); // Basculin
            families.Add(new ushort[] { 551, 552, 553 }); // Sandile
            families.Add(new ushort[] { 554, 555 }); // Darumaka
            families.Add(new ushort[] { 556 }); // Maractus
            families.Add(new ushort[] { 557, 558 }); // Dwebble
            families.Add(new ushort[] { 559, 560 }); // Scraggy
            families.Add(new ushort[] { 561 }); // Sigilyph
            families.Add(new ushort[] { 562, 563, 867 }); // Yamask
            families.Add(new ushort[] { 564, 565 }); // Tirtouga
            families.Add(new ushort[] { 566, 567 }); // Archen
            families.Add(new ushort[] { 568, 569 }); // Trubbish
            families.Add(new ushort[] { 570, 571 }); // Zorua
            families.Add(new ushort[] { 572, 573 }); // Minccino
            families.Add(new ushort[] { 574, 575, 576 }); // Gothita
            families.Add(new ushort[] { 577, 578, 579 }); // Solosis
            families.Add(new ushort[] { 580, 581 }); // Ducklett
            families.Add(new ushort[] { 582, 583, 584 }); // Vanillite
            families.Add(new ushort[] { 585, 586 }); // Deerling
            families.Add(new ushort[] { 587 }); // Emolga
            families.Add(new ushort[] { 588, 589 }); // Karrablast
            families.Add(new ushort[] { 590, 591 }); // Foongus
            families.Add(new ushort[] { 592, 593 }); // Frillish
            families.Add(new ushort[] { 594 }); // Alomomola
            families.Add(new ushort[] { 595, 596 }); // Joltik
            families.Add(new ushort[] { 597, 598 }); // Ferroseed
            families.Add(new ushort[] { 599, 600, 601 }); // Klink
            families.Add(new ushort[] { 602, 603, 604 }); // Tynamo
            families.Add(new ushort[] { 605, 606 }); // Elgyem
            families.Add(new ushort[] { 607, 608, 609 }); // Litwick
            families.Add(new ushort[] { 610, 611, 612 }); // Axew
            families.Add(new ushort[] { 613, 614 }); // Beartic
            families.Add(new ushort[] { 615 }); // Cryogonal
            families.Add(new ushort[] { 616, 617 }); // Shelmet
            families.Add(new ushort[] { 618 }); // Stunfisk
            families.Add(new ushort[] { 619, 620 }); // Mienfoo
            families.Add(new ushort[] { 621 }); // Druddigon
            families.Add(new ushort[] { 622, 623 }); // Golett
            families.Add(new ushort[] { 624, 625, 1008 }); // Pawniard
            families.Add(new ushort[] { 626 }); // Bouffalant
            families.Add(new ushort[] { 627, 628 }); // Rufflet
            families.Add(new ushort[] { 629, 630 }); // Vullaby
            families.Add(new ushort[] { 631 }); // Heatmor
            families.Add(new ushort[] { 632 }); // Durant
            families.Add(new ushort[] { 633, 634, 635 }); // Deino
            families.Add(new ushort[] { 636, 637 }); // Larvesta
            families.Add(new ushort[] { 638 }); // Cobalion
            families.Add(new ushort[] { 639 }); // Terrakion
            families.Add(new ushort[] { 640 }); // Virizion
            families.Add(new ushort[] { 641 }); // Tornadus
            families.Add(new ushort[] { 642 }); // Thundurus
            families.Add(new ushort[] { 643 }); // Reshiram
            families.Add(new ushort[] { 644 }); // Zekrom
            families.Add(new ushort[] { 645 }); // Landorus
            families.Add(new ushort[] { 646 }); // Kyurem
            families.Add(new ushort[] { 647 }); // Keldeo
            families.Add(new ushort[] { 648 }); // Meloetta
            families.Add(new ushort[] { 649 }); // Genesect
            families.Add(new ushort[] { 650, 651, 652 }); // Chespin
            families.Add(new ushort[] { 653, 654, 655 }); // Fennekin
            families.Add(new ushort[] { 656, 657, 658 }); // Froakie
            families.Add(new ushort[] { 659, 660 }); // Bunnelby
            families.Add(new ushort[] { 661, 662, 663 }); // Fletchling
            families.Add(new ushort[] { 664, 665, 666 }); // Scatterbug
            families.Add(new ushort[] { 667, 668 }); // Litleo
            families.Add(new ushort[] { 669, 670, 671 }); // Flabébé
            families.Add(new ushort[] { 672, 673 }); // Skiddo
            families.Add(new ushort[] { 674, 675 }); // Pancham
            families.Add(new ushort[] { 676 }); // Furfrou
            families.Add(new ushort[] { 677, 678 }); // Espurr
            families.Add(new ushort[] { 679, 680, 681 }); // Honedge
            families.Add(new ushort[] { 682, 683 }); // Spritzee
            families.Add(new ushort[] { 684, 685 }); // Swirlix
            families.Add(new ushort[] { 686, 687 }); // Inkay
            families.Add(new ushort[] { 688, 689 }); // Binacle
            families.Add(new ushort[] { 690, 691 }); // Skrelp
            families.Add(new ushort[] { 692, 693 }); // Clauncher
            families.Add(new ushort[] { 694, 695 }); // Helioptile
            families.Add(new ushort[] { 696, 697 }); // Tyrunt
            families.Add(new ushort[] { 698, 699 }); // Amaura
            families.Add(new ushort[] { 701 }); // Hawlucha
            families.Add(new ushort[] { 702 }); // Dedenne
            families.Add(new ushort[] { 703 }); // Carbink
            families.Add(new ushort[] { 704, 705, 706 }); // Goomy
            families.Add(new ushort[] { 707 }); // Klefki
            families.Add(new ushort[] { 708, 709 }); // Phantump
            families.Add(new ushort[] { 710, 711 }); // Pumpkaboo
            families.Add(new ushort[] { 712, 713 }); // Bergmite
            families.Add(new ushort[] { 714, 715 }); // Noibat
            families.Add(new ushort[] { 716 }); // Xerneas
            families.Add(new ushort[] { 717 }); // Yveltal
            families.Add(new ushort[] { 718 }); // Zygarde
            families.Add(new ushort[] { 719 }); // Diancie
            families.Add(new ushort[] { 720 }); // Hoopa
            families.Add(new ushort[] { 721 }); // Volcanion
            families.Add(new ushort[] { 722, 723, 724 }); // Rowlet
            families.Add(new ushort[] { 725, 726, 727 }); // Litten
            families.Add(new ushort[] { 728, 729, 730 }); // Popplio
            families.Add(new ushort[] { 731, 732, 733 }); // Pikipek
            families.Add(new ushort[] { 734, 735 }); // Yungoos
            families.Add(new ushort[] { 736, 737, 738 }); // Grubbin
            families.Add(new ushort[] { 739, 740 }); // Crabrawler
            families.Add(new ushort[] { 741 }); // Oricorio
            families.Add(new ushort[] { 742, 743 }); // Cutiefly
            families.Add(new ushort[] { 744, 745 }); // Rockruff
            families.Add(new ushort[] { 746 }); // Wishiwashi
            families.Add(new ushort[] { 747, 748 }); // Mareanie
            families.Add(new ushort[] { 749, 750 }); // Mudbray
            families.Add(new ushort[] { 751, 752 }); // Dewpider
            families.Add(new ushort[] { 753, 754 }); // Fomantis
            families.Add(new ushort[] { 755, 756 }); // Morelull
            families.Add(new ushort[] { 757, 758 }); // Salandit
            families.Add(new ushort[] { 759, 760 }); // Stufful
            families.Add(new ushort[] { 761, 762, 763 }); // Bounsweet
            families.Add(new ushort[] { 764 }); // Comfey
            families.Add(new ushort[] { 765 }); // Oranguru
            families.Add(new ushort[] { 766 }); // Passimian
            families.Add(new ushort[] { 767, 768 }); // Wimpod
            families.Add(new ushort[] { 769, 770 }); // Sandygast
            families.Add(new ushort[] { 771 }); // Pyukumuku
            families.Add(new ushort[] { 772, 773 }); // Type: Null
            families.Add(new ushort[] { 774 }); // Minior
            families.Add(new ushort[] { 775 }); // Komala
            families.Add(new ushort[] { 776 }); // Turtonator
            families.Add(new ushort[] { 777 }); // Togedemaru
            families.Add(new ushort[] { 778 }); // Mimikyu
            families.Add(new ushort[] { 779 }); // Bruxish
            families.Add(new ushort[] { 780 }); // Drampa
            families.Add(new ushort[] { 781 }); // Dhelmise
            families.Add(new ushort[] { 782, 783, 784 }); // Jangmo-o
            families.Add(new ushort[] { 785 }); // Tapu Koko
            families.Add(new ushort[] { 786 }); // Tapu Lele
            families.Add(new ushort[] { 787 }); // Tapu Bulu
            families.Add(new ushort[] { 788 }); // Tapu Fini
            families.Add(new ushort[] { 789, 790, 791, 792 }); // Cosmog
            families.Add(new ushort[] { 793 }); // Nihilego
            families.Add(new ushort[] { 794 }); // Buzzwole
            families.Add(new ushort[] { 795 }); // Pheromosa
            families.Add(new ushort[] { 796 }); // Xurkitree
            families.Add(new ushort[] { 797 }); // Kartana
            families.Add(new ushort[] { 798 }); // Celesteela
            families.Add(new ushort[] { 799 }); // Guzzlord
            families.Add(new ushort[] { 800 }); // Necrozma
            families.Add(new ushort[] { 801 }); // Magearna
            families.Add(new ushort[] { 802 }); // Marshadow
            families.Add(new ushort[] { 803, 804 }); // Poipole
            families.Add(new ushort[] { 805 }); // Stakataka
            families.Add(new ushort[] { 806 }); // Blacephalon
            families.Add(new ushort[] { 807 }); // Zeraora
            families.Add(new ushort[] { 808, 809 }); // Meltan
            families.Add(new ushort[] { 810, 811, 812 }); // Grookey
            families.Add(new ushort[] { 813, 814, 815 }); // Scorbunny
            families.Add(new ushort[] { 816, 817, 818 }); // Sobble
            families.Add(new ushort[] { 819, 820 }); // Skwovet
            families.Add(new ushort[] { 821, 822, 823 }); // Rookidee
            families.Add(new ushort[] { 824, 825, 826 }); // Blipbug
            families.Add(new ushort[] { 827, 828 }); // Nickit
            families.Add(new ushort[] { 829, 830 }); // Gossifleur
            families.Add(new ushort[] { 831, 832 }); // Wooloo
            families.Add(new ushort[] { 833, 834 }); // Chewtle
            families.Add(new ushort[] { 835, 836 }); // Yamper
            families.Add(new ushort[] { 837, 838, 839 }); // Rolycoly
            families.Add(new ushort[] { 840, 841, 842 }); // Applin
            families.Add(new ushort[] { 843, 844 }); // Silicobra
            families.Add(new ushort[] { 845 }); // Cramorant
            families.Add(new ushort[] { 846, 847 }); // Arrokuda
            families.Add(new ushort[] { 848, 849 }); // Toxel
            families.Add(new ushort[] { 850, 851 }); // Sizzlipede
            families.Add(new ushort[] { 852, 853 }); // Clobbopus
            families.Add(new ushort[] { 854, 855 }); // Sinistea
            families.Add(new ushort[] { 856, 857, 858 }); // Hattena
            families.Add(new ushort[] { 859, 860, 861 }); // Impidimp
            families.Add(new ushort[] { 868, 869 }); // Milcery
            families.Add(new ushort[] { 870 }); // Falinks
            families.Add(new ushort[] { 871 }); // Pincurchin
            families.Add(new ushort[] { 872, 873 }); // Snom
            families.Add(new ushort[] { 874 }); // Stonjourner
            families.Add(new ushort[] { 875 }); // Eiscue
            families.Add(new ushort[] { 876 }); // Indeedee
            families.Add(new ushort[] { 877 }); // Morpeko
            families.Add(new ushort[] { 878, 879 }); // Cufant
            families.Add(new ushort[] { 880 }); // Dracozolt
            families.Add(new ushort[] { 881 }); // Arctozolt
            families.Add(new ushort[] { 882 }); // Dracovish
            families.Add(new ushort[] { 883 }); // Arctovish
            families.Add(new ushort[] { 884 }); // Duraludon
            families.Add(new ushort[] { 885, 886, 887 }); // Dreepy
            families.Add(new ushort[] { 888 }); // Zacian
            families.Add(new ushort[] { 889 }); // Zamazenta
            families.Add(new ushort[] { 890 }); // Eternatus
            families.Add(new ushort[] { 891, 892 }); // Kubfu
            families.Add(new ushort[] { 893 }); // Zarude
            families.Add(new ushort[] { 894 }); // Regieleki
            families.Add(new ushort[] { 895 }); // Regidrago
            families.Add(new ushort[] { 896 }); // Glastrier
            families.Add(new ushort[] { 897 }); // Spectrier
            families.Add(new ushort[] { 898 }); // Calyrex
            families.Add(new ushort[] { 905 }); // Enamorus
            families.Add(new ushort[] { 906, 907, 908 }); // Sprigatito
            families.Add(new ushort[] { 909, 910, 911 }); // Fuecoco
            families.Add(new ushort[] { 912, 913, 914 }); // Quaxly
            families.Add(new ushort[] { 915, 916 }); // Lechonk
            families.Add(new ushort[] { 918, 919 }); // Tarountula
            families.Add(new ushort[] { 920, 921 }); // Nymble
            families.Add(new ushort[] { 922, 923 }); // Rellor
            families.Add(new ushort[] { 924, 925 }); // Greavard
            families.Add(new ushort[] { 926, 927 }); // Flittle
            families.Add(new ushort[] { 929, 930 }); // Wiglett
            families.Add(new ushort[] { 931 }); // Dodonzo
            families.Add(new ushort[] { 932 }); // Veluza
            families.Add(new ushort[] { 933, 934 }); // Finizen
            families.Add(new ushort[] { 935, 936, 937 }); // Smoliv
            families.Add(new ushort[] { 938, 939 }); // Capsakid
            families.Add(new ushort[] { 940, 941 }); // Tadbulb
            families.Add(new ushort[] { 942, 943 }); // Varoom
            families.Add(new ushort[] { 944 }); // Orthworm
            families.Add(new ushort[] { 945, 946 }); // Tandemaus
            families.Add(new ushort[] { 947, 948 }); // Cetoddle
            families.Add(new ushort[] { 949, 950, 951}); // Frigibax
            families.Add(new ushort[] { 952 }); // Tatsugiri
            families.Add(new ushort[] { 953 }); // Cyclizar
            families.Add(new ushort[] { 954, 955, 956 }); // Pawmi
            families.Add(new ushort[] { 957, 958 }); // Wattrel
            families.Add(new ushort[] { 959 }); // Bombirdier
            families.Add(new ushort[] { 960 }); // Squawkabilly
            families.Add(new ushort[] { 961 }); // Flamigo
            families.Add(new ushort[] { 962 }); // Klawf
            families.Add(new ushort[] { 963, 964, 965 }); // Nacli
            families.Add(new ushort[] { 966, 967 }); // Glimmet
            families.Add(new ushort[] { 968, 969 }); // Shroodle
            families.Add(new ushort[] { 970, 971 }); // Fidough
            families.Add(new ushort[] { 972, 973 }); // Maschiff
            families.Add(new ushort[] { 974, 975 }); // Bramblin
            families.Add(new ushort[] { 976, 977 }); // Gimmighoul
            families.Add(new ushort[] { 978 }); // Great Tusk
            families.Add(new ushort[] { 979 }); // Brute Bonnet
            families.Add(new ushort[] { 981 }); // Sandy Shocks
            families.Add(new ushort[] { 982 }); // Scream Tail
            families.Add(new ushort[] { 983 }); // Flutter Mane
            families.Add(new ushort[] { 984 }); // Slither Wing
            families.Add(new ushort[] { 985 }); // Roaring Moon
            families.Add(new ushort[] { 986 }); // Iron Treads
            families.Add(new ushort[] { 988 }); // Iron Moth
            families.Add(new ushort[] { 989 }); // Iron Hands
            families.Add(new ushort[] { 990 }); // Iron Jugulis
            families.Add(new ushort[] { 991 }); // Iron Thorns
            families.Add(new ushort[] { 992 }); // Iron Bundle
            families.Add(new ushort[] { 993 }); // Iron Valiant
            families.Add(new ushort[] { 994 }); // Ting-Lu
            families.Add(new ushort[] { 995 }); // Chien-Pao
            families.Add(new ushort[] { 996 }); // Wo-Chien
            families.Add(new ushort[] { 997 }); // Chi-Yu
            families.Add(new ushort[] { 998 }); // Koraidon
            families.Add(new ushort[] { 999 }); // Miraidon
            families.Add(new ushort[] { 1000, 1001, 1002 }); // Tinkatink
            families.Add(new ushort[] { 1003, 1004, 1005 }); // Charcadet
            families.Add(new ushort[] { 1006, 1007 }); // Toedscool
        }
    }
}
