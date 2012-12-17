using System;
using System.Collections.Generic;
using Pokemon.Objects;

namespace Pokemon.Constants
{
    public static class CreatureLists
    {
        #region All Creatures
        public static Dictionary<string, CreatureData> AllCreatures = new Dictionary<string, CreatureData>
        { { Creatures.ExampleCreature.Name, Creatures.ExampleCreature },
        };
        #endregion
    }
}

