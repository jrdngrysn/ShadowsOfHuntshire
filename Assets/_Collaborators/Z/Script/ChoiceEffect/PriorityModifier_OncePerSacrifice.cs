using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PriorityModifier_OncePerSacrifice : PriorityModifier {

        public override int GetPriority(int Value, Character Source, Pair P)
        {
            if (Source.GetKeyBase().GetKey("SacrificeEvent") > 0)
                return -1;
            return Value;
        }
    }
}