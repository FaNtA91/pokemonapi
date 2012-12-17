using System;
using System.Collections.Generic;
using Pokemon.Objects;

namespace Pokemon.Constants
{
    public static class Creatures
    {
        public static CreatureData ExampleCreature = new CreatureData("Example Creature", 185, 70, 0, 0, 80, false, false, FrontAttack.None,
            new List<DamageType>() { },
            new List<DamageModifier>() { new DamageModifier(DamageType.Fire, 10) },
            new List<DamageModifier>() { },
            new List<string>() { "Example of Speak 1.", "Example of Speak 2.", },
            new List<Loot>() { }
        );
    }
}

