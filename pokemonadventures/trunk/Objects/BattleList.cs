using System;
using System.Collections.Generic;

namespace Pokemon.Objects
{
    /// <summary>
    /// Battle list object.
    /// </summary>
    public class BattleList
    {
        private Client client;

        /// <summary>
        /// Create a battlelist object.
        /// </summary>
        /// <param name="c"></param>
        public BattleList(Client c)
        {
            client = c;
        }

        /// <summary>
        /// Get a list of all the creatures on the battlelist.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Creature> GetCreatures()
        {
            for (uint i = Addresses.BattleList.Start; i < Addresses.BattleList.End; i += Addresses.BattleList.StepCreatures)
            {
                if (client.Memory.ReadByte(i + Addresses.Creature.DistanceIsVisible) == 1)
                    yield return new Creature(client, i);
            }
        }

        /// <summary>
        /// Show invisible creatures permanently.
        /// </summary>
        public void ShowInvisible()
        {
            client.Memory.WriteByte(Addresses.Map.RevealInvisible1,
                Addresses.Map.RevealInvisible1Edited);
            client.Memory.WriteByte(Addresses.Map.RevealInvisible2,
                Addresses.Map.RevealInvisible2Edited);
        }

        /// <summary>
        /// Hide invisible creatures permanently.
        /// </summary>
        public void HideInvisible()
        {
            client.Memory.WriteByte(Addresses.Map.RevealInvisible1,
                Addresses.Map.RevealInvisible1Default);
            client.Memory.WriteByte(Addresses.Map.RevealInvisible2,
                Addresses.Map.RevealInvisible2Default);
        }
    }
    
}
