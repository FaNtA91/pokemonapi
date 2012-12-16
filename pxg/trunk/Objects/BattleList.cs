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

        public List<Creature> GetCreatures(Predicate<Creature> match)
        {
            List<Creature> creatures = new List<Creature>();
            for (uint i = Addresses.BattleList.Start; i < Addresses.BattleList.End; i += Addresses.BattleList.StepCreatures)
            {
                Creature creature = new Creature(client, i);
                if (match(creature))
                    creatures.Add(creature);
            }
            return creatures;

        }

        /// <summary>
        /// Get a list of all the cratures with the specified string in the name.
        /// </summary>
        /// <param name="creatureName"></param>
        /// <returns></returns>
        public List<Creature> GetCreatureByName(string name)
        {
            return GetCreatures(name, false);
        }

        /// <summary>
        /// Get a list of all the creatures on the battlelist.
        /// </summary>
        /// <returns></returns>
        public List<Creature> GetCreaturesInBattle()
        {
            List<Creature> creatures = new List<Creature>();
            for (uint i = Addresses.BattleList.Start; i < Addresses.BattleList.End; i += Addresses.BattleList.StepCreatures)
            {
                if (client.Memory.ReadByte(i + Addresses.Creature.DistanceIsVisible) == 1)
                    creatures.Add(new Creature(client, i));
            }
            return creatures;
        }

        /// <summary>
        /// Get a list of all the creatures with the specified name.
        /// </summary>
        /// <param name="creatureName"></param>
        /// <param name="wholeWord"></param>
        /// <returns></returns>
        public List<Creature> GetCreatures(string creatureName, bool wholeWord)
        {
            return GetCreaturesInBattle().FindAll(delegate(Creature c)
            {
                if (wholeWord)
                    return c.Name.Equals(creatureName, StringComparison.CurrentCultureIgnoreCase);
                else
                    return c.Name.IndexOf(creatureName, StringComparison.CurrentCultureIgnoreCase) != -1;
            });
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
