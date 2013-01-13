/*
 * List of All Creatures to be used to determine whether creature or player
 * Credits goes to brunelli1989
*/

using System;
using System.Collections.Generic;
using Pokemon.Objects;

namespace Pokemon.Constants
{
    public static class Creatures
    {
        public static CreatureData Bulbasaur = new CreatureData("Bulbasaur", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Poison },
            new List<string>() { "", "", "Bulbasaur", },
            new List<Loot>() { }
        );

        public static CreatureData Ivysaur = new CreatureData("Ivysaur", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Poison },
            new List<string>() { "", "", "Ivysaur", },
            new List<Loot>() { }
        );

        public static CreatureData Venusaur = new CreatureData("Venusaur", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Poison },
            new List<string>() { "", "", "Venusaur", },
            new List<Loot>() { }
        );

        public static CreatureData Charmander = new CreatureData("Charmander", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Charmander", },
            new List<Loot>() { }
        );

        public static CreatureData Charmeleon = new CreatureData("Charmeleon", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Charmeleon", },
            new List<Loot>() { }
        );

        public static CreatureData Charizard = new CreatureData("Charizard", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire, DamageType.Flying },
            new List<string>() { "", "", "Charizard", },
            new List<Loot>() { }
        );

        public static CreatureData Squirtle = new CreatureData("Squirtle", 0, 20, 0, 0, FrontAttack.None,

            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Squirtle", },
            new List<Loot>() { }
        );

        public static CreatureData Wartortle = new CreatureData("Wartortle", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Wartortle", },
            new List<Loot>() { }
        );

        public static CreatureData Blastoise = new CreatureData("Blastoise", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Blastoise", },
            new List<Loot>() { }
        );

        public static CreatureData Caterpie = new CreatureData("Caterpie", 0, 1, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug },
            new List<string>() { "", "", "Caterpie", },
            new List<Loot>() { }
        );

        public static CreatureData Metapod = new CreatureData("Metapod", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug },
            new List<string>() { "", "", "Metapod", },
            new List<Loot>() { }
        );

        public static CreatureData Butterfree = new CreatureData("Butterfree", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug },
            new List<string>() { "", "", "Butterfree", },
            new List<Loot>() { }
        );

        public static CreatureData Weedle = new CreatureData("Weedle", 0, 1, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Poison },
            new List<string>() { "", "", "Weedle", },
            new List<Loot>() { }
        );

        public static CreatureData Kakuna = new CreatureData("Kakuna", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Poison },
            new List<string>() { "", "", "Kakuna", },
            new List<Loot>() { }
        );
        public static CreatureData Beedrill = new CreatureData("Beedrill", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Poison },
            new List<string>() { "", "", "Kakuna", },
            new List<Loot>() { }
        );
        public static CreatureData Pidgey = new CreatureData("Pidgey", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "Pidgey", },
            new List<Loot>() { }
        );
        public static CreatureData Pidgotto = new CreatureData("Pidgotto", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "Pidgotto", },
            new List<Loot>() { }
        );
        public static CreatureData Pidgeot = new CreatureData("Pidgeot", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "Pidgeot", },
            new List<Loot>() { }
        );
        public static CreatureData Rattata = new CreatureData("Rattata", 0, 1, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Rattata", },
            new List<Loot>() { }
        );
        public static CreatureData Raticate = new CreatureData("Raticate", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Raticate", },
            new List<Loot>() { }
        );
        public static CreatureData Spearow = new CreatureData("Spearow", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "Spearow", },
            new List<Loot>() { }
        );
        public static CreatureData Fearow = new CreatureData("Fearow", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "Fearow", },
            new List<Loot>() { }
        );
        public static CreatureData Ekans = new CreatureData("Ekans", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Ekans", },
            new List<Loot>() { }
        );
        public static CreatureData Arbok = new CreatureData("Arbok", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Arbok", },
            new List<Loot>() { }
        );
        public static CreatureData Pikachu = new CreatureData("Pikachu", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "Pikachu", },
            new List<Loot>() { }
        );
        public static CreatureData Raichu = new CreatureData("Raichu", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "Raichu", },
            new List<Loot>() { }
        );
        public static CreatureData Sandshrew = new CreatureData("Sandshrew", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "Sandshrew", },
            new List<Loot>() { }
        );
        public static CreatureData Sandslash = new CreatureData("Sandslash", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "Sandslash", },
            new List<Loot>() { }
        );
        public static CreatureData NidoranFe = new CreatureData("Nidoran", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Nidoran", },
            new List<Loot>() { }
        );
        public static CreatureData Nidorina = new CreatureData("Nidorina", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Nidorina", },
            new List<Loot>() { }
        );
        public static CreatureData Nidoqueen = new CreatureData("Nidoqueen", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Ground },
            new List<string>() { "", "", "Nidoqueen", },
            new List<Loot>() { }
        );
        public static CreatureData NidoranMa = new CreatureData("Nidoran", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Nidoran", },
            new List<Loot>() { }
        );
        public static CreatureData Nidorino = new CreatureData("Nidorino", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Nidorino", },
            new List<Loot>() { }
        );
        public static CreatureData Nidoking = new CreatureData("Nidoking", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Ground },
            new List<string>() { "", "", "Nidoking", },
            new List<Loot>() { }
        );
        public static CreatureData Clefairy = new CreatureData("Clefairy", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Clefairy", },
            new List<Loot>() { }
        );
        public static CreatureData Clefable = new CreatureData("Clefable", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Clefable", },
            new List<Loot>() { }
        );
        public static CreatureData Vulpix = new CreatureData("Vulpix", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Vulpix", },
            new List<Loot>() { }
        );
        public static CreatureData Ninetales = new CreatureData("Ninetales", 0, 70, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Ninetales", },
            new List<Loot>() { }
        );
        public static CreatureData Jigglypuff = new CreatureData("Jigglypuff", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Jigglypuff", },
            new List<Loot>() { }
        );
        public static CreatureData Wigglytuff = new CreatureData("Wigglytuff", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Wigglytuff", },
            new List<Loot>() { }
        );
        public static CreatureData Zubat = new CreatureData("Zubat", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Flying },
            new List<string>() { "", "", "Zubat", },
            new List<Loot>() { }
        );
        public static CreatureData Golbat = new CreatureData("Golbat", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Flying },
            new List<string>() { "", "", "Golbat", },
            new List<Loot>() { }
        );
        public static CreatureData Oddish = new CreatureData("Oddish", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Grass },
            new List<string>() { "", "", "Oddish", },
            new List<Loot>() { }
        );
        public static CreatureData Gloom = new CreatureData("Gloom", 0, 25, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Grass },
            new List<string>() { "", "", "Gloom", },
            new List<Loot>() { }
        );
        public static CreatureData Vileplume = new CreatureData("Vileplume", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Grass },
            new List<string>() { "", "", "Vileplume", },
            new List<Loot>() { }
        );
        public static CreatureData Paras = new CreatureData("Paras", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Grass },
            new List<string>() { "", "", "Paras", },
            new List<Loot>() { }
        );
        public static CreatureData Parasect = new CreatureData("Parasect", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Grass },
            new List<string>() { "", "", "Parasect", },
            new List<Loot>() { }
        );
        public static CreatureData Venonat = new CreatureData("Venonat", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Poison },
            new List<string>() { "", "", "Venonat", },
            new List<Loot>() { }
        );
        public static CreatureData Venomoth = new CreatureData("Venomoth", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Poison },
            new List<string>() { "", "", "Venomoth", },
            new List<Loot>() { }
        );
        public static CreatureData Diglett = new CreatureData("Diglett", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "Diglett", },
            new List<Loot>() { }
        );
        public static CreatureData Dugtrio = new CreatureData("Dugtrio", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "Dugtrio", },
            new List<Loot>() { }
        );
        public static CreatureData Meowth = new CreatureData("Meowth", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Meowth", },
            new List<Loot>() { }
        );

        public static CreatureData Persian = new CreatureData("Persian", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Persian", },
            new List<Loot>() { }
        );
        public static CreatureData Psyduck = new CreatureData("Psyduck", 0, 20, 0, 0, FrontAttack.None,
           new List<DamageType>() { DamageType.Water },
           new List<string>() { "", "", "Psyduck", },
           new List<Loot>() { }
       );
        public static CreatureData Golduck = new CreatureData("Golduck", 0, 55, 0, 0, FrontAttack.None,
           new List<DamageType>() { DamageType.Water },
           new List<string>() { "", "", "Golduck", },
           new List<Loot>() { }
       );
        public static CreatureData Mankey = new CreatureData("Mankey", 0, 15, 0, 0, FrontAttack.None,
           new List<DamageType>() { DamageType.Fighting },
           new List<string>() { "", "", "Mankey", },
           new List<Loot>() { }
       );
        public static CreatureData Primeape = new CreatureData("Primeape", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "Primeape", },
            new List<Loot>() { }
        );
        public static CreatureData Growlithe = new CreatureData("Growlithe", 0, 25, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Growlithe", },
            new List<Loot>() { }
        );
        public static CreatureData Arcanine = new CreatureData("Arcanine", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Arcanine", },
            new List<Loot>() { }
        );
        public static CreatureData Poliwag = new CreatureData("Poliwag", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Poliwag", },
            new List<Loot>() { }
        );
        public static CreatureData Poliwhirl = new CreatureData("Poliwhirl", 0, 25, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Poliwhirl", },
            new List<Loot>() { }
        );
        public static CreatureData Poliwrath = new CreatureData("Poliwrath", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Fighting },
            new List<string>() { "", "", "Poliwrath", },
            new List<Loot>() { }
        );
        public static CreatureData Abra = new CreatureData("Abra", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "Abra", },
            new List<Loot>() { }
        );
        public static CreatureData Kadabra = new CreatureData("Kadabra", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "Kadabra", },
            new List<Loot>() { }
        );
        public static CreatureData Alakazam = new CreatureData("Alakazam", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "Alakazam", },
            new List<Loot>() { }
        );
        public static CreatureData Machop = new CreatureData("Machop", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "Machop", },
            new List<Loot>() { }
        );
        public static CreatureData Machoke = new CreatureData("Machoke", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "Machoke", },
            new List<Loot>() { }
        );
        public static CreatureData Machamp = new CreatureData("Machamp", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "Machamp", },
            new List<Loot>() { }
        );
        public static CreatureData Bellsprout = new CreatureData("Bellsprout", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Grass },
            new List<string>() { "", "", "Bellsprout", },
            new List<Loot>() { }
        );
        public static CreatureData Weepinbell = new CreatureData("Weepinbell", 0, 25, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Grass },
            new List<string>() { "", "", "Weepinbell", },
            new List<Loot>() { }
        );
        public static CreatureData Victreebel = new CreatureData("Victreebel", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Grass },
            new List<string>() { "", "", "Victreebel", },
            new List<Loot>() { }
        );
        public static CreatureData Tentacool = new CreatureData("Tentacool", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Water },
            new List<string>() { "", "", "Tentacool", },
            new List<Loot>() { }
        );
        public static CreatureData Tentacruel = new CreatureData("Tentacruel", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Water },
            new List<string>() { "", "", "Tentacruel", },
            new List<Loot>() { }
        );
        public static CreatureData Geodude = new CreatureData("Geodude", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground, DamageType.Rock },
            new List<string>() { "", "", "Geodude", },
            new List<Loot>() { }
        );
        public static CreatureData Graveler = new CreatureData("Graveler", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground, DamageType.Rock },
            new List<string>() { "", "", "Graveler", },
            new List<Loot>() { }
        );
        public static CreatureData Golem = new CreatureData("Golem", 0, 75, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground, DamageType.Rock },
            new List<string>() { "", "", "Golem", },
            new List<Loot>() { }
        );
        public static CreatureData Ponyta = new CreatureData("Ponyta", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Ponyta", },
            new List<Loot>() { }
        );
        public static CreatureData Rapidash = new CreatureData("Rapidash", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "Rapidash", },
            new List<Loot>() { }
        );
        public static CreatureData Slowpoke = new CreatureData("Slowpoke", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Psychic },
            new List<string>() { "", "", "Slowpoke", },
            new List<Loot>() { }
        );
        public static CreatureData Slowbro = new CreatureData("Slowbro", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Psychic },
            new List<string>() { "", "", "Slowbro", },
            new List<Loot>() { }
        );
        public static CreatureData Magnemite = new CreatureData("Magnemite", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric, DamageType.Steel },
            new List<string>() { "", "", "Magnemite", },
            new List<Loot>() { }
        );
        public static CreatureData Magneton = new CreatureData("Magneton", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric, DamageType.Steel },
            new List<string>() { "", "", "Magneton", },
            new List<Loot>() { }
        );
        public static CreatureData Farfetchd = new CreatureData("Farfetch'd", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Flying, DamageType.Normal },
            new List<string>() { "", "", "Farfetch'd", },
            new List<Loot>() { }
        );
        public static CreatureData Doduo = new CreatureData("Doduo", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Flying, DamageType.Normal },
            new List<string>() { "", "", "Doduo", },
            new List<Loot>() { }
        );
        public static CreatureData Dodrio = new CreatureData("Dodrio", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Flying, DamageType.Normal },
            new List<string>() { "", "", "Dodrio", },
            new List<Loot>() { }
        );
        public static CreatureData Seel = new CreatureData("Seel", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Seel", },
            new List<Loot>() { }
        );
        public static CreatureData Dewgong = new CreatureData("Dewgong", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Ice },
            new List<string>() { "", "", "Dewgong", },
            new List<Loot>() { }
        );
        public static CreatureData Grimer = new CreatureData("Grimer", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Grimer", },
            new List<Loot>() { }
        );
        public static CreatureData Muk = new CreatureData("Muk", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Muk", },
            new List<Loot>() { }
        );
        public static CreatureData Shellder = new CreatureData("Shellder", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Shellder", },
            new List<Loot>() { }
        );
        public static CreatureData Cloyster = new CreatureData("Cloyster", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Ice },
            new List<string>() { "", "", "Cloyster", },
            new List<Loot>() { }
        );
        public static CreatureData Gastly = new CreatureData("Gastly", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ghost, DamageType.Poison },
            new List<string>() { "", "", "Gastly", },
            new List<Loot>() { }
        );
        public static CreatureData Haunter = new CreatureData("Haunter", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ghost, DamageType.Poison },
            new List<string>() { "", "", "Haunter", },
            new List<Loot>() { }
        );
        public static CreatureData Gengar = new CreatureData("Gengar", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ghost, DamageType.Poison },
            new List<string>() { "", "", "Gengar", },
            new List<Loot>() { }
        );
        public static CreatureData Onix = new CreatureData("Onix", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Rock, DamageType.Ground },
            new List<string>() { "", "", "Onix", },
            new List<Loot>() { }
        );
        public static CreatureData Drowzee = new CreatureData("Drowzee", 0, 25, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "Drowzee", },
            new List<Loot>() { }
        );
        public static CreatureData Hypno = new CreatureData("Hypno", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "Hypno", },
            new List<Loot>() { }
        );
        public static CreatureData Krabby = new CreatureData("Krabby", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Krabby", },
            new List<Loot>() { }
        );
        public static CreatureData Kingler = new CreatureData("Kingler", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "Kingler", },
            new List<Loot>() { }
        );
        public static CreatureData Voltorb = new CreatureData("Voltorb", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "Voltorb", },
            new List<Loot>() { }
        );
        public static CreatureData Electrode = new CreatureData("Electrode", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "Electrode", },
            new List<Loot>() { }
        );
        public static CreatureData Exeggcute = new CreatureData("Exeggcute", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Psychic },
            new List<string>() { "", "", "Exeggcute", },
            new List<Loot>() { }
        );
        public static CreatureData Exeggutor = new CreatureData("Exeggutor", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Psychic },
            new List<string>() { "", "", "Exeggutor", },
            new List<Loot>() { }
        );
        public static CreatureData Cubone = new CreatureData("Cubone", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "Cubone", },
            new List<Loot>() { }
        );
        public static CreatureData Marowak = new CreatureData("Marowak", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "Marowak", },
            new List<Loot>() { }
        );
        public static CreatureData Hitmonlee = new CreatureData("Hitmonlee", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "Hitmonlee", },
            new List<Loot>() { }
        );
        public static CreatureData Hitmonchan = new CreatureData("Hitmonchan", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "Hitmonchan", },
            new List<Loot>() { }
        );
        public static CreatureData Lickitung = new CreatureData("Lickitung", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "Lickitung", },
            new List<Loot>() { }
        );
        public static CreatureData Koffing = new CreatureData("Koffing", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Koffing", },
            new List<Loot>() { }
        );
        public static CreatureData Weezing = new CreatureData("Weezing", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison },
            new List<string>() { "", "", "Weezing", },
            new List<Loot>() { }
        );
        public static CreatureData Rhyhorn = new CreatureData("Rhyhorn", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Rock, DamageType.Ground },
            new List<string>() { "", "", "Rhyhorn", },
            new List<Loot>() { }
        );
        public static CreatureData Rhydon = new CreatureData("Rhydon", 0, 75, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Rock, DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Chansey = new CreatureData("Chansey", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Tangela = new CreatureData("Tangela", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Kangaskhan = new CreatureData("Kangaskhan", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Horsea = new CreatureData("Horsea", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Seadra = new CreatureData("Seadra", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Goldeen = new CreatureData("Goldeen", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Seaking = new CreatureData("Seaking", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Staryu = new CreatureData("Staryu", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Starmie = new CreatureData("Starmie", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData MrMime = new CreatureData("Mr. Mime", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Scyther = new CreatureData("Scyther", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Jynx = new CreatureData("Jynx", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ice, DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Electabuzz = new CreatureData("Electabuzz", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Magmar = new CreatureData("Magmar", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Pinsir = new CreatureData("Pinsir", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Tauros = new CreatureData("Tauros", 0, 45, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Magikarp = new CreatureData("Magikarp", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Gyarados = new CreatureData("Gyarados", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Fighting },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Lapras = new CreatureData("Lapras", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Ice },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Ditto = new CreatureData("Ditto", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Eevee = new CreatureData("Eevee", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Vaporeon = new CreatureData("Vaporeon", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Jolteon = new CreatureData("Jolteon", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Flareon = new CreatureData("Flareon", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Porygon = new CreatureData("Porygon", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Omanyte = new CreatureData("Omanyte", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Omastar = new CreatureData("Omastar", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Kabuto = new CreatureData("Kabuto", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Aerodactyl = new CreatureData("Aerodactyl", 0, 100, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Flying, DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Snorlax = new CreatureData("Snorlax", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Articuno = new CreatureData("Articuno", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ice, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Zapdos = new CreatureData("Zapdos", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Moltres = new CreatureData("Moltres", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Dratini = new CreatureData("Dratini", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Dragon },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Dragonair = new CreatureData("Dragonair", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Dragon },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Dragonite = new CreatureData("Dragonite", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Dragon, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Mewtwo = new CreatureData("Mewtwo", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );

        public static CreatureData Mew = new CreatureData("Mew", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );

        public static CreatureData Chikorita = new CreatureData("Chikorita", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Bayleef = new CreatureData("Bayleef", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Meganium = new CreatureData("Meganium", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Cyndaquil = new CreatureData("Cyndaquil", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Quilava = new CreatureData("Quilava", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Typhlosion = new CreatureData("Typhlosion", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Totodile = new CreatureData("Totodile", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Croconaw = new CreatureData("Croconaw", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Feraligatr = new CreatureData("Feraligatr", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Sentret = new CreatureData("Sentret", 0, 0, 15, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Furret = new CreatureData("Furret", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Hoothoot = new CreatureData("Hoothoot", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Noctowl = new CreatureData("Noctowl", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Ledyba = new CreatureData("Ledyba", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Ledian = new CreatureData("Ledian", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Spinarak = new CreatureData("Spinarak", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Poison },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Ariados = new CreatureData("Ariados", 0, 35, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Poison },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Crobat = new CreatureData("Crobat", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Poison, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Chinchou = new CreatureData("Chinchou", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Lanturn = new CreatureData("Lanturn", 0, 75, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Pichu = new CreatureData("Pichu", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Cleffa = new CreatureData("Cleffa", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Igglybuff = new CreatureData("Igglybuff", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Togepi = new CreatureData("Togepi", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Togetic = new CreatureData("Togetic", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Natu = new CreatureData("Natu", 0, 25, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Xatu = new CreatureData("Xatu", 0, 75, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Mareep = new CreatureData("Mareep", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Flaaffy = new CreatureData("Flaaffy", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Ampharos = new CreatureData("Ampharos", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Bellossom = new CreatureData("Bellossom", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Marill = new CreatureData("Marill", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Azumarill = new CreatureData("Azumarill", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Sudowoodo = new CreatureData("Sudowoodo", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Politoed = new CreatureData("Politoed", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Hoppip = new CreatureData("Hoppip", 0, 5, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Skiploom = new CreatureData("Skiploom", 0, 25, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Jumpluff = new CreatureData("Jumpluff", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Aipom = new CreatureData("Aipom", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Sunkern = new CreatureData("Sunkern", 0, 1, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Sunflora = new CreatureData("Sunflora", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Yanma = new CreatureData("Yanma", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Wooper = new CreatureData("Wooper", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Quagsire = new CreatureData("Quagsire", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Espeon = new CreatureData("Espeon", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Umbreon = new CreatureData("Umbreon", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Dark },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Murkrow = new CreatureData("Murkrow", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Dark, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Slowking = new CreatureData("Slowking", 0, 100, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Misdreavus = new CreatureData("Misdreavus", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ghost },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Unown = new CreatureData("Unown", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Wobbuffet = new CreatureData("Wobbuffet", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Girafarig = new CreatureData("Girafarig", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric, DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Pineco = new CreatureData("Pineco", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Forretress = new CreatureData("Forretress", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Steel },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Dunsparce = new CreatureData("Dunsparce", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Gligar = new CreatureData("Gligar", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Steelix = new CreatureData("Steelix", 0, 100, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground, DamageType.Steel },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Snubbull = new CreatureData("Snubbull", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Granbull = new CreatureData("Granbull", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Qwilfish = new CreatureData("Qwilfish", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Poison },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Scizor = new CreatureData("Scizor", 0, 100, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Steel },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Shuckle = new CreatureData("Shuckle", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Heracross = new CreatureData("Heracross", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Bug, DamageType.Fighting },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Sneasel = new CreatureData("Sneasel", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ice, DamageType.Dark },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Teddiursa = new CreatureData("Teddiursa", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Ursaring = new CreatureData("Ursaring", 0, 90, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Slugma = new CreatureData("Slugma", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Magcargo = new CreatureData("Magcargo", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire, DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Swinub = new CreatureData("Swinub", 0, 15, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ice, DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Piloswine = new CreatureData("Piloswine", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ice, DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Corsola = new CreatureData("Corsola", 0, 50, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Rock },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Remoraid = new CreatureData("Remoraid", 0, 10, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Octillery = new CreatureData("Octillery", 0, 70, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Delibird = new CreatureData("Delibird", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ice, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Mantine = new CreatureData("Mantine", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Skarmory = new CreatureData("Skarmory", 0, 85, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Steel, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Houndour = new CreatureData("Houndour", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Houndoom = new CreatureData("Houndoom", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire, DamageType.Dark },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Kingdra = new CreatureData("Kingdra", 0, 90, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water, DamageType.Dragon },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Phanpy = new CreatureData("Phanpy", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Donphan = new CreatureData("Donphan", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Porygon2 = new CreatureData("Porygon2", 0, 75, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Stantler = new CreatureData("Stantler", 0, 55, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Smeargle = new CreatureData("Smeargle", 0, 40, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Tyrogue = new CreatureData("Tyrogue", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Hitmontop = new CreatureData("Hitmontop", 0, 60, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Smoochum = new CreatureData("Smoochum", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Ice, DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Elekid = new CreatureData("Elekid", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Magby = new CreatureData("Magby", 0, 30, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Miltank = new CreatureData("Miltank", 0, 80, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Normal },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Blissey = new CreatureData("Blissey", 0, 100, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fighting },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Raikou = new CreatureData("Raikou", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Electric },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Entei = new CreatureData("Entei", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Suicune = new CreatureData("Suicune", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Water },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Larvitar = new CreatureData("Larvitar", 0, 20, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Rock, DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Pupitar = new CreatureData("Pupitar", 0, 65, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Rock, DamageType.Ground },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Tyranitar = new CreatureData("Tyranitar", 0, 100, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Rock, DamageType.Dark },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Lugia = new CreatureData("Lugia", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Psychic, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Hooh = new CreatureData("Ho-oh", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Fire, DamageType.Flying },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
        public static CreatureData Celebi = new CreatureData("Celebi", 0, 0, 0, 0, FrontAttack.None,
            new List<DamageType>() { DamageType.Grass, DamageType.Psychic },
            new List<string>() { "", "", "", },
            new List<Loot>() { }
        );
    }
}

