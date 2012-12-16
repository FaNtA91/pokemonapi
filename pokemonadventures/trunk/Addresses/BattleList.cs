using System.Collections.Generic;

namespace Pokemon.Addresses
{
    /// <summary>
    /// Battle list adddresses and distances.
    /// </summary>
    public static class BattleList
    {
        /// <summary>
        /// Distance between creatures.
        /// </summary>
        public static uint StepCreatures = 0xA0;

        /// <summary>
        /// Maximum number of creatures.
        /// </summary>
        public static uint MaxCreatures = 250;

        /// <summary>
        /// Start of the battle list.
        /// </summary>
        public static uint Start = 0x632F30; //8.50

        /// <summary>
        /// End of the battle list.
        /// </summary>
        public static uint End = Start + (StepCreatures * MaxCreatures);
    }
}
