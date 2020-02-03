using System.Collections.Generic;
using System.Linq;

namespace MonCollection
{
    class MonFamily
    {
        private readonly List<int[]> families;

        public MonFamily()
        {
            families = new List<int[]>();
            AddMons();
        }

        public int[] GetFamily(int species)
        {
            foreach(int[] fam in families)
            {
                if (fam.Contains(species))
                    return fam;
            }
            return new int[] { species };
        }

        private void AddMons()
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
            families.Add(new int[] { 52, 53, 863 }); //Meowth
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
            families.Add(new int[] { 83, 865 }); //Farfetch'd
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
            families.Add(new int[] { 439, 122, 866 }); //Mime Jr.
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
            families.Add(new int[] { 222, 864 }); //Corsola
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
            families.Add(new int[] { 263, 264, 862 }); //Zigzagoon
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
            families.Add(new int[] { 387, 388, 389 }); //Turtwig
            families.Add(new int[] { 390, 391, 392 }); //Chimchar
            families.Add(new int[] { 393, 394, 395 }); //Piplup
            families.Add(new int[] { 396, 397, 398 }); //Starly
            families.Add(new int[] { 399, 400 }); //Bidoof
            families.Add(new int[] { 401, 402 }); //Kricketot
            families.Add(new int[] { 403, 404, 405 }); //Shinx
            families.Add(new int[] { 408, 409 }); //Cranidos
            families.Add(new int[] { 410, 411 }); //Shieldon
            families.Add(new int[] { 412, 413, 414 }); //Burmy
            families.Add(new int[] { 415, 416 }); //Combee
            families.Add(new int[] { 417 }); //Pachirisu
            families.Add(new int[] { 418, 419 }); //Buizel
            families.Add(new int[] { 420, 421 }); //Cherubi
            families.Add(new int[] { 422, 423 }); //Shellos
            families.Add(new int[] { 425, 426 }); //Drifloon
            families.Add(new int[] { 427, 428 }); //Buneary
            families.Add(new int[] { 431, 432 }); //Glameow
            families.Add(new int[] { 433, 434 }); //Stunky
            families.Add(new int[] { 436, 437 }); //Bronzor
            families.Add(new int[] { 441 }); //Chatot
            families.Add(new int[] { 442 }); //Spiritomb
            families.Add(new int[] { 443, 444, 445 }); //Gible
            families.Add(new int[] { 447, 448 }); //Riolu
            families.Add(new int[] { 449, 450 }); //Hippopotas
            families.Add(new int[] { 451, 452 }); //Skorupi
            families.Add(new int[] { 453, 454 }); //Croagunk
            families.Add(new int[] { 455 }); //Carnivine
            families.Add(new int[] { 456, 457 }); //Finneon
            families.Add(new int[] { 459, 460 }); //Snover
            families.Add(new int[] { 479 }); //Rotom
            families.Add(new int[] { 480 }); //Uxie
            families.Add(new int[] { 481 }); //Mesprit
            families.Add(new int[] { 482 }); //Azelf
            families.Add(new int[] { 483 }); //Dialga
            families.Add(new int[] { 484 }); //Palkia
            families.Add(new int[] { 485 }); //Heatran
            families.Add(new int[] { 486 }); //Regigigas
            families.Add(new int[] { 487 }); //Giratina
            families.Add(new int[] { 488 }); //Cresselia
            families.Add(new int[] { 489, 490 }); //Phione
            families.Add(new int[] { 491 }); //Darkrai
            families.Add(new int[] { 492 }); //Shaymin
            families.Add(new int[] { 493 }); //Arceus
            families.Add(new int[] { 494 }); //Victini
            families.Add(new int[] { 495, 496, 497 }); //Snivy
            families.Add(new int[] { 498, 499, 500 }); //Tepig
            families.Add(new int[] { 501, 502, 503 }); //Oshawott
            families.Add(new int[] { 504, 505 }); //Patrat
            families.Add(new int[] { 506, 507, 508 }); //Lillipup
            families.Add(new int[] { 509, 510 }); //Purrloin
            families.Add(new int[] { 511, 512 }); //Pansage
            families.Add(new int[] { 513, 514 }); //Pansear
            families.Add(new int[] { 515, 516 }); //Panpour
            families.Add(new int[] { 517, 518 }); //Munna
            families.Add(new int[] { 519, 520, 521 }); //Pidove
            families.Add(new int[] { 522, 523 }); //Blitzle
            families.Add(new int[] { 524, 525, 526 }); //Roggenrola
            families.Add(new int[] { 527, 528 }); //Woobat
            families.Add(new int[] { 529, 530 }); //Drilbur
            families.Add(new int[] { 531 }); //Audino
            families.Add(new int[] { 532, 533, 534 }); //Timburr
            families.Add(new int[] { 535, 536, 537 }); //Tympole
            families.Add(new int[] { 538 }); //Throh
            families.Add(new int[] { 539 }); //Sawk
            families.Add(new int[] { 540, 541, 542 }); //Sewaddle
            families.Add(new int[] { 543, 544, 545 }); //Venipede
            families.Add(new int[] { 546, 547 }); //Cottonee
            families.Add(new int[] { 548, 549 }); //Petilil
            families.Add(new int[] { 550 }); //Basculin
            families.Add(new int[] { 551, 552, 553 }); //Sandile
            families.Add(new int[] { 554, 555 }); //Darumaka
            families.Add(new int[] { 556 }); //Maractus
            families.Add(new int[] { 557, 558 }); //Dwebble
            families.Add(new int[] { 559, 560 }); //Scraggy
            families.Add(new int[] { 561 }); //Sigilyph
            families.Add(new int[] { 562, 563, 867 }); //Yamask
            families.Add(new int[] { 564, 565 }); //Tirtouga
            families.Add(new int[] { 566, 567 }); //Archen
            families.Add(new int[] { 568, 569 }); //Trubbish
            families.Add(new int[] { 570, 571 }); //Zorua
            families.Add(new int[] { 572, 573 }); //Minccino
            families.Add(new int[] { 574, 575, 576 }); //Gothita
            families.Add(new int[] { 577, 578, 579 }); //Solosis
            families.Add(new int[] { 580, 581 }); //Ducklett
            families.Add(new int[] { 582, 583, 584 }); //Vanillite
            families.Add(new int[] { 585, 586 }); //Deerling
            families.Add(new int[] { 587 }); //Emolga
            families.Add(new int[] { 588, 589 }); //Karrablast
            families.Add(new int[] { 590, 591 }); //Foongus
            families.Add(new int[] { 592, 593 }); //Frillish
            families.Add(new int[] { 594 }); //Alomomola
            families.Add(new int[] { 595, 596 }); //Joltik
            families.Add(new int[] { 597, 598 }); //Ferroseed
            families.Add(new int[] { 599, 600, 601 }); //Klink
            families.Add(new int[] { 602, 603, 604 }); //Tynamo
            families.Add(new int[] { 605, 606 }); //Elgyem
            families.Add(new int[] { 607, 608, 609 }); //Litwick
            families.Add(new int[] { 610, 611, 612 }); //Axew
            families.Add(new int[] { 613, 614 }); //Beartic
            families.Add(new int[] { 615 }); //Cryogonal
            families.Add(new int[] { 616, 617 }); //Shelmet
            families.Add(new int[] { 618 }); //Stunfisk
            families.Add(new int[] { 619, 620 }); //Mienfoo
            families.Add(new int[] { 621 }); //Druddigon
            families.Add(new int[] { 622, 623 }); //Golett
            families.Add(new int[] { 624, 625 }); //Pawniard
            families.Add(new int[] { 626 }); //Bouffalant
            families.Add(new int[] { 627, 628 }); //Rufflet
            families.Add(new int[] { 629, 630 }); //Vullaby
            families.Add(new int[] { 631 }); //Heatmor
            families.Add(new int[] { 632 }); //Durant
            families.Add(new int[] { 633, 634, 635 }); //Deino
            families.Add(new int[] { 636, 637 }); //Larvesta
            families.Add(new int[] { 638 }); //Cobalion
            families.Add(new int[] { 639 }); //Terrakion
            families.Add(new int[] { 640 }); //Virizion
            families.Add(new int[] { 641 }); //Tornadus
            families.Add(new int[] { 642 }); //Thundurus
            families.Add(new int[] { 643 }); //Reshiram
            families.Add(new int[] { 644 }); //Zekrom
            families.Add(new int[] { 645 }); //Landorus
            families.Add(new int[] { 646 }); //Kyurem
            families.Add(new int[] { 647 }); //Keldeo
            families.Add(new int[] { 648 }); //Meloetta
            families.Add(new int[] { 649 }); //Genesect
            families.Add(new int[] { 650, 651, 652 }); //Chespin
            families.Add(new int[] { 653, 654, 655 }); //Fennekin
            families.Add(new int[] { 656, 657, 658 }); //Froakie
            families.Add(new int[] { 659, 660 }); //Bunnelby
            families.Add(new int[] { 661, 662, 663 }); //Fletchling
            families.Add(new int[] { 664, 665, 666 }); //Scatterbug
            families.Add(new int[] { 667, 668 }); //Litleo
            families.Add(new int[] { 669, 670, 671 }); //Flabébé
            families.Add(new int[] { 672, 673 }); //Skiddo
            families.Add(new int[] { 674, 675 }); //Pancham
            families.Add(new int[] { 676 }); //Furfrou
            families.Add(new int[] { 677, 678 }); //Espurr
            families.Add(new int[] { 679, 680, 681 }); //Honedge
            families.Add(new int[] { 682, 683 }); //Spritzee
            families.Add(new int[] { 684, 685 }); //Swirlix
            families.Add(new int[] { 686, 687 }); //Inkay
            families.Add(new int[] { 688, 689 }); //Binacle
            families.Add(new int[] { 690, 691 }); //Skrelp
            families.Add(new int[] { 692, 693 }); //Clauncher
            families.Add(new int[] { 694, 695 }); //Helioptile
            families.Add(new int[] { 696, 697 }); //Tyrunt
            families.Add(new int[] { 698, 699 }); //Amaura
            families.Add(new int[] { 701 }); //Hawlucha
            families.Add(new int[] { 702 }); //Dedenne
            families.Add(new int[] { 703 }); //Carbink
            families.Add(new int[] { 704, 705, 706 }); //Goomy
            families.Add(new int[] { 707 }); //Klefki
            families.Add(new int[] { 708, 709 }); //Phantump
            families.Add(new int[] { 710, 711 }); //Pumpkaboo
            families.Add(new int[] { 712, 713 }); //Bergmite
            families.Add(new int[] { 714, 715 }); //Noibat
            families.Add(new int[] { 716 }); //Xerneas
            families.Add(new int[] { 717 }); //Yveltal
            families.Add(new int[] { 718 }); //Zygarde
            families.Add(new int[] { 719 }); //Diancie
            families.Add(new int[] { 720 }); //Hoopa
            families.Add(new int[] { 721 }); //Volcanion
            families.Add(new int[] { 722, 723, 724 }); //Rowlet
            families.Add(new int[] { 725, 726, 727 }); //Litten
            families.Add(new int[] { 728, 729, 730 }); //Popplio
            families.Add(new int[] { 731, 732, 733 }); //Pikipek
            families.Add(new int[] { 734, 735 }); //Yungoos
            families.Add(new int[] { 736, 737, 738 }); //Grubbin
            families.Add(new int[] { 739, 740 }); //Crabrawler
            families.Add(new int[] { 741 }); //Oricorio
            families.Add(new int[] { 742, 743 }); //Cutiefly
            families.Add(new int[] { 744, 745 }); //Rockruff
            families.Add(new int[] { 746 }); //Wishiwashi
            families.Add(new int[] { 747, 748 }); //Mareanie
            families.Add(new int[] { 749, 750 }); //Mudbray
            families.Add(new int[] { 751, 752 }); //Dewpider
            families.Add(new int[] { 753, 754 }); //Fomantis
            families.Add(new int[] { 755, 756 }); //Morelull
            families.Add(new int[] { 757, 758 }); //Salandit
            families.Add(new int[] { 759, 760 }); //Stufful
            families.Add(new int[] { 761, 762, 763 }); //Bounsweet
            families.Add(new int[] { 764 }); //Comfey
            families.Add(new int[] { 765 }); //Oranguru
            families.Add(new int[] { 766 }); //Passimian
            families.Add(new int[] { 767, 768 }); //Wimpod
            families.Add(new int[] { 769, 770 }); //Sandygast
            families.Add(new int[] { 771 }); //Pyukumuku
            families.Add(new int[] { 772, 773 }); //Type: Null
            families.Add(new int[] { 774 }); //Minior
            families.Add(new int[] { 775 }); //Komala
            families.Add(new int[] { 776 }); //Turtonator
            families.Add(new int[] { 777 }); //Togedemaru
            families.Add(new int[] { 778 }); //Mimikyu
            families.Add(new int[] { 779 }); //Bruxish
            families.Add(new int[] { 780 }); //Drampa
            families.Add(new int[] { 781 }); //Dhelmise
            families.Add(new int[] { 782, 783, 784 }); //Jangmo-o
            families.Add(new int[] { 785 }); //Tapu Koko
            families.Add(new int[] { 786 }); //Tapu Lele
            families.Add(new int[] { 787 }); //Tapu Bulu
            families.Add(new int[] { 788 }); //Tapu Fini
            families.Add(new int[] { 789, 790, 791, 792 }); //Cosmog
            families.Add(new int[] { 793 }); //Nihilego
            families.Add(new int[] { 794 }); //Buzzwole
            families.Add(new int[] { 795 }); //Pheromosa
            families.Add(new int[] { 796 }); //Xurkitree
            families.Add(new int[] { 797 }); //Kartana
            families.Add(new int[] { 798 }); //Celesteela
            families.Add(new int[] { 799 }); //Guzzlord
            families.Add(new int[] { 800 }); //Necrozma
            families.Add(new int[] { 801 }); //Magearna
            families.Add(new int[] { 802 }); //Marshadow
            families.Add(new int[] { 803, 804 }); //Poipole
            families.Add(new int[] { 805 }); //Stakataka
            families.Add(new int[] { 806 }); //Blacephalon
            families.Add(new int[] { 807 }); //Zeraora
            families.Add(new int[] { 808, 809 }); //Meltan
            families.Add(new int[] { 810, 811, 812 }); //Grookey
            families.Add(new int[] { 813, 814, 815 }); //Scorbunny
            families.Add(new int[] { 816, 817, 818 }); //Sobble
            families.Add(new int[] { 819, 820 }); //Skwovet
            families.Add(new int[] { 821, 822, 823 }); //Rookidee
            families.Add(new int[] { 824, 825, 826 }); //Blipbug
            families.Add(new int[] { 827, 828 }); //Nickit
            families.Add(new int[] { 829, 830 }); //Gossifleur
            families.Add(new int[] { 831, 832 }); //Wooloo
            families.Add(new int[] { 833, 834 }); //Chewtle
            families.Add(new int[] { 835, 836 }); //Yamper
            families.Add(new int[] { 837, 838, 839 }); //Rolycoly
            families.Add(new int[] { 840, 841, 842 }); //Applin
            families.Add(new int[] { 843, 844 }); //Silicobra
            families.Add(new int[] { 845 }); //Cramorant
            families.Add(new int[] { 846, 847 }); //Arrokuda
            families.Add(new int[] { 848, 849 }); //Toxel
            families.Add(new int[] { 850, 851 }); //Sizzlipede
            families.Add(new int[] { 852, 853 }); //Clobbopus
            families.Add(new int[] { 854, 855 }); //Sinistea
            families.Add(new int[] { 856, 857, 858 }); //Hattena
            families.Add(new int[] { 859, 860, 861 }); //Impidimp
            families.Add(new int[] { 868, 869 }); //Milcery
            families.Add(new int[] { 870 }); //Falinks
            families.Add(new int[] { 871 }); //Pincurchin
            families.Add(new int[] { 872, 873 }); //Snom
            families.Add(new int[] { 874 }); //Stonjourner
            families.Add(new int[] { 875 }); //Eiscue
            families.Add(new int[] { 876 }); //Indeedee
            families.Add(new int[] { 877 }); //Morpeko
            families.Add(new int[] { 878, 879 }); //Cuphant
            families.Add(new int[] { 880 }); //Dracozolt
            families.Add(new int[] { 881 }); //Arctozolt
            families.Add(new int[] { 882 }); //Dracovish
            families.Add(new int[] { 883 }); //Arctovish
            families.Add(new int[] { 884 }); //Duraludon
            families.Add(new int[] { 885, 886, 887 }); //Dreepy
            families.Add(new int[] { 888 }); //Zacian
            families.Add(new int[] { 889 }); //Zamazenta
            families.Add(new int[] { 890 }); //Eternatus
        }
    }
}
