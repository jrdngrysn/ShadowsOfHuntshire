using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PriorityModifier_Survivor : PriorityModifier {
        public string ActiveName;
        public string DeathName;

        public override int GetPriority(int Value, Character Source, Pair P)
        {
            if (Character.Find(ActiveName) && Character.Find(ActiveName).Active && (!Character.Find(DeathName) || !Character.Find(DeathName).Active))
                return Value;
            return -1;
        }
    }
}