using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace THAN
{
    public class PriorityModifier_EndingTrigger : PriorityModifier {
        public string Key;
        public Vector2 ValueRange;

        public override int GetPriority(int Value, Character Source, Pair P)
        {
            if (KeyBase.Main.GetKey(Key) < ValueRange.x || KeyBase.Main.GetKey(Key) > ValueRange.y)
                return -1;
            return base.GetPriority(Value, Source, P);
        }
    }
}