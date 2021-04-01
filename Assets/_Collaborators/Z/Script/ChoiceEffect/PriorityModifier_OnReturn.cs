using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PriorityModifier_OnReturn : PriorityModifier {

        public override int GetPriority(int Value, Character Source, Pair P)
        {
            if (Source.GetKeyBase().GetKey("Return") > 0)
                return Value * 999;
            return Value;
        }
    }
}