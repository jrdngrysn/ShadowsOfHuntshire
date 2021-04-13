using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PriorityModifier_Twins : PriorityModifier {
        public string NameI;
        public string NameII;

        public override int GetPriority(int Value, Character Source, Pair P)
        {
            if (Character.Find(NameI) && Character.Find(NameI).Active && Character.Find(NameII) && Character.Find(NameII).Active)
                return -1;
            return Value;
        }
    }
}