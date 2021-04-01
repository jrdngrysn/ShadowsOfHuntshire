using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PriorityModifier_OnlyAfterSacrifice : PriorityModifier {

        public override int GetPriority(int Value, Character Source, Pair P)
        {
            if (GlobalControl.Main.GetSacrificeActive())
                return Value * 10;
            else
                return -1;
        }
    }
}