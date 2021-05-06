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
            if (((Character.Find(ActiveName) && Character.Find(ActiveName).Active) || ActiveName == "")
                && (!Character.Find(DeathName) || !Character.Find(DeathName).Active || DeathName == ""))
                return Value;
            return -1;
        }
    }
}